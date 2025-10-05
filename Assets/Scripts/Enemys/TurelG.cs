using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class TurelG : MonoBehaviour
{
    protected Transform player;


    private bool fireFlag = false;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    [SerializeField] float damage;
    ObjectPool<GameObject> gunBulletPool;
    protected float dist;
    [SerializeField] float atackDistance;

    void Start()
    {
        //try
        //{
        //    player = GameObject.FindGameObjectWithTag("Player").transform;
        //}
        //catch { Debug.Log("player is not active"); }//Debug.Log("player is not active"); }
        var managerObj = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        gunBulletPool = managerObj.flyBulletPool;
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
                //Atack();
                StartCoroutine(Fire());

            }
        }
        else if (dist > atackDistance)
        {
            fireFlag = false;
            StopAllCoroutines();
        }
    }

    public void Atack()
    {
        StartCoroutine(Fire());
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            //audioManager.SoundPlay1();
            var bullet = gunBulletPool.Get();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            var blt = bullet.GetComponent<Bullet>();
            blt.damage = damage;
            blt.gunBulletPool = gunBulletPool;
            bullet.transform.localScale = new(0.15f, 0.15f, 1);
            blt.ReturnToPool(2);
            bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(reloadTime);
        }
    }
}