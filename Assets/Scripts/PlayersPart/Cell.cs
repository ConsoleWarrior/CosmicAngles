using UnityEngine;

public class Cell : MonoBehaviour
{
    public int number;
    [SerializeField] float maxHp;
    [SerializeField] float currentHp;
    public Modulus module;
    Inventory inventory;
    public GameObject slot;
    public Transform shipGrid;
    [SerializeField] AudioManager audioManager;
    public bool isDestroyed = false;
    Player player;


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        try
        {
            player = transform.parent.GetComponent<Player>();
            if (player == null) { Debug.Log("player null"); }
        }
        catch
        {
            Debug.Log("не находит player");
        }
        shipGrid = GameObject.FindGameObjectWithTag("StartPlayer").GetComponent<StartPlayer>().shipGrid;
        for (int i = 0; i < shipGrid.childCount; i++)
        {
            if (shipGrid.GetChild(i).GetComponent<UISlot>() != null)
            {
                if (shipGrid.GetChild(i).GetComponent<UISlot>().number == number) //привязка слота и себя к слоту
                {
                    slot = shipGrid.GetChild(i).gameObject;
                    slot.GetComponent<UISlot>().cell = this;
                    ReturnColor();
                }
            }
        }
    }

    void Update()
    {
        if (currentHp <= 0 && !isDestroyed)
        {
            if (module != null) module.gameObject.SetActive(false);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            if (slot != null) slot.SetActive(false);
            isDestroyed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player == null)
            Debug.Log("player == null");//затычка
        else
        {
            if (other.gameObject.CompareTag("Scrap"))
            {
                float value = other.gameObject.GetComponent<Scrap>().value;
                inventory.CollectScrap(value);
                audioManager.SoundPlay0();
            }
            if (player.isLooting) Debug.Log("isLooting");
            if (other.gameObject.CompareTag("Drop") && !player.isLooting)
            {
                player.isLooting = true;
                var drop = other.gameObject.GetComponent<Drop>().itemPrefab;
                if (other.gameObject == null) Debug.Log("уже уничтожен");
                if (inventory.TryCollectDrop(drop))//&&drop!=null
                {
                    audioManager.SoundPlay0();
                    if (other.gameObject == null)
                    {
                        Debug.Log("дроп уже собрали is null");
                    }
                    Destroy(other.gameObject);
                }
                player.isLooting = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        audioManager.SoundPlay1();
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ReturnColor", 0.12f);
    }
    void ReturnColor()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1 - currentHp / maxHp, 0.5f, 0.5f, 1);
    }
    public float CalculateRepairHPCost()
    {
        var x = maxHp - currentHp;
        return x;
    }
    public void Repair()
    {
        currentHp = maxHp;
        ReturnColor();
        if (module != null) module.gameObject.SetActive(true);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        if (slot != null) slot.SetActive(true);
        isDestroyed = false;
    }
    public void UpdateArmor(float bonus)
    {
        currentHp += bonus;
        maxHp += bonus;
        Debug.Log("updateArmor");
    }
}
