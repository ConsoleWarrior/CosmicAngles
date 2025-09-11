using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public List<MapSector> sectors = new();
    Transform player;
    int currentSector = 99;

    //void Start()
    //{
    //    foreach (var sector in sectors)
    //    {
    //        sector.StartRespawnSector();
    //    }
    //}
    private void Update()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (currentSector != 0 && player.position.y > -100 && player.position.y < 100)
        {
            sectors[1].StopRespawnSector();
            sectors[0].StartRespawnSector();
            //sectors[1].StartRespawnSector();
            currentSector = 0;
        }
        else if (currentSector != 1 && player.position.y > 100 && player.position.y < 300)
        {
            sectors[0].StopRespawnSector();
            sectors[1].StartRespawnSector();
            //sectors[2].StartRespawnSector();
            currentSector = 1;
        }
        else if (currentSector != 2 && player.position.y > 300 && player.position.y < 500)
        {
            sectors[1].StopRespawnSector();
            sectors[2].StartRespawnSector();
            //sectors[2].StartRespawnSector();
            currentSector = 2;
        }
        else if (currentSector != 3 && player.position.y > 300 && player.position.y < 500)
        {
            sectors[2].StopRespawnSector();
            sectors[3].StartRespawnSector();
            //sectors[2].StartRespawnSector();
            currentSector = 3;
        }
        //else if (currentSector != 4 && player.position.y > 300 && player.position.y < 500)
        //{
        //    sectors[2].StartRespawnSector();
        //    sectors[2].StartRespawnSector();
        //    currentSector = 4;
        //}
    }
}
