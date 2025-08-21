using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string itemName;
    public string type;
    public int price;
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
        startSlotTransform = rectTransform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (eventData == null) Debug.Log("нет ивент даты");
        startSlotTransform = rectTransform.parent;
        currentSlotTransform = rectTransform.parent;
        currentSlotTransform.SetAsLastSibling();
        group.blocksRaycasts = false;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowInfo();
    }

    public virtual void SellScrap()
    {
        inventory.UpdateKredit(price*0.3f);
        Debug.Log("продал за " + System.Math.Round(price * 0.3f, 0));
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
                    //Debug.Log("переместили из корабля, старый слот чист");
                }
                else
                {
                    //Debug.Log("переместили из трюма");
                    startSlotTransform.GetComponent<UISlot>().currentItem = null;
                }
            }
        }
        var startShop = startSlotTransform.GetComponent<UISlotShop>();
        if (startShop != null) //вызываем в магазе новый итем вместо старого
        {
            startShop.currentItem = Instantiate(startShop.UIItemPrefab, startSlotTransform).GetComponent<UIItem>();
            //startShop.currentItem = Instantiate(startShop.UIItemPrefab, startSlotTransform.position, startSlotTransform.rotation).GetComponent<UIItem>();

            //startShop.currentItem.transform.SetParent(startSlotTransform);
            startShop.currentItem.transform.localPosition = Vector3.zero;
            if (startShop.currentItem.GetComponent<CanvasGroup>() == null) { Debug.Log("нет канваса у кьюрент итема"); }
            startShop.currentItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
            //Debug.Log("Instantiate in shop new "+ startShop.currentItem.gameObject.name);
        }
    }
    public void ReturnBack()
    {
        transform.SetParent(startSlotTransform);
        transform.localPosition = Vector3.zero;
        group.blocksRaycasts = true;
    }
    public virtual void CallNewModule(Cell cell)
    {
        if (cell.module == null)
        {
            GameObject c = (GameObject)Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
            c.transform.SetParent(cell.transform);
            cell.module = c.GetComponent<Modulus>();
            //Debug.Log("Итем вызвал новый модуль : " + c.name);
        }
    }
    public void ShowInfo()
    {
        ItemInfo info = GameObject.FindGameObjectWithTag("ItemInfo").GetComponent<ItemInfo>();
        if (info == null) { Debug.Log("info == null"); }
        if (itemName == null) { Debug.Log("itemName == null"); }
        info.itemName.text = "name: " + itemPrefab.name;
        info.type.text = "type: " + type;
        info.characteristics.text = ReturnCharacter();
        info.characteristics.color = itemColor;
        info.cost.text = "price: " + price;
    }
    public virtual string ReturnCharacter()
    {
        return "character: " + itemPrefab.GetComponent<Modulus>().GetCharacter();
    }
}
