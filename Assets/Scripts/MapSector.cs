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
    FoodRespawn foodRespawn;
    [SerializeField] int sectorScrapValue;
    private void Start()
    {
        foodRespawn = GameObject.Find("Scraps").GetComponent<FoodRespawn>();
    }

    public void StartRespawnSector()
    {
        //foodRespawn = GameObject.Find("Scraps").GetComponent<FoodRespawn>();
        Invoke("StartRespawnScrap", 0.5f);

        StartCoroutine("RespawnCoro");
    }
    public void StopRespawnSector()
    {
        StopAllCoroutines();
        foodRespawn.ReturnToPoolSectorScraps();
        //foreach(var enemy in transform.GetChild)
    }
    void StartRespawnScrap()
    {
        foodRespawn.ScrapRespawnSector(sectorScrapValue, minX, maxX, minY, maxY);
    }
    void RespawnSector()
    {
        if (!overflow)
            foreach (var prefab in enemyPrefabs)
            {
                GameObject c = (GameObject)Instantiate(prefab, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);
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
        //yield return new WaitForSeconds(1);

        while (true)
        {
            RespawnSector();
            yield return new WaitForSeconds(time);
        }
    }

}
