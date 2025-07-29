using System.Collections;
using UnityEngine;


public class EnemyFly : Enemy
{
    private bool flag = false;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;

    public override void Atack()
    {
        if (!flag && dist < 7)
        {
            flag = true;
            StartCoroutine(Fire());
        }
        else if (dist >= 7)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator Fire()
    {
        while (dist < 7)
        {
            //audioManager.SoundPlay1();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().damage = damage;
            Destroy(bullet, 2f);
            yield return new WaitForSeconds(reloadTime);
        }
        flag = false;
    }
    public override void Evolve()
    {
        maxHp += maxHp * 0.3f;
        currentHp += currentHp * 0.3f;
        transform.localScale = new Vector3(2, 2, 2);
        speed -= speed * 0.3f;
        reloadTime -= reloadTime * 0.3f;
        dropScrapCount = 4;
    }
    public override void EvolveLevel2()
    {
        maxHp += maxHp * 0.3f;
        currentHp += currentHp * 0.3f;
        transform.localScale = new Vector3(2.5f, 2.5f, 1);
        speed -= speed * 0.2f;
        reloadTime -= reloadTime * 0.3f;
        dropScrapCount = 8;
    }
    public override void CalculateAndCallDrop()
    {
        if (xp > 8 && Random.Range(0, 25) == 0)
        {
            Instantiate(Resources.Load("DropItems/DropGunBlue", typeof(GameObject)), new(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity);
        }
        GameObject c = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), transform.position, Quaternion.identity);
        c.GetComponent<Scrap>().value = Random.Range(dropScrapCount / 2, dropScrapCount * 2);
        c.transform.localScale = new Vector3(0.6f, 0.6f, 1);
    }
}
