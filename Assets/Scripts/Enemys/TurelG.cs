using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class TurelG : MonoBehaviour
{
    protected Transform player;


    private bool fireFlag = false;
    //[SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    ObjectPool<GameObject> gunBulletPool;
    protected float dist;
    [SerializeField] float atackDistance;

    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { Debug.Log("player is not active"); }//Debug.Log("player is not active"); }
        ////animator = GetComponent<Animator>();
        //sector = transform.parent.GetComponent<MapSector>();
        //target = new Vector3(Random.Range(sector.minX, sector.maxX), Random.Range(sector.minY, sector.maxY), 0);
        //audioManager.a.volume = 0.5f;
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        gunBulletPool = managerObj.flyBulletPool;
        //enemyPool = managerObj.burnerPool;
        //animPool = managerObj.destroyAnimPool;

    }
    void FixedUpdate()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        dist = Vector3.Distance(player.position, transform.position);
        if (!fireFlag)
        {
            if (dist > 0 && dist < atackDistance)
            {
                fireFlag = true;
                Atack();
            }
        }
        else if (dist > atackDistance)
        {
            fireFlag = false;
            StopAllCoroutines();
        }
        //else
        //{
        //    fireFlag = false;
        //    StopAllCoroutines();
        //}
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Cell"))
    //    {
    //        if (!fireFlag)
    //        {
    //            fireFlag = true;
    //            Atack();
    //        }
    //    }
    //}

    public void Atack()
    {
        //if (!fireFlag && dist < fireDistance)
        //{
        //fireFlag = true;
        StartCoroutine(Fire());
        //}

        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        //hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            //audioManager.SoundPlay1();
            var bullet = gunBulletPool.Get();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            var blt = bullet.GetComponent<Bullet>();
            blt.damage = damage;
            blt.gunBulletPool = gunBulletPool;
            bullet.transform.localScale = new(0.15f, 0.15f, 1);
            blt.ReturnToPool(2);
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            //bullet.GetComponent<Bullet>().damage = damage;
            //Destroy(bullet, 2f);
            yield return new WaitForSeconds(reloadTime);
        }
        fireFlag = false;
    }
}