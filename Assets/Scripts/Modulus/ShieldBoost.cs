using UnityEngine;

public class ShieldBoost : Modulus
{
    public GameObject shieldFront;
    public GameObject shieldBack;
    Player player;

    void Start()
    {
        ConnectToShip();
    }
    void OnDestroy()
    {
        if (player != null && !player.GetComponent<Player>().isDestroing)
            DisconnectFromShip();
    }
    void OnDisable()
    {
        if (player != null && !player.GetComponent<Player>().isDestroing)
            DisconnectFromShip();
    }
    //void OnEnable()
    //{
    //    ConnectToShip();
    //}
    public void ConnectToShip()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shieldFront = player.shieldFront;
        shieldBack = player.shieldBack;

        //shieldFront.GetComponent<HalfCircle>().SetThickness(2);
        //shieldBack.GetComponent<HalfCircle>().SetThickness(2);

        shieldFront.GetComponentInChildren<Shield>().shieldCapacity += 50;
        shieldFront.GetComponentInChildren<Shield>().shieldVolumeRecovery += 5;
        shieldFront.GetComponentInChildren<Shield>().coefficientTickness += 1;
        shieldBack.GetComponentInChildren<Shield>().shieldCapacity += 50;
        shieldBack.GetComponentInChildren<Shield>().shieldVolumeRecovery += 5;
        shieldBack.GetComponentInChildren<Shield>().coefficientTickness += 1;
    }
    public void DisconnectFromShip()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shieldFront = player.shieldFront;
        shieldBack = player.shieldBack;

        //shieldFront.GetComponent<HalfCircle>().SetThickness(1);
        //shieldBack.GetComponent<HalfCircle>().SetThickness(1);

        shieldFront.GetComponentInChildren<Shield>().shieldCapacity -= 50;
        shieldFront.GetComponentInChildren<Shield>().shieldVolumeRecovery -= 5;
        shieldFront.GetComponentInChildren<Shield>().coefficientTickness -= 1;
        shieldBack.GetComponentInChildren<Shield>().shieldCapacity -= 50;
        shieldBack.GetComponentInChildren<Shield>().shieldVolumeRecovery -= 5;
        shieldBack.GetComponentInChildren<Shield>().coefficientTickness -= 1;
    }
    public override string GetCharacter()
    {
        return "shieldCapacity += 50\nshieldVolumeRecovery += 5";
    }
}
