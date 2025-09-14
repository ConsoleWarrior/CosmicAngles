using UnityEngine;

public class SplashBullet : Bullet
{
    int flag = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (flag == 0)
            {
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                StopAllCoroutines();
                gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(12,12);
                Invoke("Return", 0.2f);
            }
            else
            {
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }
            //var enemy = other.gameObject.GetComponent<Enemy>();
            //enemy.TakeDamage(damage);
            //Destroy(this.gameObject);
            //inFly = false;
            //StopAllCoroutines();
            //gunBulletPool.Release(this.gameObject);
        }
    }
    void Return()
    {
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.19f, 0.46f);
        //gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0.32f);
        gunBulletPool.Release(this.gameObject);
    }
}
