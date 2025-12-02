using UnityEngine;

public class ButtonExit : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();

#if UNITY_EDITOR
        // В редакторе, для тестирования, можно остановить игру
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}