using System.Collections;
using UnityEngine;

public class Accelerator : MonoBehaviour
{

    [SerializeField] float accelerationSpeed;
    [SerializeField] float currentTank;
    [SerializeField] float fullTank;
    [SerializeField] float refillValues;
    [SerializeField] SpriteRenderer activateSprite;
    [SerializeField] AudioManager audioManager;
    Player player;
    float temp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        temp = player.speed;
        //GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<TempValues>().accelerationModule = this;

        StartCoroutine(Refill());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && currentTank > 0)
        {
            currentTank -= Time.deltaTime * 10;
            //player.speed = accelerationSpeed;
            player.transform.Translate(0, accelerationSpeed * Time.deltaTime, 0);
            activateSprite.enabled = true;
            if(!audioManager.a.isPlaying)audioManager.SoundPlay0();
        }
        else //if (player.speed != temp)
        {
            //player.speed = temp;
            activateSprite.enabled = false;
            audioManager.a.Stop();
        }
        //else audioManager.a.Stop();
    }


    IEnumerator Refill()
    {
        while (true)
        {
            if (currentTank < fullTank) currentTank += refillValues;
            if (currentTank > fullTank) currentTank = fullTank;
            //transform.GetComponent<SpriteRenderer>().color = new Color(currentTank / fullTank, currentTank / fullTank, currentTank / fullTank, 1);
            yield return new WaitForSeconds(1);
        }
    }

    public void RefillFullTank()
    {
        //tempValues = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<TempValues>();
        //tempValues.currentTank = tank;
        currentTank = fullTank;
        //transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    //public void PushDownAndroidButton()
    //{
    //    if (currentTank > 0)
    //    {
    //        currentTank -= Time.deltaTime * 10;
    //        player.speed = accelerationSpeed;
    //        activateSprite.enabled = true;
    //        //transform.GetComponent<SpriteRenderer>().color = Color.red;
    //    }
    //}
    //public void PushUpAndroidButton()
    //{
    //    if (player.speed != temp)
    //    {
    //        player.speed = temp;
    //        activateSprite.enabled = false;
    //        //transform.GetComponent<SpriteRenderer>().color = Color.white;
    //    }
    //}
}
