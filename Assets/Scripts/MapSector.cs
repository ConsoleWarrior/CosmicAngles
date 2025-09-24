using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapSector : MonoBehaviour
{
    //public string nameSector;
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
    PoolManager poolManager;
    GameObject enemyObject;
    Transform player;

    void Start()
    {
        foodRespawn = GameObject.Find("Scraps").GetComponent<FoodRespawn>();
        scrapParent = transform.GetChild(0);
        poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
    }

    public void StartRespawnSector()
    {
        StopAllCoroutines();

        StartCoroutine(RespawnScrapCoro());
        StartCoroutine(RespawnEnemyCoro());
    }
    public void StopRespawnSector()
    {
        StopAllCoroutines();
        //Debug.Log(foodRespawn.scrapPool.CountAll);
        StartCoroutine(SlowReturnToPoolScrap());
        StartCoroutine(SlowReturnToPoolEnemy());
    }
    IEnumerator RespawnScrapCoro()
    {
        while (true)
        {
            var scrapObj = foodRespawn.scrapPool.Get();
            scrapObj.transform.SetParent(scrapParent);
            scrapObj.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            var scrap = scrapObj.GetComponent<Scrap>();
            scrap.isCollected = false;
            scrap.value = sectorScrapValue;
            scrap.scrapPool = foodRespawn.scrapPool;
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
    IEnumerator RespawnEnemyCoro()
    {
        while (true)
        {
            RespawnSector();
            yield return new WaitForSeconds(enemyRespawnTime);
        }
    }
    void RespawnSector()
    {
        if (!overflow)
        {
            if (player == null) player = GameObject.FindWithTag("Player").transform;
            foreach (var prefab in enemyPrefabs)
            {
                var position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                while (true)
                {
                    if (Vector3.Distance(player.position, position) > 30) break;
                    else position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                }
                switch (prefab.name)
                {
                    case "Fly": { enemyObject = poolManager.flyPool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }
                    case "Kamikadze": { enemyObject = poolManager.kamikadzePool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }
                    case "Burner": { enemyObject = poolManager.burnerPool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }
                    case "Mommy": { enemyObject = poolManager.mommyPool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }
                    case "Moscito": { enemyObject = poolManager.moscitoPool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }
                    case "Destroyer": { enemyObject = poolManager.destroyerPool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }
                    case "Turel": { enemyObject = poolManager.turelPool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }
                    case "Daddy": { enemyObject = poolManager.daddyPool.Get(); enemyObject.transform.SetParent(transform, true); enemyObject.transform.position = position; break; }

                }
            }
        }
        var count = transform.childCount;
        overflow = false;
        if (count < maxPopulation * 0.3f) enemyRespawnTime = 2f;
        else if (count < maxPopulation * 0.5f) enemyRespawnTime = 6;
        else if (count < maxPopulation * 0.7f) enemyRespawnTime = 12;
        else if (count < maxPopulation) enemyRespawnTime = 20;
        else if (count > maxPopulation) overflow = true;
    }

    IEnumerator SlowReturnToPoolScrap()
    {
        while (scrapParent.childCount > 0)
        {
            foodRespawn.scrapPool.Release(scrapParent.GetChild(0).gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator SlowReturnToPoolEnemy()
    {
        while (transform.childCount > 2)
        {
            var c = transform.GetChild(2).gameObject;
            switch (c.name)
            {
                case "Fly(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.flyPool.Release(c); break; }
                case "Kamikadze(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.kamikadzePool.Release(c); break; }
                case "Burner(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.burnerPool.Release(c); break; }
                case "Mommy(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.mommyPool.Release(c); break; }
                case "Moscito(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.moscitoPool.Release(c); break; }
                case "Destroyer(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.destroyerPool.Release(c); break; }
                case "Turel(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.turelPool.Release(c); break; }
                case "Daddy(Clone)": { c.transform.SetParent(poolManager.transform); poolManager.daddyPool.Release(c); break; }

            }
            yield return new WaitForSeconds(3f);
        }
    }
}
