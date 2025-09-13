using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyFly : Enemy
{
    bool fireFlag = false;
    //[SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    ObjectPool<GameObject> gunBulletPool;
    //ObjectPool<GameObject> enemyPool;


    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { Debug.Log("player is not active"); }//Debug.Log("player is not active"); }
        animator = GetComponent<Animator>();
        sector = transform.parent.GetComponent<MapSector>();
        target = new Vector3(Random.Range(sector.minX, sector.maxX), Random.Range(sector.minY, sector.maxY), 0);
        audioManager.a.volume = 0.5f;
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        gunBulletPool = managerObj.flyBulletPool;
        enemyPool = managerObj.flyPool;

    }
    public override void Atack()
    {
        if (!fireFlag && dist < fireDistance)
        {
            fireFlag = true;
            StartCoroutine(Fire());
        }
        else if (dist >= fireDistance)
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
        while (dist < fireDistance)
        {
            //audioManager.SoundPlay1();
            var bullet = gunBulletPool.Get();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            var blt = bullet.GetComponent<Bullet>();
            blt.damage = damage;
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(2);
            bullet.transform.localScale = new(0.15f, 0.15f, 1);
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            //bullet.GetComponent<Bullet>().damage = damage;
            //Destroy(bullet, 2f);
            yield return new WaitForSeconds(reloadTime);
        }
        fireFlag = false;
    }
    public override void Level0()
    {
        fireFlag = false;

        damage = 5;
        maxHp = 120;
        currentHp = 120;
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        reloadTime = 1;
        dropScrapCount = 2;
    }
    public override void Level1()
    {
        fireFlag = false;

        damage = 7;
        maxHp = 150;
        currentHp = 150;
        transform.localScale = new Vector3(1.7f, 1.7f, 1);
        reloadTime = 0.8f;
        dropScrapCount = 4;
    }
    public override void Level2()
    {
        fireFlag = false;

        damage = 9;
        maxHp = 180;
        currentHp = 180;
        transform.localScale = new Vector3(2f, 2f, 1);
        reloadTime = 0.7f;
        dropScrapCount = 6;
    }
}
