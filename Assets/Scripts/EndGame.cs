using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject winPicture;
    [SerializeField] AudioManager audioManager;
    [SerializeField] GameObject buttonPause;
    [SerializeField] GameObject gameover;

    private void OnDisable()
    {
        if (Time.timeScale != 0 && gameover != null && !gameover.activeSelf)
        {
            if (buttonPause != null) buttonPause.SetActive(false);
            if (winPicture != null)
            {
                winPicture.SetActive(true);
                audioManager.SoundPlay0();
            }
            Time.timeScale = 0;
        }
    }
}
