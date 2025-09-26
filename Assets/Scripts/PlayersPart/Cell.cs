using UnityEngine;

public class Cell : MonoBehaviour
{
    public int number;
    public float maxHp;
    public float currentHp;
    public int armorThickness;
    public Modulus module;
    public Inventory inventory;
    public GameObject slot;
    Transform shipGrid;
    [SerializeField] AudioManager audioManager;
    public bool isDestroyed = false;
    Player player;


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        player = transform.parent.GetComponent<Player>();
        shipGrid = GameObject.FindGameObjectWithTag("StartPlayer").GetComponent<StartPlayer>().shipGrid;
        audioManager.a.volume = 0.7f;
        //try
        //{
        //    player = transform.parent.GetComponent<Player>();
        //    if (player == null) { Debug.Log("player null"); }
        //}
        //catch
        //{
        //    Debug.Log("не находит player");
        //}
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
            if (module != null) //module.gameObject.SetActive(false);
                Destroy(module.gameObject);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            if (slot != null) slot.SetActive(false);
            isDestroyed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Scrap"))
        //{
        //    inventory.TryCollectScrap(other.gameObject);
        //    //audioManager.SoundPlay0();
        //}
        if (other.gameObject.CompareTag("Scrap"))
        {
            var scrap = other.gameObject.GetComponent<Scrap>();
            if (!scrap.isCollected)
            {
                if (inventory.TryCollectScrap(scrap))
                {
                    scrap.isCollected = true;
                    audioManager.SoundPlay0();
                    scrap.ReturnScrapToPool();
                    //Destroy(other.gameObject);
                }
            }
            else Debug.Log("уже собран");
        }
        //if (player.isLooting) Debug.Log("isLooting");
        if (other.gameObject.CompareTag("Drop"))
        {
            var drop = other.gameObject.GetComponent<Drop>();
            if (!drop.isCollected)
            {
                if (inventory.TryCollectDrop(drop.itemPrefab))
                {
                    drop.isCollected = true;
                    audioManager.SoundPlay0();
                    Destroy(other.gameObject);
                }
            }
            else Debug.Log("уже собран");
        }
        if (other.gameObject.CompareTag("TitanBox"))
        {
            var drop = other.gameObject.GetComponent<Scrap>();
            if (!drop.isCollected)
            {
                if (inventory.TryCollectTitan())
                {
                    drop.isCollected = true;
                    audioManager.SoundPlay0();
                    Destroy(other.gameObject);
                }
            }
            else Debug.Log("уже собран");
        }
    }

    public void TakeDamage(float damage)
    {
        audioManager.SoundPlay1();
        currentHp -= (damage - damage * armorThickness / 10);
        if (currentHp < 0) currentHp = 0;
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ReturnColor", 0.12f);
    }
    public void ReturnColor()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1 - currentHp / maxHp, 0.5f, 0.5f, 1);
    }
    public void UpgradeCellSprite(int number)
    {
        if (player == null) player = transform.parent.GetComponent<Player>();
        transform.GetComponent<SpriteRenderer>().sprite = player.armorSprites[number];
    }
    public float CalculateRepairHPCost()
    {
        float x = maxHp - currentHp;
        if (isDestroyed && slot.GetComponent<UISlot>().currentItem != null)
            x += slot.GetComponent<UISlot>().currentItem.price / 100;
        return x;
    }
    public void FullRepair()
    {
        currentHp = maxHp;
        ReturnColor();
        if (slot != null) slot.SetActive(true);
        //if (module != null) module.gameObject.SetActive(true);
        if (slot.GetComponent<UISlot>().currentItem != null)
        {
            slot.GetComponent<UISlot>().currentItem.CallNewModule(this);
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        isDestroyed = false;
    }
    public void UpdateArmor(float bonus)
    {
        currentHp += bonus;
        maxHp += bonus;
        //Debug.Log("updateArmor");
    }
}
