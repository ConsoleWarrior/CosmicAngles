using System.Collections;
using UnityEngine;

public class LazerGunEnemy : MonoBehaviour
{
    //[SerializeField] float range;
    [SerializeField] LineRenderer line;
    [SerializeField] ParticleSystem LLHitEffect;
    [SerializeField] AudioManager audioManager;
    [SerializeField] float damage;
    [SerializeField] float reload;
    Transform temp;
    EnemyMoscito enemy;
    public bool coroIsWorkNow = false;

    void Start()
    {
        audioManager.a.volume = 0.12f;
        enemy = transform.parent.GetComponent<EnemyMoscito>();
    }

    void FixedUpdate()
    {
        line.SetPosition(0, transform.position);

        if (enemy.flag)
        {
            StartFire();
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
        while (enemy.flag)
        {
            //Debug.Log("coro working now");
            coroIsWorkNow = true;
            audioManager.SoundPlay2();
            if (target.GetComponent<Cell>() != null) target.GetComponent<Cell>().TakeDamage(damage);
            if (target.GetComponent<Shield>() != null) target.GetComponent<Shield>().TakeDamage(damage);
            yield return new WaitForSeconds(reload);
        }
        audioManager.Stop();
    }
    public void StartFire()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up, 10, 1 << 3);
        if (raycastHit)
        {
            var c = raycastHit.transform;
            if (c.CompareTag("Shield") || c.CompareTag("Cell"))
            {
                if (temp == c && coroIsWorkNow)
                {
                    line.SetPosition(1, raycastHit.point);
                    LLHitEffect.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y, -1);
                }
                else
                {
                    temp = c;
                    line.SetPosition(1, raycastHit.point);
                    LLHitEffect.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y, -1); ;
                    LLHitEffect.Play();
                    //Debug.Log("stop coro по новой цели луча");
                    StopAllCoroutines();
                    coroIsWorkNow = false;
                    StartCoroutine(Fire(c));
                }
            }
        }
    }
}