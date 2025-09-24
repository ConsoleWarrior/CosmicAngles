using UnityEngine;

public class SplashBullet : Bullet
{
    int flag = 0;
    public float speed;
    [SerializeField] Transform sprite;


    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (flag == 0)
            {
                flag++;
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                StopAllCoroutines();
                gameObject.GetComponent<ParticleSystem>().Play();
                gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(12, 12);
                Invoke("ResetAndReturn", 0.2f);
            }
            else
            {
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }
        }
    }
    void ResetAndReturn()
    {
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(2.48f, 2.48f);
        flag = 0;
        gunBulletPool.Release(this.gameObject);
    }
}
