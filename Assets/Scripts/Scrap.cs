using UnityEngine;

public class Scrap : MonoBehaviour
{
    public float value;
    private float startInvisibleTime = 1;
    void Start()
    {
        transform.GetComponent<Collider2D>().enabled = false;
        Invoke("InvisibleTime", startInvisibleTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cell") || other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    void InvisibleTime()
    {
        transform.GetComponent<Collider2D>().enabled = true;
    }
}
