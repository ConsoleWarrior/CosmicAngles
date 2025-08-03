using System.Collections;
using UnityEngine;


public class EnemyTurel : Enemy
{
    private bool flag = false;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    [SerializeField] float repairReloadTime;
    [SerializeField] float repairVolume;


    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { Debug.Log("player is not active"); }
        animator = GetComponent<Animator>();
        target = transform.position;
        audioManager.a.volume = 0.3f;
        StartRepair();
    }
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }
        else dist = Vector3.Distance(player.position, transform.position);
        if (dist != 0 && dist < 13)
            Atack();
        else
            if(target != transform.position) Eating();
    }
    public override void Eating()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector2 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public override void Atack()
    {
        if (!flag && dist < 10)
        {
            flag = true;
            StartCoroutine(Fire());
        }
        else if (dist >= 10)
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
        while (dist < 10)
        {
            audioManager.SoundPlay1();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.localScale = Vector3.one;
            bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().damage = damage;
            Destroy(bullet, 2f);
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
    public void StartRepair()
    {
        StartCoroutine(RepairTic());
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
