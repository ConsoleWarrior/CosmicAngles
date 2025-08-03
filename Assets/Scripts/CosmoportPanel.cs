using UnityEngine;

public class CosmoportPanel : MonoBehaviour
{
}
//using UnityEngine;

//public class CosmoPort : MonoBehaviour
//{
//    public Pause pause;
//    public int number;

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Cell"))
//        {
//            gameObject.GetComponent<Collider2D>().enabled = false;
//            pause.PauseOpenInventory();
//            pause.cosmoPortPanels[number].SetActive(true);
//        }
//    }
//    public void ColliderActivate()
//    {
//        Invoke("ColliderActivateInvoke", 1);
//    }
//    void ColliderActivateInvoke()
//    {
//        gameObject.GetComponent<Collider2D>().enabled = true;

//    }
//}