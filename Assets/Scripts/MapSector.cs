using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapSector : MonoBehaviour
{
    public string nameSector;
    [SerializeField] int maxPopulation;
    float enemyRespawnTime = 1;
    float scrapRespawnTime;
    [SerializeField] List<GameObject> enemyPrefabs = new();
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;
    bool overflow = false;
    FoodRespawn foodRespawn;
    [SerializeField] int sectorScrapValue;
    Transform scrapParent;


    private void Start()
    {
        foodRespawn = GameObject.Find("Scraps").GetComponent<FoodRespawn>();
        scrapParent = transform.GetChild(0);
    }

    public void StartRespawnSector()
    {
        //foodRespawn = GameObject.Find("Scraps").GetComponent<FoodRespawn>();
        //Invoke(nameof(StartRespawnScrap), 0.5f);
        StopAllCoroutines();

        StartCoroutine(RespawnScrapCoro());
        StartCoroutine(RespawnEnemyCoro());

    }
    public void StopRespawnSector()
    {
        StopAllCoroutines();
        Debug.Log(foodRespawn.scrapPool.CountAll);
        StartCoroutine(SlowReturnToPoolScrap());
        //foodRespawn.ReturnToPoolSectorScraps();
        //foreach(var enemy in transform.GetChild)
    }
    void StartRespawnScrap()
    {
        //foodRespawn.ScrapRespawnSector(sectorScrapValue, minX, maxX, minY, maxY);
        StartCoroutine(foodRespawn.RespawnCoro(sectorScrapValue, minX, maxX, minY, maxY, scrapParent));
    }
    IEnumerator RespawnScrapCoro()
    {
        while (true)
        {
            var scrapObj = foodRespawn.scrapPool.Get();
            scrapObj.transform.SetParent(scrapParent);
            scrapObj.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            var scrap = scrapObj.GetComponent<Scrap>();
            scrap.value = sectorScrapValue;
            scrap.scrapPool = foodRespawn.scrapPool;
            //Respawn();
            if (scrapParent.childCount > 10000 && scrapRespawnTime != 1)
            {
                scrapRespawnTime = 1;
            }
            if (scrapParent.childCount < 10000 && scrapParent.childCount > 5000 && scrapRespawnTime != 0.2f)
            {
                scrapRespawnTime = 0.2f;
            }
            else if (scrapParent.childCount <= 5000 && scrapRespawnTime != 0.03f)
            {
                scrapRespawnTime = 0.03f;
            }
            yield return new WaitForSeconds(scrapRespawnTime);
        }
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
        if (count < maxPopulation * 0.3f) enemyRespawnTime = 2f;
        else if (count < maxPopulation * 0.5f) enemyRespawnTime = 6;
        else if (count < maxPopulation * 0.7f) enemyRespawnTime = 12;
        else if (count < maxPopulation) enemyRespawnTime = 20;
        else if (count > maxPopulation) overflow = true;
        //Debug.Log(nameSector+" time " + time);
    }
    IEnumerator RespawnEnemyCoro()
    {
        //yield return new WaitForSeconds(1);

        while (true)
        {
            RespawnSector();
            yield return new WaitForSeconds(enemyRespawnTime);
        }
    }
    IEnumerator SlowReturnToPoolScrap()
    {
        while (scrapParent.childCount > 0)
        {
            foodRespawn.scrapPool.Release(scrapParent.GetChild(0).gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
