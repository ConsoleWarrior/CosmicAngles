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

    public ObjectPool<GameObject> flyBulletPool;
    [SerializeField] GameObject flyBulletPrefab;
    public ObjectPool<GameObject> destroyerBulletPool;
    [SerializeField] GameObject destroyerBulletPrefab;
    public ObjectPool<GameObject> mommyBulletPool;
    [SerializeField] GameObject mommyBulletPrefab;

    private void Start()
    {
        gunBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(gunBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        rocketBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(rocketBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 64, maxSize: 128);
        blasterBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(blasterBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);

        flyBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(flyBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        destroyerBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(destroyerBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        mommyBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(mommyBulletPrefab); obj.transform.SetParent(transform); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);

    }

}
