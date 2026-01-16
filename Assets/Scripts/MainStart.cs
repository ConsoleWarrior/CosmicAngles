using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MainStart : MonoBehaviour
{
    [SerializeField] string token;  //токен бота
    [SerializeField] string chatId; //id юзера
    [SerializeField] TMP_InputField inputText;
    [SerializeField] TMP_InputField inputChatId;


    void Start() => StartCoroutine(GetMessages());

    //void Start()
    //{
    //    Send("Йопа рогалик).0 Получи фашист гранату. Тест пройден Урря");
    //}

    //public void Send(string message) => StartCoroutine(PostMessage(message));
    //public void Send(string message) => StartCoroutine(SendPost(message));
    //IEnumerator PostMessage(string text)
    //{
    //    string url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={UnityWebRequest.EscapeURL(text)}";
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
    //    {
    //        yield return webRequest.SendWebRequest();
    //        if (webRequest.result != UnityWebRequest.Result.Success)
    //            Debug.Log("Ошибка: " + webRequest.error);
    //    }
    //}
    //public string Sendler(string message)
    //{
    //    string url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={UnityWebRequest.EscapeURL(message)}";
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
    //    {
    //        yield return webRequest.SendWebRequest();
    //        if (webRequest.result != UnityWebRequest.Result.Success)
    //            Debug.LogError("Ошибка: " + webRequest.error);
    //    }
    //}

    public void Send(string message) => StartCoroutine(SendPost(message));

    IEnumerator SendPost(string text)
    {
        // Очищаем токен от возможных пробелов, которые ты мог случайно скопировать
        string cleanToken = token.Trim();
        string cleanChatId = inputChatId.text.Trim(); //chatId.Trim();
        if (cleanChatId == "")
        {
            Debug.Log("пустое поле chatId");
            yield return null;
        }
        // Собираем URL вручную, чтобы точно не ошибиться
        string url = "api.telegram.org/bot" + cleanToken + "/sendMessage";
        WWWForm form = new WWWForm();
        form.AddField("chat_id", cleanChatId);
        form.AddField("text", inputText.text);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Выводим в консоль ПОЛНЫЙ URL (без токена для безопасности), чтобы понять, где косяк
                Debug.Log($"Ошибка! Ответ сервера: {www.error}");
            }
            else
            {
                Debug.Log($"Отправлено юзеру({cleanChatId}): {inputText.text}");
            }
        }
    }

    private int lastUpdateId;
    IEnumerator GetMessages()
    {
        while (true) // Бесконечный цикл проверки
        {
            // offset = lastUpdateId + 1 позволяет получать только НОВЫЕ сообщения
            string url = $"https://api.telegram.org/bot{token}/getUpdates?offset={lastUpdateId + 1}";
            //string url = $"https://api.telegram.org/bot{token}/getUpdates";

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    ParseResponse(www.downloadHandler.text);
                }
            }
            yield return new WaitForSeconds(2f); // Проверка каждые 2 секунды
        }
    }

    void ParseResponse(string json)
    {
        // Для парсинга JSON в Unity лучше использовать библиотеку Newtonsoft JSON или SimpleJSON
        // Здесь логика: найти "text" и "update_id"
        // Если update_id > lastUpdateId, значит сообщение новое — выполняем действие в игре

        //var data = JsonConvert.DeserializeObject<YourRootClass>(json);
        //Debug.Log(data.result[0].message.text);

        //Debug.Log("Принято: " + json);
        try
        {
            // Десериализуем весь JSON в наши классы
            TelegramResponse response = JsonConvert.DeserializeObject<TelegramResponse>(json);

            if (response.ok && response.result.Count > 0)
            {
                foreach (var update in response.result)
                {
                    // ИЗВЛЕКАЕМ ТЕКСТ
                    string messageText = update.message.text;
                    string senderName = update.message.chat.first_name;
                    lastUpdateId = update.update_id;
                    Debug.Log($"Сообщение от {senderName}({update.message.chat.id}): {messageText}");

                    // Здесь можно добавить логику для игры
                    if (messageText == "/start")
                    {
                        Debug.Log("Новый запуск бота");
                    }
                    inputChatId.text = update.message.chat.id.ToString();
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Ошибка парсинга: " + e.Message);
        }
    }
}

[System.Serializable]
public class TelegramResponse
{
    public bool ok;
    public List<Update> result;
}

[System.Serializable]
public class Update
{
    public int update_id;
    public Message message;
}

[System.Serializable]
public class Message
{
    public string text;
    public Chat chat;
}

[System.Serializable]
public class Chat
{
    public long id;
    public string first_name;
}