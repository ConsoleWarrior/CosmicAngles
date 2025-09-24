using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public ObjectPool<GameObject> gunBulletPool;
    [SerializeField] GameObject gunBulletPrefab;
    public ObjectPool<GameObject> rocketBulletPool;
    [SerializeField] GameObject rocketBulletPrefab;
    public ObjectPool<GameObject> blasterBulletPool;
    [SerializeField] GameObject blasterBulletPrefab;
    public ObjectPool<GameObject> splashBulletPool;
    [SerializeField] GameObject splashBulletPrefab;

    public ObjectPool<GameObject> flyBulletPool;
    [SerializeField] GameObject flyBulletPrefab;
    public ObjectPool<GameObject> destroyerBulletPool;
    [SerializeField] GameObject destroyerBulletPrefab;
    public ObjectPool<GameObject> mommyBulletPool;
    [SerializeField] GameObject mommyBulletPrefab;
    public ObjectPool<GameObject> daddyBulletPool;
    [SerializeField] GameObject daddyBulletPrefab;

    public ObjectPool<GameObject> flyPool;
    [SerializeField] GameObject flyPrefab;
    public ObjectPool<GameObject> kamikadzePool;
    [SerializeField] GameObject kamikadzePrefab;
    public ObjectPool<GameObject> burnerPool;
    [SerializeField] GameObject burnerPrefab;
    public ObjectPool<GameObject> moscitoPool;
    [SerializeField] GameObject moscitoPrefab;
    public ObjectPool<GameObject> destroyerPool;
    [SerializeField] GameObject destroyerPrefab;
    public ObjectPool<GameObject> mommyPool;
    [SerializeField] GameObject mommyPrefab;
    public ObjectPool<GameObject> turelPool;
    [SerializeField] GameObject turelPrefab;
    public ObjectPool<GameObject> daddyPool;
    [SerializeField] GameObject daddyPrefab;


    void Awake()
    {
        gunBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(gunBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        rocketBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(rocketBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 64, maxSize: 128);
        blasterBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(blasterBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        splashBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(splashBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);

        flyBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(flyBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        destroyerBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(destroyerBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        mommyBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(mommyBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        daddyBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(daddyBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);

        flyPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(flyPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        kamikadzePool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(kamikadzePrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 64, maxSize: 128);
        burnerPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(burnerPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        moscitoPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(moscitoPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        destroyerPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(destroyerPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        mommyPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(mommyPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        turelPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(turelPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        daddyPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(daddyPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);

    }

}
