using UnityEngine;

public class Cosmoport : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] Pause pause;
    [SerializeField] RepairBlok repairBlok;
    public int number;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            pause.PauseOpenInventory();
            panel.SetActive(true);
            repairBlok.OutputRepairAndRefillCost();
            //pause.cosmoPortPanels[number].SetActive(true);
        }
    }
    public void ColliderActivate()
    {
        Invoke("ColliderActivateInvoke", 3);
    }
    void ColliderActivateInvoke()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;

    }
}