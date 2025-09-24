using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    //[SerializeField] List<Transform> cosmoports;
    [SerializeField] Transform ports;
    Transform currentCosmoport;
    // Start is called before the first frame update
    //void Start()
    //{
    //    cosmoPort = GameObject.FindGameObjectWithTag("Port").transform;
    //}
    private void Start()
    {
        currentCosmoport = ports.GetChild(0);
    }
    void Update()
    {
        Vector2 direction = (currentCosmoport.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
    public void SetCurrentCosmoport(int number)
    {
        currentCosmoport = ports.GetChild(number);
        switch (number)
        {
            case 0: gameObject.GetComponent<SpriteRenderer>().color = Color.white; break;
            case 1: gameObject.GetComponent<SpriteRenderer>().color = Color.green; break;
            case 2: gameObject.GetComponent<SpriteRenderer>().color = Color.blue; break;
            case 3: gameObject.GetComponent<SpriteRenderer>().color = Color.yellow; break;
            case 4: gameObject.GetComponent<SpriteRenderer>().color = Color.magenta; break;
            case 5: gameObject.GetComponent<SpriteRenderer>().color = Color.red; break;

        }
    }
}
