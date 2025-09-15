using UnityEngine;

public class SplashBullet : Bullet
{
    int flag = 0;
    public float speed;
    [SerializeField] Transform sprite;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (flag == 0)
            {
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                StopAllCoroutines();
                sprite.localScale = new Vector2(2.5f,2.5f);
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
        sprite.localScale = new Vector2(0.01f, 0.01f);

        //gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0.32f);
        gunBulletPool.Release(this.gameObject);
    }
    void Update()
    {
        //if (targetEnemy.gameObject.activeSelf && !flag) Atack(targetEnemy);
        //else //Destroy(this.gameObject);
        //{
        //    flag = true;
        //    transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);

        //    //StopAllCoroutines();
        //    //gunBulletPool.Release(this.gameObject);
        //}
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);

    }
    private void Atack(Transform enemy)
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
        Vector2 direction = (enemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
