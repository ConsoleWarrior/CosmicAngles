using System.Collections;
using UnityEngine;

public class Acceleration : Modulus
{

    [SerializeField] float accelerationSpeed;
    [SerializeField] float currentTank;
    [SerializeField] float fullTank;
    [SerializeField] float refillValues;
    [SerializeField] SpriteRenderer activateSprite;
    Player player;
    float temp;
    //[SerializeField] TempValues tempValues;
    //public int quality;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        temp = player.speed;
        GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<TempValues>().accelerationModule = this;

        //if (tempValues.currentAccelerationQuality != quality) { tempValues.currentTank = tank; tempValues.currentAccelerationQuality = quality; }
        StartCoroutine(Refill());
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !flag)
        //{
        //    flag = true;
        //    Activate();
        //}
        if (Input.GetKey(KeyCode.Space) && currentTank > 0)
        {
            currentTank -= Time.deltaTime * 10;
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
        if (currentTank > 0)
        {
            currentTank -= Time.deltaTime * 10;
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

    IEnumerator Refill()
    {
        while (true)
        {
            if (currentTank < fullTank) currentTank += refillValues;
            transform.GetComponent<SpriteRenderer>().color = new Color(currentTank / fullTank, currentTank / fullTank, currentTank / fullTank, 1);
            yield return new WaitForSeconds(1);
        }
    }
    public float CostRefillFullTank()
    {
        return fullTank - currentTank;
    }
    public void RefillFullTank()
    {
        //tempValues = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<TempValues>();
        //tempValues.currentTank = tank;
        currentTank = fullTank;
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public override string GetCharacter()
    {
        return "AccelerationSpeed = " + accelerationSpeed + "\nfulltank = " + fullTank + "l\nrefillValues = " + refillValues + "l/s";
    }
}
