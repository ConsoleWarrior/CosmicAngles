using System.Collections;
using UnityEngine;

public class Acceleration : Modulus
{
    Player player;
    [SerializeField] float duration;
    [SerializeField] float accelerationSpeed;
    bool flag = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !flag)
        {
            flag = true;
            Enable();
        }
    }
    void Enable()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        var temp = player.speed;
        ChangePlayerSpeed(accelerationSpeed);
        StartCoroutine(InvokeRoutine(() => ChangePlayerSpeed(temp), duration));
    }
    void ChangePlayerSpeed(float rate)
    {
        player.speed = rate;
    }
    IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
        flag = false;
    }
    public override string GetCharacter()
    {
        return "AccelerationSpeed: " + accelerationSpeed + "\nDuration: " + duration;
    }
}
