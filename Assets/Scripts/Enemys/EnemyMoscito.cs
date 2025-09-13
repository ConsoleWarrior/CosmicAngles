using UnityEngine;


public class EnemyMoscito : Enemy
{
    public bool fireFlag = false;
    [SerializeField] LazerGunEnemy gun;


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
        enemyPool = managerObj.moscitoPool;

    }
    //void Update()
    //{
    //    if (currentHp <= 0 && !destroyFlag)
    //    {
    //        destroyFlag = true;
    //        animator.SetBool("Destroy", true);
    //        audioManager.a.volume = 1;
    //        audioManager.SoundPlay0();
    //        //transform.GetComponent<CircleCollider2D>().enabled = false;
    //        CalculateAndCallDrop();
    //        Invoke("Destroying", 0.5f);

    //        //Destroy(this.gameObject, 0.5f);
    //    }
    //}
    public override void Atack()
    {
        if (!fireFlag && dist < fireDistance)
        {
            fireFlag = true;
        }
        else if (dist >= fireDistance)
        {
            fireFlag = false;
            gun.coroIsWorkNow = false;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void Level0()
    {
        fireFlag = false;
        gun.damage = 2f;
        maxHp = 220;
        currentHp = 220;
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        dropScrapCount = 8;
    }
    public override void Level1()
    {
        fireFlag = false;
        gun.damage = 2.5f;
        maxHp = 260;
        currentHp = 260;
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
        dropScrapCount = 10;
    }
    public override void Level2()
    {
        fireFlag = false;
        gun.damage = 3f;
        maxHp = 320;
        currentHp = 320;
        transform.localScale = new Vector3(0.9f, 0.9f, 1);
        dropScrapCount = 12;
    }
}