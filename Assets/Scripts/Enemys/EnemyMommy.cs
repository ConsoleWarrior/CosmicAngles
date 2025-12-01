using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyMommy : Enemy
{
    private bool fireFlag = false;
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
        sector = transform.parent.GetComponent<MapSector>();
        target = new Vector3(Random.Range(sector.minX, sector.maxX), Random.Range(sector.minY, sector.maxY), 0);
        audioManager.a.volume = 0.5f;
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        gunBulletPool = managerObj.mommyBulletPool;
        enemyPool = managerObj.mommyPool;
        animPool = managerObj.destroyAnimPool;

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

            var blt = bullet.GetComponent<MommyRocket>();
            blt.SetParameters(damage);
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(3.5f);
            yield return new WaitForSeconds(reloadTime);
        }
        fireFlag = false;
    }
    public override void Level0()
    {
        fireFlag = false;

        damage = 30f;
        maxHp = 175;
        currentHp = 175;
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        reloadTime = 3f;
        dropScrapCount = 6;
    }
    public override void Level1()
    {
        fireFlag = false;

        damage = 40f;
        maxHp = 225;
        currentHp = 225;
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
        reloadTime = 2.8f;
        dropScrapCount = 8;
    }
    public override void Level2()
    {
        fireFlag = false;

        damage = 50f;
        maxHp = 275;
        currentHp = 275;
        transform.localScale = new Vector3(0.9f, 0.9f, 1);
        reloadTime = 2.6f;
        dropScrapCount = 10;
    }
}