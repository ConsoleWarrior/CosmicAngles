using UnityEngine;

public class Cosmoport : MonoBehaviour
{
    public Pause pause;
    public int number;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            pause.PauseOpenInventory();
            pause.cosmoPortPanels[number].SetActive(true);
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