using System.Collections;
using UnityEngine;


public class EnemyDestroyer : Enemy
{
    public bool fireFlag = false;
    [SerializeField] float reloadTime;
    //[SerializeField] float damage;
    [SerializeField] BlasterEnemy gun1;
    [SerializeField] BlasterEnemy gun2;


    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { Debug.Log("player is not active"); }//Debug.Log("player is not active"); }
        //animator = GetComponent<Animator>();
        sector = transform.parent.GetComponent<MapSector>();
        target = new Vector3(Random.Range(sector.minX, sector.maxX), Random.Range(sector.minY, sector.maxY), 0);
        audioManager.a.volume = 0.5f;
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        enemyPool = managerObj.destroyerPool;
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
            gun1.Fire(player);
            gun2.Fire(player);
            yield return new WaitForSeconds(reloadTime);
        }
        fireFlag = false;
    }
    public override void Level0()
    {
        fireFlag = false;
        gun1.damage = 3f;
        gun2.damage = 3f;
        maxHp = 300;
        currentHp = 300;
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        dropScrapCount = 10;
    }
    public override void Level1()
    {
        fireFlag = false;
        gun1.damage = 4f;
        gun2.damage = 4f;
        maxHp = 400;
        currentHp = 400;
        transform.localScale = new Vector3(1f, 1f, 1);
        dropScrapCount = 12;
    }
    public override void Level2()
    {
        fireFlag = false;
        gun1.damage = 5f;
        gun2.damage = 5f;
        maxHp = 500;
        currentHp = 500;
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        dropScrapCount = 14;
    }
}