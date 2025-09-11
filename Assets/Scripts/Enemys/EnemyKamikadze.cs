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
    }
    public override void Atack()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public override void Evolve()
    {
        maxHp += maxHp * 0.3f;
        currentHp += currentHp * 0.3f;
        transform.localScale = new Vector3(1, 1, 1);
        speed += speed * 0.3f;
        dropScrapCount = 4;
    }
    public override void EvolveLevel2()
    {
        maxHp += maxHp * 0.3f;
        currentHp += currentHp * 0.3f;
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        //speed += speed * 0.1f;
        dropScrapCount = 6;
    }
}
