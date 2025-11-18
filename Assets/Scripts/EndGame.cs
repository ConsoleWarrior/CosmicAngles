using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject winPicture;
    [SerializeField] AudioManager audioManager;
    [SerializeField] GameObject buttonPause;
    [SerializeField] GameObject gameover;

    private void OnDisable()
    {
        if (Time.timeScale != 0 && !gameover.activeSelf)
        {
            buttonPause.SetActive(false);
            winPicture.SetActive(true);
            audioManager.SoundPlay0();
            Time.timeScale = 0;
        }
        //try
        //{

        //}
        //catch { Debug.Log("ошибка рестарта, все ок"); }
    }
}
