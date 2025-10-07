using Unity.VisualScripting;
using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrefabs;
    Player player;
    Inventory inventory;
    public Camera playerCamera;
    [SerializeField] GameObject gameOver;
    [SerializeField] AudioManager audioManager;
    public Transform shipGrid;
    public RepairBlok repairBlok;
    public GameObject buyButton;
    public GameObject buyButton2;
    public GameObject buyButton3;
    public GameObject buyButton4;
    public GameObject buyButton5;
    int shipLevel = 0;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var obj = Instantiate(playerPrefabs[0], gameObject.transform);
        Initialyze(obj);
    }
    void Initialyze(GameObject obj)
    {
        player = obj.GetComponent<Player>();
        player.playerCamera = playerCamera;
        playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
        player.gameOver = gameOver;
        player.audioManager = audioManager;
        repairBlok.RefreshCells(player);
    }
    void TransferShipData(GameObject newPlayer)
    {
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).GetComponent<Cell>() != null)
            {
                var cell = newPlayer.transform.GetChild(i).GetComponent<Cell>();
                cell.armorThickness = player.transform.GetChild(i).GetComponent<Cell>().armorThickness;
                cell.UpgradeCellSprite(cell.armorThickness);

                cell.slot = player.transform.GetChild(i).GetComponent<Cell>().slot;
                //cell.slot.GetComponent<UISlot>().currentItem.CallNewModule(cell);
                //if(cell.slot.GetComponent<UISlot>().currentItem == null) Debug.Log("item нуль");
                //cell.FullRepair();
            }
        }
    }
    void AddSlot(int i, int number)
    {
        shipGrid.GetChild(i).gameObject.SetActive(true);
        shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(number).GetComponent<Cell>();
        player.transform.GetChild(number).GetComponent<Cell>().slot = shipGrid.GetChild(i).GetComponent<UISlot>().gameObject;
    }
    void DestroyOldShip()
    {
        player.isDestroing = true;
        player.gameObject.SetActive(false);
        Destroy(player.gameObject);
    }
    public void CallNewShipPrefab1()
    {
        if (inventory.kredit >= 25000 && shipLevel == 0)
        {
            var obj = Instantiate(playerPrefabs[1], gameObject.transform);
            TransferShipData(obj);
            DestroyOldShip();
            Initialyze(obj);
            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot7") AddSlot(i, 7);
                if (shipGrid.GetChild(i).name == "Slot8") AddSlot(i, 8);
                if (shipGrid.GetChild(i).name == "Slot9") AddSlot(i, 9);
            }
            repairBlok.RepairNowAll();
            inventory.UpdateKredit(-25000);
            shipLevel = 1;
            buyButton.SetActive(false);
        }
        else
        {
            Debug.Log("нет 25к");
        }
    }
    public void CallNewShipPrefab2()
    {
        if (inventory.kredit >= 50000 && shipLevel == 1)
        {
            var obj = Instantiate(playerPrefabs[2], gameObject.transform);
            TransferShipData(obj);
            DestroyOldShip();
            Initialyze(obj);
            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot10") AddSlot(i, 10);
                if (shipGrid.GetChild(i).name == "Slot11") AddSlot(i, 11);
                if (shipGrid.GetChild(i).name == "Slot12") AddSlot(i, 12);
            }
            repairBlok.RepairNowAll();
            inventory.UpdateKredit(-50000);
            shipLevel = 2;
            buyButton2.SetActive(false);
        }
        else
        {
            Debug.Log("нет 50к или не куплен предыдущий апгрейд");
        }
    }
    public void CallNewShipPrefab3()
    {
        if (inventory.kredit >= 100000 && shipLevel == 2)
        {
            var obj = Instantiate(playerPrefabs[3], gameObject.transform);
            TransferShipData(obj);
            DestroyOldShip();
            Initialyze(obj);
            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot13") AddSlot(i, 13);
                if (shipGrid.GetChild(i).name == "Slot14") AddSlot(i, 14);
            }
            repairBlok.RepairNowAll();
            inventory.UpdateKredit(-100000);
            shipLevel = 3;
            buyButton3.SetActive(false);
        }
        else
        {
            Debug.Log("нет 100к или не куплен предыдущий апгрейд");
        }
    }
    public void CallNewShipPrefab4()
    {
        if (inventory.kredit >= 200000 && shipLevel == 3)
        {
            var obj = Instantiate(playerPrefabs[4], gameObject.transform);
            TransferShipData(obj);
            DestroyOldShip();
            Initialyze(obj);
            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot15") AddSlot(i, 15);
                if (shipGrid.GetChild(i).name == "Slot16") AddSlot(i, 16);
            }
            repairBlok.RepairNowAll();
            inventory.UpdateKredit(-200000);
            shipLevel = 4;
            buyButton4.SetActive(false);
        }
        else
        {
            Debug.Log("нет 200к или не куплен предыдущий апгрейд");
        }
    }
    public void CallNewShipPrefab5()
    {
        if (inventory.kredit >= 300000 && shipLevel == 4)
        {
            var obj = Instantiate(playerPrefabs[5], gameObject.transform);
            TransferShipData(obj);
            DestroyOldShip();
            Initialyze(obj);
            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot17") AddSlot(i, 17);
                if (shipGrid.GetChild(i).name == "Slot18") AddSlot(i, 18);
            }
            repairBlok.RepairNowAll();
            inventory.UpdateKredit(-300000);
            shipLevel = 5;
            buyButton5.SetActive(false);
        }
        else
        {
            Debug.Log("нет 300к или не куплен предыдущий апгрейд");
        }
    }
}
