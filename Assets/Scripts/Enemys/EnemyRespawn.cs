using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public List<MapSector> sectors = new();

    void Start()
    {
        foreach(var sector in sectors)
        {
            sector.StartRespawnSector();
        }
    }
}
