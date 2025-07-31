using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGunEnemy : MonoBehaviour
{
    public EnemyMoscito enemy;
    public float range;
    public LineRenderer line;
    //Transform lineEndPoint;
    //Vector2 lineDefaultPoint;
    //public ParticleSystem LLHitEffectLeft;
    public ParticleSystem LLHitEffect;
    //public Collider2D targetCollider;
    [SerializeField] protected AudioManager audioManager;
    //protected bool flag = false;
    [SerializeField] protected float damage;
    //protected Transform target;
    //protected List<Transform> targets = new();
    [SerializeField] protected float reload;
    Transform temp;
    bool coroIsWorkNow = false;

    void Start()
    {
        audioManager.a.volume = 0.15f;
        enemy = transform.parent.GetComponent<EnemyMoscito>();
    }

    void FixedUpdate()
    {
        line.SetPosition(0, transform.position);

        if (enemy.flag)
        {
            StartFire();
            //Vector2 direction = (target.position - transform.position).normalized;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ////sprite.rotation = Quaternion.Euler(0, 0, angle - 90);
            ////audioManager.SoundPlay2();
            //line.SetPosition(1, target.position);
            //LLHitEffect.transform.position = new Vector3(target.position.x, target.position.y, -1);
            //if (!LLHitEffect.isPlaying) LLHitEffect.Play();
        }
        else
        {
            line.SetPosition(1, transform.position);
            LLHitEffect.transform.position = transform.position;
            LLHitEffect.Pause();
        }
    }
    IEnumerator Fire(Transform target)
    {
        //flag = true;
        Debug.Log("start coro");
        while (enemy.flag)
        {
            Debug.Log("coro working now");
            coroIsWorkNow = true;

            //var dist = Vector3.Distance(targets[0].position, transform.position);
            //target = targets[0];
            //if (targets.Count > 1)
            //{
            //    foreach (var item in targets)
            //    {
            //        if (Vector3.Distance(item.position, transform.position) < dist) //проверяем ближайшую цель
            //        {
            //            target = item;
            //        }
            //    }
            //}

            //Vector2 direction = (target.position - transform.position).normalized;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //sprite.rotation = Quaternion.Euler(0, 0, angle - 90);

            //RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up, range);
            //if (raycastHit)
            //{
            //    if (!flag)
            //    {
            //        var b = raycastHit.transform;
            //        Debug.Log("raycastHit:" + b.name);
            //        if (b.CompareTag("Enemy"))
            //        {
            //line.SetPosition(1, target.position);
            //LLHitEffect.transform.position = target.position;
            //            //c.GetComponent<Enemy>().TakeDamage(damage);
            //            //StartCoroutine(Fire(c));
            //        }
            //    }

            //}
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bullet.GetComponent<Bullet>().damage = damage;
            //bullet.GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse); // Придать пуле скорость
            //Destroy(bullet, 2f);
            audioManager.SoundPlay2();
            if(target.GetComponent<Cell>() != null) target.GetComponent<Cell>().TakeDamage(damage);
            if (target.GetComponent<Shield>() != null) target.GetComponent<Shield>().TakeDamage(damage);
            yield return new WaitForSeconds(reload);
        }
        Debug.Log("stop coro по флагу энеми");
        //sprite.rotation = Quaternion.Euler(0,0,0);
        //flag = false;
        //Debug.Log("таргетов нет, корутина кончилась, смотрю в ноль");
        //audioManager.Stop();
        //line.SetPosition(1, transform.position);
        //LLHitEffect.transform.position = transform.position;
        //LLHitEffect.Pause();//if (LLHitEffect.isPlaying) 
        ////StopAllCoroutines();
    }
    public void StartFire()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up, 10, 1 << 3);
        if (raycastHit)
        {
            var c = raycastHit.transform;
            //Debug.Log("raycastHitEnemy:" + c.name);
            if (c.CompareTag("Shield") || c.CompareTag("Cell"))
            {
                if (temp == c && coroIsWorkNow)
                {
                    line.SetPosition(1, raycastHit.point);
                    LLHitEffect.transform.position = raycastHit.point;
                }
                else
                {
                    temp = c;
                    line.SetPosition(1, raycastHit.point);
                    LLHitEffect.transform.position = raycastHit.point;
                    LLHitEffect.Play();
                    Debug.Log("stop coro по новой цели луча");
                    StopAllCoroutines();
                    coroIsWorkNow = false;
                    StartCoroutine(Fire(c));
                }
                
            }
        }
    }
    public void StopFire()
    {
        StopAllCoroutines();
        audioManager.Stop();
        line.SetPosition(1, transform.position);
        LLHitEffect.transform.position = transform.position;
        LLHitEffect.Pause();
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Cell"))
    //    {
    //        targets.Add(other.transform);
    //        //Vector2 direction = (other.transform.position - transform.position).normalized;
    //        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //        //transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    //        if (targets.Count == 1 && !flag)
    //            StartCoroutine(Fire());
    //    }
    //}
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Cell"))
    //    {
    //        targets.Remove(other.transform);
    //    }
    //}
}