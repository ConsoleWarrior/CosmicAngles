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
    private void Start()
    {
        gunBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(gunBulletPrefab); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);
        rocketBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(rocketBulletPrefab); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 64, maxSize: 128);
        blasterBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(blasterBulletPrefab); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);

    }

}
