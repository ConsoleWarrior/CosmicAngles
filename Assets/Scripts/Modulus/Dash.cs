using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Player player;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
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
        var temp = player.speed;
        ChangePlayerSpeed(32);
        StartCoroutine(InvokeRoutine(() => ChangePlayerSpeed(temp), 0.5f));
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
