using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MommyRocket : Enemy
{

    public ObjectPool<GameObject> gunBulletPool;

    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { Debug.Log("player is not active"); }//Debug.Log("player is not active"); }
                                                    //animator = GetComponent<Animator>();
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        animPool = managerObj.destroyAnimPool;
        audioManager.a.volume = 0.5f;
    }
    void Update()
    {
        if (currentHp <= 0)// && !animator.GetBool("Destroy")
        {
            //animator.SetBool("Destroy", true);
            //audioManager.a.volume = 1;
            //audioManager.SoundPlay0();
            //transform.GetComponent<CircleCollider2D>().enabled = false;
            StopAllCoroutines();
            var obj = animPool.Get();
            obj.transform.position = transform.position;
            obj.GetComponent<AnimDestroy>().animPool = animPool;
            gunBulletPool.Release(this.gameObject);
            //ReturnToPool(0.3f);
        }
    }
    void FixedUpdate()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        Atack();
    }

    public override void Atack()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
    public void ReturnToPool(float time)
    {
        StartCoroutine(ReturnCoro(time));
    }
    IEnumerator ReturnCoro(float time)
    {
        yield return new WaitForSeconds(time);
        gunBulletPool.Release(this.gameObject);
        //Debug.Log(gameObject.name + " = " + gunBulletPool.CountAll);
    }
    public void SetParameters(float damage)//для снаряда мамки
    {
        currentHp = damage;
    }
    public override void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    //if (other.gameObject.CompareTag("Scrap"))
    //    //{
    //    //    float value = other.gameObject.GetComponent<Scrap>().value;
    //    //    xp += value;
    //    //}
    //    if (other.gameObject.CompareTag("Cell"))
    //    {
    //        other.gameObject.GetComponent<Cell>().TakeDamage(currentHp);
    //        currentHp = 0;
    //    }
    //}
}
