using System.Collections;
using UnityEngine;

public class Acceleration : Modulus
{
    Player player;
    [SerializeField] float duration;
    [SerializeField] float accelerationSpeed;
    bool flag = false;
    float temp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !flag)
        {
            flag = true;
            Activate();
        }
    }
    void OnDestroy()
    {
        if (flag)
            player.speed  = temp;
    }
    void Activate()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        temp = player.speed;
        ChangePlayerSpeed(accelerationSpeed);
        //player.transform.Find("ShipSprite").GetComponent<SpriteRenderer>().color = Color.red;
        transform.GetComponent<SpriteRenderer>().color = Color.red;
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
        //player.transform.Find("ShipSprite").GetComponent<SpriteRenderer>().color = Color.white;
        transform.GetComponent<SpriteRenderer>().color = Color.white;
        flag = false;
    }
    public override string GetCharacter()
    {
        return "AccelerationSpeed= " + accelerationSpeed + "\nDuration= " + duration;
    }
}
