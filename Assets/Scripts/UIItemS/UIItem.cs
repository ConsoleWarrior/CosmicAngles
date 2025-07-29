using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string itemName;
    public string type;
    public int price;
    //public string characteristics;
    protected RectTransform rectTransform;
    protected Canvas canvas;
    public CanvasGroup group;
    protected Transform currentSlotTransform;
    protected Transform startSlotTransform;
    protected Inventory inventory;
    public GameObject itemPrefab;
    [SerializeField] Color itemColor;



    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        group = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData == null) Debug.Log("нет ивент даты");
        startSlotTransform = rectTransform.parent;
        currentSlotTransform = rectTransform.parent;
        currentSlotTransform.SetAsLastSibling();
        group.blocksRaycasts = false;
        //if (slotTransform.GetComponent<UISlot>().cell != null)
        //{
        //    Destroy(slotTransform.GetComponent<UISlot>().cell.module.gameObject);
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if (eventData.pointerEnter != null)
        {
            currentSlotTransform = eventData.pointerEnter.transform;
            transform.SetParent(currentSlotTransform);
            currentSlotTransform.SetAsLastSibling();// чтобы было видно поверх слотов
        }

    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {

    }
    public virtual void SellScrap()
    {
        //var slot = startSlotTransform.GetComponent<UISlot>();
        //slot.isFree = true;
        //slot.currentItem = null;
        inventory.UpdateKredit(price / 2);
        //inventory.kredit += scrapItemCount * 10;
        //inventory.tmpKredit.text = inventory.kredit.ToString();
        Debug.Log("продал за полцены"+ price / 2);
        //Destroy(gameObject);
        ClearOldSlot();
        Destroy(gameObject);

    }
    public virtual void ClearOldSlot()
    {
        if (startSlotTransform.GetComponent<UISlot>() != null)
        {
            startSlotTransform.GetComponent<UISlot>().isFree = true;
            if (startSlotTransform.GetComponent<UISlot>().cell != null)
            {
                if (startSlotTransform.GetComponent<UISlot>().cell.module != null)
                {
                    Destroy(startSlotTransform.GetComponent<UISlot>().cell.module.gameObject);
                    startSlotTransform.GetComponent<UISlot>().currentItem = null;// ошибка ребута пушек
                    Debug.Log("переместили из корабля");
                }
                else
                {
                    Debug.Log("переместили из трюма");
                    startSlotTransform.GetComponent<UISlot>().currentItem = null;
                }

            }
        }
        var startShop = startSlotTransform.GetComponent<UISlotShop>();
        if (startShop != null) //вызываем в магазе новый итем вместо старого
        {
            //startSlotTransform.GetComponent<UISlotShop>().isFree = true;
            startShop.currentItem = Instantiate(startShop.UIItemPrefab, startSlotTransform).GetComponent<UIItem>();
            startShop.currentItem.transform.SetParent(startSlotTransform);
            startShop.currentItem.transform.localPosition = Vector3.zero;
            //startShop.currentItem.group.blocksRaycasts = true;
            Debug.Log("Instantiate(startSlotTransform.GetComponent<UISlotShop>().UIItemPrefab");
        }
    }
    public void ReturnBack()
    {
        transform.SetParent(startSlotTransform);
        transform.localPosition = Vector3.zero;
        group.blocksRaycasts = true;
    }
    public virtual GameObject CallNewModule(Cell cell)//затычка
    {
        return cell.gameObject;
    }
    public void ShowInfo()
    {
        ItemInfo info = GameObject.FindGameObjectWithTag("ItemInfo").GetComponent<ItemInfo>();
        if (info == null) { Debug.Log("info == null"); }
        if (itemName == null) { Debug.Log("itemName == null"); }
        info.itemName.text = "name: " + itemName;
        info.type.text = "type: " + type;
        info.characteristics.text = ReturnCharacter();
        info.characteristics.color = itemColor;
        info.cost.text = "price: " + price;
    }
    public virtual string ReturnCharacter()
    {
        return "character: 0";
    }

}
