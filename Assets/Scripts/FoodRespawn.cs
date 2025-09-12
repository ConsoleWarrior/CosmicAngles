using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FoodRespawn : MonoBehaviour
{
    private float time = 0.025f;
    public ObjectPool<GameObject> scrapPool;
    [SerializeField] GameObject scrapPrefab;

    void Start()
    {
        scrapPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(scrapPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => { obj.SetActive(false); obj.transform.SetParent(transform); }, actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 20000, maxSize: 40000);

        //StartCoroutine("RespawnCoro");
    }

    //public void ScrapRespawnSector(int sectorScrapValue, int minX, int maxX, int minY, int maxY)
    //{
    //    StartCoroutine(RespawnCoro(sectorScrapValue, minX, maxX, minY, maxY));
    //}
    public void ReturnToPoolSectorScraps()
    {
        StopAllCoroutines();
        for (int i = 0; i < transform.childCount; i++)
        {
            scrapPool.Release(transform.GetChild(i).gameObject);
        }
    }
    //public IEnumerator SlowReturnToPoolScrap(Transform parent)
    //{
    //    while (parent.childCount > 0)
    //    {
    //        yield return new WaitForSeconds(time);
    //    }
    //}
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

    public IEnumerator RespawnCoro(int sectorScrapValue, int minX, int maxX, int minY, int maxY, Transform parent)
    {
        while (true)
        {
            var scrapObj = scrapPool.Get();
            scrapObj.transform.SetParent(parent);
            scrapObj.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            var scrap = scrapObj.GetComponent<Scrap>();
            scrap.value = sectorScrapValue;
            scrap.scrapPool = scrapPool;
            //Respawn();
            if (parent.childCount > 10000 && time != 1)
            {
                time = 1;
            }
            if (parent.childCount < 10000 && parent.childCount > 5000 && time != 0.2f)
            {
                time = 0.2f;
            }
            else if (scrapPool.CountActive <= 5000 && time != 0.025f)
            {
                time = 0.025f;
            }
            yield return new WaitForSeconds(time);
        }
    }
}
