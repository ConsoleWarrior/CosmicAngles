using System.Collections;
using UnityEngine;

public class SplashRocketGun : Guns
{

    void Start()
    {
        audioManager.a.volume = 0.15f;
        gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().splashBulletPool;
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

            //Vector2 direction = (target.position - transform.position).normalized;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //sprite.rotation = Quaternion.Euler(0, 0, angle - 90);

            audioManager.SoundPlay0();
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            var bullet = gunBulletPool.Get();
            //bullet.transform.SetParent(transform);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            var blt = bullet.GetComponent<SplashBullet>();
            blt.damage = damage;
            blt.speed = bulletSpeed;
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(4);
            //var blt = bullet.GetComponent<Bullet>();
            //blt.damage = damage;
            //blt.gunBulletPool = gunBulletPool;
            //blt.ReturnToPool(2);
            //bullet.GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * bulletSpeed*50, ForceMode2D.Force); // Придать пуле скорость
            //Destroy(bullet, 2f);
            yield return new WaitForSeconds(reload);
        }
        flag = false;
    }
}
