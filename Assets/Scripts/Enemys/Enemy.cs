using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    [SerializeField] protected float speed;
    [SerializeField] protected float atackDistance;
    [SerializeField] protected float fireDistance;
    [SerializeField] protected Image hpBar;
    [SerializeField] protected GameObject hpBarCanvas;
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected int dropScrapCount;
    [SerializeField] protected List<GameObject> dropPrefabs;
    [SerializeField] protected List<int> dropChance;

    protected ObjectPool<GameObject> enemyPool;
    protected ObjectPool<GameObject> animPool;
    protected MapSector sector;
    protected Transform player;
    protected float dist;
    protected Vector3 target;
    bool destroyFlag = false;


    void Update()
    {
        if (currentHp <= 0 && !destroyFlag)
        {
            destroyFlag = true;
            var obj = animPool.Get();
            obj.transform.position = transform.position;
            obj.GetComponent<AnimDestroy>().animPool = animPool;
            CalculateAndCallDrop();
            Destroying();
        }
    }
    protected void Destroying()
    {
        transform.SetParent(GameObject.Find("PoolManager").transform);
        ResetEnemy();
        enemyPool.Release(this.gameObject);
    }
    void FixedUpdate()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        dist = Vector3.Distance(player.position, transform.position);
        if (dist != 0 && dist < atackDistance)
            Atack();
        else
            Walking();
    }
    public virtual void Atack()
    {
    }
    public virtual void Level0()
    {
    }
    public virtual void Level1()
    {
    }
    public virtual void Level2()
    {
    }
    public virtual void Walking()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector2 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (transform.position == target)
        {
            target = new Vector3(Random.Range(sector.minX, sector.maxX), Random.Range(sector.minY, sector.maxY), 0);
        }
    }
    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
        hpBar.fillAmount = currentHp / maxHp;
        //audioManager.SoundPlay1();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            other.gameObject.GetComponent<Cell>().TakeDamage(currentHp);
            currentHp = 0;
        }
    }
    public virtual void CalculateAndCallDrop()
    {
        if (dropPrefabs != null)
            for (int i = 0; i < dropPrefabs.Count; i++)
            {
                if (dropChance[i] >= Random.Range(1, 1001))
                {
                    if (dropPrefabs[i].CompareTag("Scrap"))
                    {
                        var scrap = Instantiate(dropPrefabs[i], new(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2)), Quaternion.identity);
                        scrap.GetComponent<Scrap>().value = Random.Range(dropScrapCount / 2, dropScrapCount * 2);
                    }
                    else Instantiate(dropPrefabs[i], new(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2)), Quaternion.identity);
                }
            }
    }
    public virtual void ResetEnemy()
    {
        var rand = Random.Range(0, 3);
        if (rand == 0) Level0();
        else if (rand == 1) Level1();
        else Level2();

        hpBar.fillAmount = currentHp / maxHp;
        destroyFlag = false;
        //audioManager.a.volume = 0.5f;
    }
}
