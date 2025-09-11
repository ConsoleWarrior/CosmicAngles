using UnityEngine;
using UnityEngine.Pool;

public class BlasterEnemy : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;
    ObjectPool<GameObject> gunBulletPool;

    void Start()
    {
        gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().destroyerBulletPool;
    }
    public void Fire(Transform player)
    {
        var bullet = gunBulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        var blt = bullet.GetComponent<Bullet>();
        blt.damage = damage;
        blt.gunBulletPool = gunBulletPool;
        blt.ReturnToPool(2);
        //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
        //bullet.GetComponent<Bullet>().damage = damage;
        //Destroy(bullet, 2f);
    }
}
