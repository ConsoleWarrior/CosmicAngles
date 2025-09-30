using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public List<MapSector> sectors = new();
    Transform player;
    int currentSector = 99;
    [SerializeField] Pointer pointer;

    void Update()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (currentSector != 0 && player.position.y > -100 && player.position.y < 100)
        {
            sectors[1].StopRespawnSector();
            sectors[0].StartRespawnSector();
            //sectors[1].StartRespawnSector();
            currentSector = 0;
            pointer.SetCurrentCosmoport(0);
        }
        else if (currentSector != 1 && player.position.y > 100 && player.position.y < 300)
        {
            sectors[0].StopRespawnSector();
            sectors[2].StopRespawnSector();
            sectors[1].StartRespawnSector();
            //sectors[2].StartRespawnSector();
            currentSector = 1;
            pointer.SetCurrentCosmoport(1);
        }
        else if (currentSector != 2 && player.position.y > 300 && player.position.y < 500)
        {
            sectors[1].StopRespawnSector();
            sectors[3].StopRespawnSector();
            sectors[2].StartRespawnSector();
            //sectors[2].StartRespawnSector();
            currentSector = 2;
            pointer.SetCurrentCosmoport(2);
        }
        else if (currentSector != 3 && player.position.y > 500 && player.position.y < 700)
        {
            sectors[2].StopRespawnSector();
            sectors[4].StopRespawnSector();
            sectors[3].StartRespawnSector();
            //sectors[2].StartRespawnSector();
            currentSector = 3;
            pointer.SetCurrentCosmoport(3);
        }
        else if (currentSector != 4 && player.position.y > 700 && player.position.y < 900)
        {
            sectors[3].StopRespawnSector();
            sectors[5].StopRespawnSector();
            sectors[4].StartRespawnSector();
            currentSector = 4;
            pointer.SetCurrentCosmoport(4);

        }
        else if (currentSector != 5 && player.position.y > 900 && player.position.y < 1100)
        {
            sectors[4].StopRespawnSector();
            sectors[6].StopRespawnSector();
            sectors[5].StartRespawnSector();
            currentSector = 5;
            pointer.SetCurrentCosmoport(5);
        }
        else if (currentSector != 6 && player.position.y > 1100 && player.position.y < 1300)
        {
            sectors[5].StopRespawnSector();
            sectors[6].StartRespawnSector();
            currentSector = 5;
            pointer.SetCurrentCosmoport(5);
        }
    }
}
