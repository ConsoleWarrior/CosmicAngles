
using TMPro;
using UnityEngine;

public class InfoPanelTester : MonoBehaviour
{
    public Transform enemys;
    public TextMeshProUGUI enemysCount;

    void Start()
    {
        
    }
    void Update()
    {
        string message = "";
        foreach(var sector in enemys.GetComponent<EnemyRespawn>().sectors)
        {
            //message += sector.name + " enemys now: "+sector.transform.childCount+"\n";
            message += sector.name + ":" + sector.transform.childCount + "/";

        }
        enemysCount.text = message;
    }
}
