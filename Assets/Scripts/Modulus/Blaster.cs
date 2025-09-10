using System.Collections;
using UnityEngine;

public class Blaster : Guns
{
    [SerializeField] Transform gunBarrel1;
    [SerializeField] Transform gunBarrel2;

    void Start()
    {
        audioManager.a.volume = 0.15f;
        gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().blasterBulletPool;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.transform);
            if (targets.Count == 1 && !flag)
            {
                StartCoroutine("Fire");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Remove(other.transform);
        }
    }
    IEnumerator Fire()
    {
        flag = true;
        while (targets.Count > 0)
        {
            var dist = Vector3.Distance(targets[0].position, transform.position);
            target = targets[0];
            if (targets.Count > 1)
            {
                foreach (var item in targets)
                {
                    if (Vector3.Distance(item.position, transform.position) < dist)
                    {
                        target = item;
                    }
                }
            }

            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sprite.rotation = Quaternion.Euler(0, 0, angle - 90);

            audioManager.SoundPlay0();
            InstantiateBullet(gunBarrel1);
            InstantiateBullet(gunBarrel2);
            yield return new WaitForSeconds(reload);
        }
        flag = false;
    }

    private void InstantiateBullet(Transform gunBarrel)
    {
        var bullet = gunBulletPool.Get();
        bullet.transform.position = gunBarrel.position;
        bullet.transform.rotation = gunBarrel.rotation;
        var blt = bullet.GetComponent<Bullet>();
        blt.damage = damage;
        blt.gunBulletPool = gunBulletPool;
        blt.ReturnToPool(2);
        //GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        //bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce((target.position - gunBarrel.position).normalized * bulletSpeed, ForceMode2D.Impulse); // Придать пуле скорость
        //Destroy(bullet, 2f);
    }
}
