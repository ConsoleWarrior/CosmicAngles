using System.Collections;
using UnityEngine;

public class Dash : Modulus
{
    Player player;
    [SerializeField] float distance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoJump();
        }
    }
    public void DoJump()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.transform.position = player.transform.position + player.transform.up * distance;
    }
    void ChangePlayerSpeed(float rate)
    {
        player.speed = rate;
    }
    IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}
