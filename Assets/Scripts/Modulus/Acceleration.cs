using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Acceleration : Modulus
{
    Player player;
    [SerializeField] float duration;
    [SerializeField] float accelerationSpeed;
    bool flag = false;
    float temp;
    [SerializeField] float tank = 100;
    [SerializeField] SpriteRenderer activateSprite;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        temp = player.speed;
        StartCoroutine(Refill());
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !flag)
        //{
        //    flag = true;
        //    Activate();
        //}
        if (Input.GetKey(KeyCode.Space) && tank > 0)
        {
            tank -= Time.deltaTime * 10;
            player.speed = accelerationSpeed;
            activateSprite.enabled = true;
            //transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (player.speed != temp)
        {
            player.speed = temp;
            activateSprite.enabled = false;
            //transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    void OnDestroy()
    {
        if (flag)
            player.speed = temp;
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
    IEnumerator InvokeRoutine(System.Action f, float delay) //корутина на методом с параметрами!
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
    IEnumerator Refill()
    {
        while (true)
        {
            if (tank <= 98) tank += 2;
            transform.GetComponent<SpriteRenderer>().color = new Color(tank / 100, tank / 100, tank / 100, 1);

            yield return new WaitForSeconds(1);
        }
    }
}
