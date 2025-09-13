using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyBurner : Enemy
{
    private bool fireFlag = false;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    ObjectPool<GameObject> gunBulletPool;

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
        enemyPool = managerObj.burnerPool;

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
            audioManager.SoundPlay1();
            var bullet = gunBulletPool.Get();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            var blt = bullet.GetComponent<Bullet>();
            blt.damage = damage;
            blt.gunBulletPool = gunBulletPool;
            bullet.transform.localScale = new(0.15f, 0.15f, 1);
            blt.ReturnToPool(2);
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

        damage = 1.5f;
        maxHp = 150;
        currentHp = 150;
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        reloadTime = 0.15f;
        dropScrapCount = 4;
    }
    public override void Level1()
    {
        fireFlag = false;

        damage = 2f;
        maxHp = 200;
        currentHp = 200;
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
        reloadTime = 0.125f;
        dropScrapCount = 6;
    }
    public override void Level2()
    {
        fireFlag = false;

        damage = 2.5f;
        maxHp = 250;
        currentHp = 250;
        transform.localScale = new Vector3(0.9f, 0.9f, 1);
        reloadTime = 0.1f;
        dropScrapCount = 8;
    }
}