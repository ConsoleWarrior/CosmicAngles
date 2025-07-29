using System.Collections;
using UnityEngine;

public class LazerGun : LazerGuns
{

    public float range;
    public LineRenderer line;
    //Transform lineEndPoint;
    //Vector2 lineDefaultPoint;
    //public ParticleSystem LLHitEffectLeft;
    public ParticleSystem LLHitEffect;
    //public Collider2D targetCollider;


    void Start()
    {
        audioManager.a.volume = 0.15f;
        //targetCollider = transform.parent.GetComponent<Collider2D>();
    }
    //public void OnEnable()
    //{
    //    line.enabled = true;
    //    line.SetPosition(0, transform.position);

    //    RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.right, range, 1 << 9);
    //    if (raycastHit)
    //    {
    //        line.SetPosition(1, raycastHit.point);
    //    }

    //    else
    //    {
    //        line.SetPosition(1, new Vector3(2, 2, 0));
    //    }
    //}

    //private void OnDisable()
    //{
    //    line.enabled = false;
    //}

    void Update()
    {
        line.SetPosition(0, transform.position);

        //RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up, range);

        //if (raycastHit)
        //{
        //    if (!flag)
        //    {
        //        var c = raycastHit.transform;
        //        Debug.Log("raycastHit:" + c.name);
        //        if (c.CompareTag("Enemy"))
        //        {
        //            line.SetPosition(1, c.position);
        //            LLHitEffect.transform.position = c.position;
        //            LLHitEffect.Play();
        //            //c.GetComponent<Enemy>().TakeDamage(damage);
        //            StartCoroutine(Fire(c));
        //        }
        //    }

        //}
        //else
        //{
        //    flag = false;
        //    Debug.Log("нет попадани€ вообще");
        //    line.SetPosition(1, transform.position);
        //    LLHitEffect.transform.position = transform.position;
        //    LLHitEffect.Pause();//if (LLHitEffect.isPlaying) 
        //    StopAllCoroutines();
        //}
    }
    IEnumerator Fire(Transform c)
    {
        flag = true;
        while (targets.Count > 0)
        {
            var dist = Vector3.Distance(targets[0].position, transform.position);
            target = targets[0];
            if (targets.Count > 1)
            {
                foreach (var item in targets)
                {
                    if (Vector3.Distance(item.position, transform.position) < dist) //провер€ем ближайшую цель
                    {
                        target = item;
                    }
                }
            }

            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up, range);
            if (raycastHit)
            {
                if (!flag)
                {
                    var b = raycastHit.transform;
                    Debug.Log("raycastHit:" + b.name);
                    if (b.CompareTag("Enemy"))
                    {
                        line.SetPosition(1, b.position);
                        LLHitEffect.transform.position = b.position;
                        LLHitEffect.Play();
                        //c.GetComponent<Enemy>().TakeDamage(damage);
                        //StartCoroutine(Fire(c));
                    }
                }

            }
            audioManager.SoundPlay0();
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bullet.GetComponent<Bullet>().damage = damage;
            //bullet.GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse); // ѕридать пуле скорость
            //Destroy(bullet, 2f);
            c.GetComponent<Enemy>().TakeDamage(damage);
            yield return new WaitForSeconds(reload);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        flag = false;
        Debug.Log("нет попадани€ вообще");
        line.SetPosition(1, transform.position);
        LLHitEffect.transform.position = transform.position;
        LLHitEffect.Pause();//if (LLHitEffect.isPlaying) 
        //StopAllCoroutines();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.transform);
            Vector2 direction = (other.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            //if (targets.Count == 1 && !flag)
            //{
            StartCoroutine(Fire(other.transform));
            //}
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Remove(other.transform);
        }
    }
}