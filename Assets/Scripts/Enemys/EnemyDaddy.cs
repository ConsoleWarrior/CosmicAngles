using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyDaddy : Enemy
{
    private bool fireFlag = false;
    //[SerializeField] GameObject bulletPrefab;
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
        gunBulletPool = managerObj.daddyBulletPool;
        enemyPool = managerObj.daddyPool;

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
            bullet.transform.GetComponent<CircleCollider2D>().enabled = true;

            //bullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            var blt = bullet.GetComponent<DaddyRocket>();
            blt.SetParameters(damage);
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(3.5f);

            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            //Destroy(bullet, 3.5f);
            yield return new WaitForSeconds(reloadTime);
        }
        fireFlag = false;
    }
    public override void Level0()
    {
        fireFlag = false;

        damage = 29f;
        maxHp = 400;
        currentHp = 400;
        transform.localScale = new Vector3(0.6f, 0.6f, 1);
        reloadTime = 3f;
        dropScrapCount = 12;
    }
    public override void Level1()
    {
        fireFlag = false;

        damage = 38f;
        maxHp = 550;
        currentHp = 550;
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        reloadTime = 2.8f;
        dropScrapCount = 14;
    }
    public override void Level2()
    {
        fireFlag = false;

        damage = 47f;
        maxHp = 700;
        currentHp = 700;
        transform.localScale = new Vector3(1f, 1f, 1);
        reloadTime = 2.6f;
        dropScrapCount = 16;
    }
}