using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform cosmoPort;
    // Start is called before the first frame update
    //void Start()
    //{
    //    cosmoPort = GameObject.FindGameObjectWithTag("Port").transform;
    //}

    void Update()
    {
        Vector2 direction = (cosmoPort.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
