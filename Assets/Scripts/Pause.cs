using UnityEngine;

public class Pause : MonoBehaviour
{
    public Transform player;
    public GameObject InventarPanel;
    public GameObject[] cosmoPortPanels;
    public GameObject[] cosmoPorts;
    public void Play()
    {
        InventarPanel.SetActive(false);
        foreach (var i in cosmoPortPanels)
        {
            i.SetActive(false);
        }
        foreach (var i in cosmoPorts)
        {
            i.GetComponent<Cosmoport>().ColliderActivate();
        }
        Time.timeScale = 1f;
    }
    public void PauseOpenInventory()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.rotation = Quaternion.identity;
        InventarPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
