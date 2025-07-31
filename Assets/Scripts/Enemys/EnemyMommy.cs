using System.Collections;
using UnityEngine;


public class EnemyMommy : Enemy
{
    private bool flag = false;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;

    //void Start()
    //{
    //    try
    //    {
    //        player = GameObject.FindGameObjectWithTag("Player").transform;
    //    }
    //    catch { Debug.Log("player is not active"); }
    //    animator = GetComponent<Animator>();
    //    target = new Vector3(Random.Range(-100, 100), Random.Range(-200, -100), 0);
    //    audioManager.a.volume = 0.5f;
    //}
    public override void Atack()
    {
        if (!flag && dist < 9)
        {
            flag = true;
            StartCoroutine(Fire());
        }
        else if (dist >= 9)
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
        while (dist < 9)
        {

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            bullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            bullet.GetComponent<Enemy>().SetParameters(damage);
            Destroy(bullet, 3.5f);
            yield return new WaitForSeconds(reloadTime);
        }
        flag = false;
    }
    public override void Evolve()
    {
        maxHp += maxHp * 0.3f;
        currentHp += currentHp * 0.3f;
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
        speed += speed * 0.1f;
        reloadTime -= reloadTime * 0.2f;
        dropScrapCount = 8;
        damage += damage * 0.3f; 
    }
    public override void EvolveLevel2()
    {
        maxHp += maxHp * 0.3f;
        currentHp += currentHp * 0.3f;
        transform.localScale = new Vector3(1, 1, 1);
        speed += speed * 0.1f;
        reloadTime -= reloadTime * 0.2f;
        dropScrapCount = 12;
        damage += damage * 0.3f;
    }
    //public override void Eating()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    //    Vector2 direction = (target - transform.position).normalized;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    //    hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);

    //    if (transform.position == target)
    //    {
    //        target = new Vector3(Random.Range(-100, 100), Random.Range(-200, -100), 0);
    //    }
    //}
    //public override void CalculateAndCallDrop()
    //{
    //    if (xp > 8 && Random.Range(0, 35) == 0)
    //    {
    //        Instantiate(Resources.Load("DropItems/DropMachineGunGreen", typeof(GameObject)), new(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity);
    //    }
    //    if (xp > 8 && Random.Range(0, 30) == 0)
    //    {
    //        Instantiate(Resources.Load("DropItems/DropRocketGun", typeof(GameObject)), new(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity);
    //    }
    //    GameObject c = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), transform.position, Quaternion.identity);
    //    c.GetComponent<Scrap>().value = Random.Range(dropScrapCount / 2, dropScrapCount * 2);
    //    c.transform.localScale = new Vector3(0.6f, 0.6f, 1);
    //}
}