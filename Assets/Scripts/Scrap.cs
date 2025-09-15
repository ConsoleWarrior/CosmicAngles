using UnityEngine;
using UnityEngine.Pool;

public class Scrap : MonoBehaviour
{
    public float value;
    //private float startInvisibleTime = 1;
    public ObjectPool<GameObject> scrapPool;


    //void OnEnable()
    //{
    //    transform.GetComponent<Collider2D>().enabled = false;
    //    Invoke("InvisibleTime", startInvisibleTime);
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Cell"))
    //    {
    //        if(gameObject.name != "Scrap(Clone)") Destroy(this.gameObject);
    //        else scrapPool.Release(gameObject);
    //        //Destroy(this.gameObject);
    //    }
    //}
    public void ReturnScrapToPool()
    {
        if (gameObject.name == "Scrap(Clone)") scrapPool.Release(gameObject); 
        else Destroy(this.gameObject);
    }
    //void InvisibleTime()
    //{
    //    transform.GetComponent<Collider2D>().enabled = true;
    //}
}
