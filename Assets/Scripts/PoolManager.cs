using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public ObjectPool<GameObject> gunBulletPool;
    [SerializeField] GameObject gunBulletPrefab;

    private void Start()
    {
        gunBulletPool = new ObjectPool<GameObject>(createFunc: () => { var obj = Instantiate(gunBulletPrefab); obj.SetActive(false); return obj; }, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 256, maxSize: 512);

    }

    //var _pool = new ObjectPool<GameObject>(createFunc: () => new GameObject("PooledObject"), actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionChecks: false, defaultCapacity: 10, maxPoolSize: 10);

}
