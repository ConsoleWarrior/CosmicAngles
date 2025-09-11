using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FoodRespawn : MonoBehaviour
{
    private float time = 0.05f;
    public ObjectPool<GameObject> scrapPool;
    [SerializeField] GameObject scrapPrefab;

    void Start()
    {
        scrapPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(scrapPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 20000, maxSize: 40000);

        //StartCoroutine("RespawnCoro");
    }

    public void ScrapRespawnSector(int sectorScrapValue, int minX, int maxX, int minY, int maxY)
    {
        StartCoroutine(RespawnCoro(sectorScrapValue, minX, maxX, minY, maxY));
    }
    public void ReturnToPoolSectorScraps()
    {
        StopAllCoroutines();
        for (int i = 0; i < transform.childCount; i++)
        {
            scrapPool.Release(transform.GetChild(i).gameObject);
        }
    }
    //public void Respawn()
    //{
    //    GameObject c = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), new Vector2(Random.Range(-100, 100), Random.Range(-100, 100)), Quaternion.identity);
    //    c.transform.SetParent(transform, true);
    //    GameObject b = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), new Vector2(Random.Range(-100, 100), Random.Range(-200, -100)), Quaternion.identity);
    //    b.transform.SetParent(transform, true);
    //    GameObject d = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), new Vector2(Random.Range(-200, -100), Random.Range(-100, 100)), Quaternion.identity);
    //    d.transform.SetParent(transform, true);
    //    GameObject e = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), new Vector2(Random.Range(-100, 100), Random.Range(100, 200)), Quaternion.identity);
    //    e.transform.SetParent(transform, true);
    //}

    IEnumerator RespawnCoro(int sectorScrapValue, int minX, int maxX, int minY, int maxY)
    {
        while (true)
        {
            var scrapObj = scrapPool.Get();
            scrapObj.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            var scrap = scrapObj.GetComponent<Scrap>();
            scrap.value = sectorScrapValue;
            scrap.scrapPool = scrapPool;
            //Respawn();
            if (transform.childCount > 10000 && time != 0.3f)
            {
                time = 0.3f;
            }
            if (transform.childCount < 10000 && transform.childCount > 5000 && time != 0.15f)
            {
                time = 0.15f;
            }
            else if (transform.childCount <= 5000 && time != 0.05f)
            {
                time = 0.05f;
            }
            yield return new WaitForSeconds(time);
        }
    }
}
