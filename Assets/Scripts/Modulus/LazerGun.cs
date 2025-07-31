using System.Collections;
using UnityEngine;

public class LazerGun : LazerGuns
{

    //public float range;
    public LineRenderer line;
    public ParticleSystem LLHitEffect;


    void Start()
    {
        audioManager.a.volume = 0.15f;
    }

    void Update()
    {
        line.SetPosition(0, transform.position);
        if (!flag) line.SetPosition(1, transform.position);
    }
    void FixedUpdate()
    {
        if (flag)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sprite.rotation = Quaternion.Euler(0, 0, angle - 90);
            //audioManager.SoundPlay2();
            line.SetPosition(1, target.position);
            LLHitEffect.transform.position = new Vector3(target.position.x, target.position.y, -1);
            if (!LLHitEffect.isPlaying) LLHitEffect.Play();
        }
    }
    IEnumerator Fire()
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
                    if (Vector3.Distance(item.position, transform.position) < dist) //проверяем ближайшую цель
                    {
                        target = item;
                    }
                }
            }
            audioManager.SoundPlay2();
            target.GetComponent<Enemy>().TakeDamage(damage);
            yield return new WaitForSeconds(reload);
        }
        //sprite.rotation = Quaternion.Euler(0,0,0);
        flag = false;
        //Debug.Log("таргетов нет, корутина кончилась, смотрю в ноль");
        audioManager.Stop();
        line.SetPosition(1, transform.position);
        LLHitEffect.transform.position = transform.position;
        LLHitEffect.Pause();//if (LLHitEffect.isPlaying) 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.transform);
            if (targets.Count == 1 && !flag)
                StartCoroutine(Fire());
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