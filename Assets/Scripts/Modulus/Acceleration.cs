using System.Collections;
using UnityEngine;

public class Acceleration : Modulus
{
    Player player;
    //[SerializeField] float duration;
    [SerializeField] float accelerationSpeed;
    //bool flag = false;
    float temp;
    [SerializeField] float tank;
    [SerializeField] float refillValues;
    [SerializeField] SpriteRenderer activateSprite;
    [SerializeField] TempValues tempValues;
    public int quality;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        temp = player.speed;
        tempValues = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<TempValues>();
        if (tempValues.currentAccelerationQuality != quality) { tempValues.accelerationTank = tank; tempValues.currentAccelerationQuality = quality; }
        StartCoroutine(Refill());
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !flag)
        //{
        //    flag = true;
        //    Activate();
        //}
        if (Input.GetKey(KeyCode.Space) && tempValues.accelerationTank > 0)
        {
            tempValues.accelerationTank -= Time.deltaTime * 10;
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
        player.speed = temp;
    }
    public void PushDownAndroidButton()
    {
        if (tempValues.accelerationTank > 0)
        {
            tempValues.accelerationTank -= Time.deltaTime * 10;
            player.speed = accelerationSpeed;
            activateSprite.enabled = true;
            //transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    public void PushUpAndroidButton()
    {
        if (player.speed != temp)
        {
            player.speed = temp;
            activateSprite.enabled = false;
            //transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    //void Activate()
    //{
    //    if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    //    temp = player.speed;
    //    ChangePlayerSpeed(accelerationSpeed);
    //    //player.transform.Find("ShipSprite").GetComponent<SpriteRenderer>().color = Color.red;
    //    transform.GetComponent<SpriteRenderer>().color = Color.red;
    //    StartCoroutine(InvokeRoutine(() => ChangePlayerSpeed(temp), 10));
    //}
    //void ChangePlayerSpeed(float rate)
    //{
    //    player.speed = rate;
    //}
    //IEnumerator InvokeRoutine(System.Action f, float delay) //корутина на методом с параметрами!
    //{
    //    yield return new WaitForSeconds(delay);
    //    f();
    //    //player.transform.Find("ShipSprite").GetComponent<SpriteRenderer>().color = Color.white;
    //    transform.GetComponent<SpriteRenderer>().color = Color.white;
    //    flag = false;
    //}
    public override string GetCharacter()
    {
        return "AccelerationSpeed = " + accelerationSpeed + "\nTank = " + tank;
    }
    IEnumerator Refill()
    {
        while (true)
        {
            if (tempValues.accelerationTank <= tank) tempValues.accelerationTank += refillValues;
            transform.GetComponent<SpriteRenderer>().color = new Color(tempValues.accelerationTank / tank, tempValues.accelerationTank / tank, tempValues.accelerationTank / tank, 1);
            yield return new WaitForSeconds(1);
        }
    }
    public void RefillFullTank()
    {
        tempValues = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<TempValues>();
        tempValues.accelerationTank = tank;
    }
}
