using UnityEngine;


public class EnemyMoscito : Enemy
{
    public bool flag = false;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    [SerializeField] LazerGunEnemy gun;

    //void Start()
    //{
    //    try
    //    {
    //        player = GameObject.FindGameObjectWithTag("Player").transform;
    //    }
    //    catch { Debug.Log("player is not active"); }
    //    animator = GetComponent<Animator>();
    //    target = new Vector3(Random.Range(-200, -100), Random.Range(-100, 100), 0);
    //    audioManager.a.volume = 0.5f;
    //}
    public override void Atack()
    {
        if (!flag && dist < 8)
        {
            flag = true;
        }
        else if (dist >= 8)
        {
            flag = false;
            gun.coroIsWorkNow = false;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void Evolve()
    {
        maxHp += maxHp * 0.3f;
        currentHp += currentHp * 0.3f;
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
        //speed += speed * 0.2f;
        reloadTime -= reloadTime * 0.2f;
        dropScrapCount = 10;
    }
    public override void EvolveLevel2()
    {
        maxHp += maxHp * 0.2f;
        currentHp += currentHp * 0.2f;
        transform.localScale = new Vector3(1, 1, 1);
        speed += speed * 0.1f;
        reloadTime -= reloadTime * 0.2f;
        dropScrapCount = 12;
    }
    //public override void Eating()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    //    Vector2 direction = (target - transform.position).normalized;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    //    hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);

    //    if (transform.position == target)
    //    {
    //        target = new Vector3(Random.Range(-200, -100), Random.Range(-100, 100), 0);
    //    }
    //}
}