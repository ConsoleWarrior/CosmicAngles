using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    public float speed;
    void Start()
    {

    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        Moving();
    }
    void Moving()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, objPosition, speed * Time.deltaTime);
        }
    }
}
