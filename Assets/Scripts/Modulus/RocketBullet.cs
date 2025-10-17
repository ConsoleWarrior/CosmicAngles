using System.Collections;
using UnityEngine;

public class RocketBullet : Bullet
{
    public float speed;
    public Transform targetEnemy;
    public bool flag;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            StopAllCoroutines();
            flag = false;
            gunBulletPool.Release(this.gameObject);
            //Destroy(this.gameObject);
        }
    }
    void Update()
    {
        if(targetEnemy.gameObject.activeSelf && !flag) Atack(targetEnemy);
        else //Destroy(this.gameObject);
        {
            flag = true;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);

            //StopAllCoroutines();
            //gunBulletPool.Release(this.gameObject);
        }
    }
    private void Atack(Transform enemy)
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
        Vector2 direction = (enemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
    public override void ReturnToPool(float time)
    {
        StartCoroutine(ReturnCoro(time));
    }
    IEnumerator ReturnCoro(float time)
    {
        yield return new WaitForSeconds(time);
        flag = false;
        gunBulletPool.Release(this.gameObject);
        //Debug.Log(gameObject.name + " = " + gunBulletPool.CountAll);
    }
}
