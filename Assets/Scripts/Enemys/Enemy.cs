using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float currentHp;
    [SerializeField] protected Image hpBar;
    [SerializeField] protected GameObject hpBarCanvas;
    [SerializeField] protected AudioManager audioManager;
    protected Animator animator;
    protected Transform player;
    [SerializeField] protected float xp;
    [SerializeField] protected float speed;
    [SerializeField] protected int dropScrapCount;
    protected float dist;
    protected Vector3 target;
    protected bool evolved = false;


    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch { }//Debug.Log("player is not active"); }
        animator = GetComponent<Animator>();
        target = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
        audioManager.a.volume = 0.5f;
    }
    //protected void Starting()
    //{
    //    try
    //    {
    //        player = GameObject.FindGameObjectWithTag("Player").transform;
    //    }
    //    catch { Debug.Log("player is not active"); }
    //    animator = GetComponent<Animator>();
    //    target = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
    //    audioManager.a.volume = 0.5f;
    //}
    void Update()
    {
        if (currentHp <= 0 && !animator.GetBool("Destroy"))
        {
            animator.SetBool("Destroy", true);
            audioManager.a.volume = 1;
            audioManager.SoundPlay0();
            transform.GetComponent<CircleCollider2D>().enabled = false;
            CalculateAndCallDrop();

            //GameObject c = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);
        }
        if (xp == 4 && !evolved)
        {
            Evolve();
            //Debug.Log("Эволюционировал");
            evolved = true;
        }
        if (xp == 8 && evolved)
        {
            EvolveLevel2();
            //Debug.Log("Эволюционировал второй раз");
            evolved = false;
        }
    }
    void FixedUpdate()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        dist = Vector3.Distance(player.position, transform.position);
        if (dist != 0 && dist < 10)
            Atack();
        else
            Eating();
    }
    public virtual void Atack()
    {
    }
    public virtual void CalculateAndCallDrop()
    {
    }
    public virtual void Evolve()
    {
    }
    public virtual void EvolveLevel2()
    {
    }
    public virtual void SetParameters(float damage)
    {
        maxHp = damage;
        currentHp = damage;
        speed = 5;
    }
    public virtual void Eating()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector2 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        hpBarCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (transform.position == target)
        {
            target = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
        hpBar.fillAmount = currentHp / maxHp;
        //audioManager.SoundPlay1();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Scrap"))
        {
            float value = other.gameObject.GetComponent<Scrap>().value;
            xp += value;
        }
        if (other.gameObject.CompareTag("Cell"))
        {
            other.gameObject.GetComponent<Cell>().TakeDamage(currentHp);
            currentHp = 0;
        }
    }
}
