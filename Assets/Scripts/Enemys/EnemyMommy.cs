using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyMommy : Enemy
{
    private bool flag = false;
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
        gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().mommyBulletPool;

        Invoke("Evolve", 180);
        Invoke("EvolveLevel2", 480);
    }
    public override void Atack()
    {
        if (!flag && dist < fireDistance)
        {
            flag = true;
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
            var blt = bullet.GetComponent<MommyRocket>();
            blt.SetParameters(damage);
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(3.5f);

            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            //Destroy(bullet, 3.5f);
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
}