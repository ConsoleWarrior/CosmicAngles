using UnityEngine;

public class EnemyKamikadze : Enemy
{

    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { Debug.Log("player is not active"); }//Debug.Log("player is not active"); }
        animator = GetComponent<Animator>();
        if (transform.parent != null)
        {
            sector = transform.parent.GetComponent<MapSector>();
            target = new Vector3(Random.Range(sector.minX, sector.maxX), Random.Range(sector.minY, sector.maxY), 0);
        }
        audioManager.a.volume = 0.5f;
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        enemyPool = managerObj.kamikadzePool;
    }
    public override void Atack()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public override void Level0()
    {
        maxHp = 100;
        currentHp = 100;
        speed = 2;
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        dropScrapCount = 2;
    }
    public override void Level1()
    {
        maxHp = 125;
        currentHp = 125;
        speed = 2.5f;
        transform.localScale = new Vector3(1f, 1f, 1);
        dropScrapCount = 4;
    }
    public override void Level2()
    {
        maxHp = 150;
        currentHp = 150;
        speed = 3;
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        dropScrapCount = 6;
    }
}
