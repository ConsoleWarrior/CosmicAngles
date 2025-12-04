using UnityEngine;

public class Pause : MonoBehaviour
{
    public Transform player;
    public GameObject InventarPanel;
    //public GameObject[] cosmoPortPanels;
    public Transform cosmoPorts;


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
        for (int i = 0; i < cosmoPorts.childCount; i++)
        {
            cosmoPorts.GetChild(i).GetComponent<Cosmoport>().panel.SetActive(false);
            cosmoPorts.GetChild(i).GetComponent<Cosmoport>().ColliderActivate();
        }
        Time.timeScale = 1f;
    }
    public void PauseOpenInventory()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        player.rotation = Quaternion.identity;
        player.gameObject.GetComponent<Player>().audioManager.a.Stop();
        var slot = player.gameObject.GetComponent<Player>().core.slot;
        slot.transform.Find("LockImg").gameObject.SetActive(true);
        slot.transform.Find("LockImg").SetAsLastSibling();
        if (slot.GetComponent<UISlot>().currentItem != null)
        slot.GetComponent<UISlot>().currentItem.group.blocksRaycasts = false;

        InventarPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
