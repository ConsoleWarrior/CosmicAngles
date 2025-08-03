using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrefabs;
    Player player;
    public Camera playerCamera;
    [SerializeField] GameObject gameOver;
    [SerializeField] AudioManager audioManager;
    public Transform shipGrid;
    public RepairBlok[] repairBloks;
    Inventory inventory;
    public GameObject buyButton;
    public GameObject buyButton2;
    int shipLevel = 0;
    //[SerializeField] RebootGuns reboot;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var obj = Instantiate(playerPrefabs[0], gameObject.transform);
        player = obj.GetComponent<Player>();
        player.playerCamera = playerCamera;
        playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
        player.gameOver = gameOver;
        player.audioManager = audioManager;
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).GetComponent<Cell>() != null)
            {
                foreach (var blok in repairBloks)
                {
                    blok.cells.Add(player.transform.GetChild(i).GetComponent<Cell>());
                }
            }
        }
    }

    public void CallNewShipPrefab1()
    {
        if (inventory.kredit >= 25000 && shipLevel == 0)
        {
            inventory.UpdateKredit(-25000);
            player.isDestroing = true;
            Destroy(player.gameObject);

            var obj = Instantiate(playerPrefabs[1], gameObject.transform);
            player = obj.GetComponent<Player>();
            player.playerCamera = playerCamera;
            playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            player.gameOver = gameOver;
            player.audioManager = audioManager;
            foreach (var blok in repairBloks)
            {
                blok.cells = new();
                for (int i = 0; i < player.transform.childCount; i++)
                {
                    if (player.transform.GetChild(i).GetComponent<Cell>() != null)
                    {
                        blok.cells.Add(player.transform.GetChild(i).GetComponent<Cell>());
                    }
                }
            }
            for (int i = 0; i < shipGrid.childCount; i++)
            {
                if (shipGrid.GetChild(i).name == "Slot7")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(7).GetComponent<Cell>();
                }

                if (shipGrid.GetChild(i).name == "Slot8")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(8).GetComponent<Cell>();
                }
                if (shipGrid.GetChild(i).name == "Slot9")
                {
                    shipGrid.GetChild(i).gameObject.SetActive(true);
                    shipGrid.GetChild(i).GetComponent<UISlot>().cell = player.transform.GetChild(9).GetComponent<Cell>();
                }
            }
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
            Destroy(player.gameObject);

            var obj = Instantiate(playerPrefabs[2], gameObject.transform);
            player = obj.GetComponent<Player>();
            player.playerCamera = playerCamera;
            playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            player.gameOver = gameOver;
            player.audioManager = audioManager;

            foreach (var blok in repairBloks)
            {
                blok.cells = new();
                for (int i = 0; i < player.transform.childCount; i++)
                {
                    if (player.transform.GetChild(i).GetComponent<Cell>() != null)
                    {
                        blok.cells.Add(player.transform.GetChild(i).GetComponent<Cell>());
                    }
                }
            }
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
}
