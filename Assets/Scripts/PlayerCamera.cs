using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}