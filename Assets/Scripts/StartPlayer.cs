
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrefabs;
    Player player;
    public Camera playerCamera;
    [SerializeField] GameObject gameOver;
    public Transform shipGrid;
    public RepairBlok[] repairBloks;
    public Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var obj = Instantiate(playerPrefabs[0], gameObject.transform);
        player = obj.GetComponent<Player>();
        player.playerCamera = playerCamera;
        playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
        player.gameOver = gameOver;
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).GetComponent<Cell>() != null)
            {
                foreach (var blok in repairBloks)
                {
                    blok.cells.Add(player.transform.GetChild(i).GetComponent<Cell>());
                }
            }
            //repairBlok.cells.Add(player.transform.GetChild(i).GetComponent<Cell>());
        }
        //Debug.Log(repairBlok.cells.Count);
    }

    public void CallNewShipPrefab1()
    {
        if(inventory.kredit >= 25000)
        {
            Destroy(player.gameObject);

            var obj = Instantiate(playerPrefabs[1], gameObject.transform);
            player = obj.GetComponent<Player>();
            player.playerCamera = playerCamera;
            playerCamera.gameObject.GetComponent<PlayerCamera>().player = obj.transform;
            player.gameOver = gameOver;
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
        }
        else
        {
            Debug.Log("нет 25к");
        }
        
    }
}
