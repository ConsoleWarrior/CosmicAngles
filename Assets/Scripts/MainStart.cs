using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MainStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Send("Йопа рогалик).0 Получи фашист гранату. Тест пройден Урря");
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    private string token = "1085090607";
    private string chatId = "bot8280606822:AAHNbkHRtiQmx33-F5g84EdNt86nvqSr8wI";

    //public void Send(string message) => StartCoroutine(PostMessage(message));
    public void Send(string message) => StartCoroutine(SendPost(message));
    IEnumerator PostMessage(string text)
    {
        string url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={UnityWebRequest.EscapeURL(text)}";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
                Debug.Log("Ошибка: " + webRequest.error);
        }
    }
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
    private IEnumerator SendPost(string text)
    {
        // Очищаем токен от возможных пробелов, которые ты мог случайно скопировать
        string cleanToken = token.Trim();
        string cleanChatId = chatId.Trim();

        // Собираем URL вручную, чтобы точно не ошибиться
        string url = "api.telegram.org"  +cleanToken + "/sendMessage";

        WWWForm form = new WWWForm();
        form.AddField("chat_id", cleanChatId);
        form.AddField("text", text);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Выводим в консоль ПОЛНЫЙ URL (без токена для безопасности), чтобы понять, где косяк
                Debug.LogError($"Ошибка! Ответ сервера: {www.error}");
                Debug.LogError($"Проверь URL (без токена): api.telegram.org.../sendMessage");
            }
            else
            {
                Debug.Log("🎉 Ура! Сообщение в Telegram отправлено!");
            }
        }
    }
}
