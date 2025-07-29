using UnityEngine;

public class GunBullet : Bullet
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
