using System.Collections;
using UnityEngine;


public class EnemyDestroyer : Enemy
{
    public bool flag = false;
    [SerializeField] float reloadTime;
    [SerializeField] BlasterEnemy gun1;
    [SerializeField] BlasterEnemy gun2;

    public override void Atack()
    {
        if (!flag && dist < fireDistance)
        {
            flag = true;
            StartCoroutine(Fire());
        }
        else if (dist >= fireDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    IEnumerator Fire()
    {
        while (dist < fireDistance)
        {
            audioManager.SoundPlay1();
            gun1.Fire(player);
            gun2.Fire(player);
            yield return new WaitForSeconds(reloadTime);
        }
        flag = false;
    }
    public override void Evolve()
    {
        maxHp += maxHp * 0.25f;
        currentHp += currentHp * 0.25f;
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        speed += speed * 0.1f;
        //reloadTime -= reloadTime * 0.2f;
        dropScrapCount = 16;
    }
    public override void EvolveLevel2()
    {
        maxHp += maxHp * 0.25f;
        currentHp += currentHp * 0.25f;
        transform.localScale = new Vector3(1.4f, 1.4f, 1);
        speed += speed * 0.1f;
        //reloadTime -= reloadTime * 0.2f;
        dropScrapCount = 18;
    }
}