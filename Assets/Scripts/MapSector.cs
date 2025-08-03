using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapSector : MonoBehaviour
{
    public string nameSector;
    [SerializeField] int maxPopulation;
    float time = 1;
    [SerializeField] List<GameObject> enemyPrefabs = new();
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;
    bool overflow = false;


    void RespawnSector()
    {
        if (!overflow)
            foreach (var prefab in enemyPrefabs)
            {
                GameObject c = (GameObject)Instantiate(prefab, new Vector2(Random.Range(minX, minX), Random.Range(minY, maxY)), Quaternion.identity);
                c.transform.SetParent(transform, true);
            }
        var count = transform.childCount;
        overflow = false;
        if (count < maxPopulation * 0.3f) time = 2f;
        else if (count < maxPopulation * 0.5f) time = 10;
        else if (count < maxPopulation * 0.7f) time = 15;
        else if (count < maxPopulation) time = 20;
        else if (count > maxPopulation) overflow = true;
        //Debug.Log(nameSector+" time " + time);
    }
    IEnumerator RespawnCoro()
    {
        while (true)
        {
            RespawnSector();
            yield return new WaitForSeconds(time);
        }
    }
    public void StartRespawnSector()
    {
        StartCoroutine("RespawnCoro");
    }
}
