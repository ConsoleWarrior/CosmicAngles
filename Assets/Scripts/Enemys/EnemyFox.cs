using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyFox : Enemy
{
    private bool fireFlag = false;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    ObjectPool<GameObject> gunBulletPool;
    bool foxFlag = false;
    Vector3 poz;

    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { Debug.Log("player is not active"); }//Debug.Log("player is not active"); }
        sector = transform.parent.GetComponent<MapSector>();
        target = new Vector3(Random.Range(sector.minX, sector.maxX), Random.Range(sector.minY, sector.maxY), 0);
        audioManager.a.volume = 0.05f;
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        gunBulletPool = managerObj.flyBulletPool;
        enemyPool = managerObj.foxPool;
        animPool = managerObj.destroyAnimPool;
    }

    public override void Atack()
    {

        if (!fireFlag && dist < fireDistance && !foxFlag)
        {
            fireFlag = true;
            StartCoroutine(Fire());
        }
        else if (foxFlag)
        {
            transform.position = Vector3.MoveTowards(transform.position, poz, speed * Time.deltaTime);
            var direction1 = (poz - transform.position).normalized;
            float angle1 = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle1 - 90);
            hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (transform.position == poz)
            {
                foxFlag = false;
            }
            return;
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
        var count = 0;
        while (dist < fireDistance & count < 20)
        {
            audioManager.SoundPlay1();
            var bullet = gunBulletPool.Get();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            var blt = bullet.GetComponent<Bullet>();
            blt.damage = damage;
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(2);
            bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            count++;
            yield return new WaitForSeconds(reloadTime);
        }
        poz = new(transform.position.x + Random.Range(-3, 4), transform.position.y + Random.Range(-3, 4));
        fireFlag = false;
        foxFlag = true;
    }

    public override void Level0()
    {
        fireFlag = false;

        damage = 2.5f;
        maxHp = 500;
        currentHp = 500;
        transform.localScale = new Vector3(0.6f, 0.6f, 1);
        dropScrapCount = 14;
    }
    public override void Level1()
    {
        fireFlag = false;

        damage = 3f;
        maxHp = 750;
        currentHp = 750;
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        dropScrapCount = 16;
    }
    public override void Level2()
    {
        fireFlag = false;

        damage = 3.5f;
        maxHp = 1000;
        currentHp = 1000;
        transform.localScale = new Vector3(1f, 1f, 1);
        dropScrapCount = 18;
    }
}