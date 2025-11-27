using System.Collections;
using UnityEngine;

public class RocketGun : Guns
{
    void Start()
    {
        audioManager.a.volume = 0.15f;
        gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().rocketBulletPool;
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
            sprite.rotation = Quaternion.Euler(0, 0, angle - 90);// поворот гана

            audioManager.SoundPlay0();
            var bullet = gunBulletPool.Get();
            bullet.transform.position = mouth.position;
            bullet.transform.rotation = transform.rotation;
            var blt = bullet.GetComponent<RocketBullet>();
            blt.damage = damage;
            blt.speed = bulletSpeed;
            blt.targetEnemy = target;
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(5);
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bullet.GetComponent<RocketBullet>().damage = damage;
            //bullet.GetComponent<RocketBullet>().speed = bulletSpeed;
            //bullet.GetComponent<RocketBullet>().targetEnemy = target;
            //bullet.GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse); // Придать пуле скорость
            //Destroy(bullet, 5f);
            yield return new WaitForSeconds(reload);
        }
        flag = false;
    }
}
