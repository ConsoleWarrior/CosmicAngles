using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    public Transform player;
    public GameObject InventarPanel;
    //public GameObject[] cosmoPortPanels;
    public GameObject[] cosmoPorts;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !InventarPanel.activeSelf)
        {
            PauseOpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && InventarPanel.activeSelf)
        {
            Play();
        }
    }
    public void Play()
    {
        InventarPanel.SetActive(false);
        //foreach (var i in cosmoPortPanels)
        //{
        //    i.SetActive(false);
        //}
        foreach (var port in cosmoPorts)
        {
            port.GetComponent<Cosmoport>().panel.SetActive(false);
            port.GetComponent<Cosmoport>().ColliderActivate();
        }
        Time.timeScale = 1f;
    }
    public void PauseOpenInventory()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        player.rotation = Quaternion.identity;
        InventarPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
