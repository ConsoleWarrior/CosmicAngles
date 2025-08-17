using UnityEngine;

public class BlasterEnemy : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;

    public void Fire(Transform player)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().damage = damage;
        Destroy(bullet, 2f);
    }
}
