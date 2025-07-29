using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cell"))
        {
            
            
            
            //Destroy(this.gameObject,0.5f);
        }
    }
}
