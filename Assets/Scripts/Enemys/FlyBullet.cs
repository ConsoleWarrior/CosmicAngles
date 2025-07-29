using UnityEngine;

public class FlyBullet : Bullet
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cell"))
        {
            var cell = other.gameObject.GetComponent<Cell>();
            cell.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Shield"))
        {
            var shield = other.gameObject.GetComponent<Shield>();
            shield.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
