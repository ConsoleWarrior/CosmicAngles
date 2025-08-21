using UnityEngine;

public class ShieldBoost : Modulus
{
    public Shield shieldFront;
    public Shield shieldBack;
    Player player;
    [SerializeField] float coefficientTicknessBonus;
    [SerializeField] float shieldCapacityBonus;
    [SerializeField] float shieldVolumeRecoveryBonus;

    void Start()
    {
        ConnectToShip();
    }
    void OnDestroy()
    {
        if (player != null && !player.GetComponent<Player>().isDestroing)
            DisconnectFromShip();
    }

    public void ConnectToShip()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shieldFront = player.shieldFront.GetComponentInChildren<Shield>();
        shieldBack = player.shieldBack.GetComponentInChildren<Shield>();

        shieldFront.shieldCapacity += shieldCapacityBonus;
        shieldFront.shieldVolumeRecovery += shieldVolumeRecoveryBonus;
        shieldFront.coefficientTickness += coefficientTicknessBonus;
        shieldBack.shieldCapacity += shieldCapacityBonus;
        shieldBack.shieldVolumeRecovery += shieldVolumeRecoveryBonus;
        shieldBack.coefficientTickness += coefficientTicknessBonus;
    }
    public void DisconnectFromShip()
    {
        shieldFront.shieldCapacity -= shieldCapacityBonus;
        shieldFront.shieldVolumeRecovery -= shieldVolumeRecoveryBonus;
        shieldFront.coefficientTickness -= coefficientTicknessBonus;
        shieldBack.shieldCapacity -= shieldCapacityBonus;
        shieldBack.shieldVolumeRecovery -= shieldVolumeRecoveryBonus;
        shieldBack.coefficientTickness -= coefficientTicknessBonus;
    }
    public override string GetCharacter()
    {
        return "shieldCapacityBonus = " + shieldCapacityBonus + "\nshieldVolumeRecoveryBonus = " + shieldVolumeRecoveryBonus;
    }
}
