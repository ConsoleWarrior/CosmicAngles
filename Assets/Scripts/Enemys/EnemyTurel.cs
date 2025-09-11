using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyTurel : Enemy
{
    private bool flag = false;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    [SerializeField] float repairReloadTime;
    [SerializeField] float repairVolume;
    ObjectPool<GameObject> gunBulletPool;


    void Start()
    {
        animator = GetComponent<Animator>();
        target = transform.position;
        audioManager.a.volume = 0.3f;
        //gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().flyBulletPool;
        Invoke("Initialize",0.5f);
        StartCoroutine(RepairTic());
    }
    void Initialize()
    {
        gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().flyBulletPool;
        if (gunBulletPool == null) Debug.Log("gunBulletPool == null");
    }
    void FixedUpdate()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        dist = Vector3.Distance(player.position, transform.position);
        if (dist != 0 && dist < atackDistance)
            Atack();
        else
            if(target != transform.position) Walking();
    }
    public override void Walking()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector2 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
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
            var blt = bullet.GetComponent<Bullet>();
            blt.damage = damage;
            blt.gunBulletPool = gunBulletPool;
            blt.ReturnToPool(5);
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.localScale = Vector3.one;
            bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            //bullet.GetComponent<Bullet>().damage = damage;
            //Destroy(bullet, 2f);
            yield return new WaitForSeconds(reloadTime);
        }
        flag = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            other.gameObject.GetComponent<Cell>().TakeDamage(currentHp);
        }
    }
    IEnumerator RepairTic()
    {
        while (true)
        {
            currentHp += repairVolume;
            if (currentHp > maxHp) currentHp = maxHp;
            yield return new WaitForSeconds(repairReloadTime);
        }
    }
}
