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
    public Transform bar;
    //float temp;


    void Start()
    {
        player = transform.parent.GetComponent<Player>();
        StartCoroutine(Refill());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && currentTank > 0)
        {
            currentTank -= Time.deltaTime * 10;
            player.transform.Translate(0, accelerationSpeed * Time.deltaTime, 0);
            activateSprite.enabled = true;
            if (!audioManager.a.isPlaying) audioManager.SoundPlay0();
        }
        else
        {
            activateSprite.enabled = false;
            audioManager.a.Stop();
        }
        bar.localScale = new(currentTank / fullTank, 1, 0);
    }


    IEnumerator Refill()
    {
        while (true)
        {
            if (currentTank < 0)
            {
                yield return new WaitForSeconds(3);
                currentTank = 10;
                continue;
            }
            if (currentTank < fullTank) currentTank += refillValues;
            if (currentTank > fullTank) currentTank = fullTank;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void RefillFullTank()
    {
        currentTank = fullTank;
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
