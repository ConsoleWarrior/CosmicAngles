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


    void RespawnSector()
    {
        foreach (var prefab in enemyPrefabs)
        {
            GameObject c = (GameObject)Instantiate(prefab, new Vector2(Random.Range(minX, minX), Random.Range(minY, maxY)), Quaternion.identity);
            c.transform.SetParent(transform, true);
        }
        var count = transform.childCount;
        if (count < maxPopulation * 0.3f) time = 1.5f;
        else if (count < maxPopulation * 0.5f) time = 5;
        else if (count < maxPopulation * 0.7f) time = 7;
        else if (count > maxPopulation) time = 60;
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
