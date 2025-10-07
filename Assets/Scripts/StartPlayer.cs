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
        //player = obj.GetComponent<Player>();
        //player.playerCamera = playerCamera;
        //playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
        //player.gameOver = gameOver;
        //player.audioManager = audioManager;
        //repairBlok.RefreshCells(player);
        Initialyze(obj);
    }
    void TransferPlayerData(GameObject newPlayer)
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
    void Initialyze(GameObject obj)
    {
        player = obj.GetComponent<Player>();
        player.playerCamera = playerCamera;
        playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
        player.gameOver = gameOver;
        player.audioManager = audioManager;
        repairBlok.RefreshCells(player);
    }
    public void CallNewShipPrefab1()
    {
        if (inventory.kredit >= 25000 && shipLevel == 0)
        {
            inventory.UpdateKredit(-25000);
            player.isDestroing = true;
            var obj = Instantiate(playerPrefabs[1], gameObject.transform);
            TransferPlayerData(obj);
            Destroy(player.gameObject);

            //player = obj.GetComponent<Player>();
            //player.playerCamera = playerCamera;
            //playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            //player.gameOver = gameOver;
            //player.audioManager = audioManager;
            //repairBlok.RefreshCells(player);
            Initialyze(obj);
            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot7")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(7).GetComponent<Cell>();
                    player.transform.GetChild(7).GetComponent<Cell>().slot = shipGrid.GetChild(i).GetComponent<UISlot>().gameObject;
                }

                if (shipGrid.GetChild(i).name == "Slot8")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(8).GetComponent<Cell>();
                    player.transform.GetChild(8).GetComponent<Cell>().slot = shipGrid.GetChild(i).GetComponent<UISlot>().gameObject;

                }
                if (shipGrid.GetChild(i).name == "Slot9")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(9).GetComponent<Cell>();
                    player.transform.GetChild(9).GetComponent<Cell>().slot = shipGrid.GetChild(i).GetComponent<UISlot>().gameObject;

                }
            }
            repairBlok.RepairNowAll();

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
            inventory.UpdateKredit(-50000);
            player.isDestroing = true;
            var obj = Instantiate(playerPrefabs[2], gameObject.transform);
            TransferPlayerData(obj);
            Destroy(player.gameObject);

            player = obj.GetComponent<Player>();
            player.playerCamera = playerCamera;
            playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            player.gameOver = gameOver;
            player.audioManager = audioManager;
            repairBlok.RefreshCells(player);

            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot10")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(10).GetComponent<Cell>();
                }

                if (shipGrid.GetChild(i).name == "Slot11")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(11).GetComponent<Cell>();
                }
                if (shipGrid.GetChild(i).name == "Slot12")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(12).GetComponent<Cell>();
                }
            }
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
            inventory.UpdateKredit(-100000);
            player.isDestroing = true;
            var obj = Instantiate(playerPrefabs[3], gameObject.transform);
            TransferPlayerData(obj);
            Destroy(player.gameObject);

            player = obj.GetComponent<Player>();
            player.playerCamera = playerCamera;
            playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            player.gameOver = gameOver;
            player.audioManager = audioManager;
            repairBlok.RefreshCells(player);

            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot13")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(13).GetComponent<Cell>();
                }

                if (shipGrid.GetChild(i).name == "Slot14")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(14).GetComponent<Cell>();
                }
            }
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
            inventory.UpdateKredit(-200000);
            player.isDestroing = true;
            var obj = Instantiate(playerPrefabs[4], gameObject.transform);
            TransferPlayerData(obj);
            Destroy(player.gameObject);

            player = obj.GetComponent<Player>();
            player.playerCamera = playerCamera;
            playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            player.gameOver = gameOver;
            player.audioManager = audioManager;
            repairBlok.RefreshCells(player);

            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot15")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(15).GetComponent<Cell>();
                }

                if (shipGrid.GetChild(i).name == "Slot16")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(16).GetComponent<Cell>();
                }
            }
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
            inventory.UpdateKredit(-300000);
            player.isDestroing = true;
            var obj = Instantiate(playerPrefabs[5], gameObject.transform);
            TransferPlayerData(obj);
            Destroy(player.gameObject);

            player = obj.GetComponent<Player>();
            player.playerCamera = playerCamera;
            playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            player.gameOver = gameOver;
            player.audioManager = audioManager;
            repairBlok.RefreshCells(player);

            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot17")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(17).GetComponent<Cell>();
                }

                if (shipGrid.GetChild(i).name == "Slot18")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(18).GetComponent<Cell>();
                }
            }
            shipLevel = 5;
            buyButton5.SetActive(false);
        }
        else
        {
            Debug.Log("нет 200к или не куплен предыдущий апгрейд");
        }
    }
}
