using UnityEngine;

public class RocketBullet : Bullet
{
    [SerializeField] Animator animator;
    public float speed;
    public Transform targetEnemy;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        if(targetEnemy != null) Atack(targetEnemy);
        else Destroy(this.gameObject);
    }
    private void Atack(Transform enemy)
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
        Vector2 direction = (enemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
