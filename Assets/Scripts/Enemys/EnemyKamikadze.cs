using UnityEngine;

public class EnemyKamikadze : Enemy
{

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
        speed += speed*0.3f;
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
    public override void CalculateAndCallDrop()
    {
        if (xp > 8 && Random.Range(0, 25) == 0)
        {
            Instantiate(Resources.Load("DropItems/DropGunBlue", typeof(GameObject)), new(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity);
        }
        GameObject c = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), transform.position, Quaternion.identity);
        c.GetComponent<Scrap>().value = Random.Range(dropScrapCount / 2, dropScrapCount * 2);
        c.transform.localScale = new Vector3(0.6f, 0.6f, 1);
    }
}
