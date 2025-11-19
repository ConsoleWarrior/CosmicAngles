using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
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
    [SerializeField] protected GameObject trashPanel;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        trashPanel = GameObject.Find("Bootstrap").GetComponent<Bootstrap>().trashPanel;

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
        inventory.UpdateKredit(price * 0.3f);
        Debug.Log("продал за " + System.Math.Round(price * 0.3f, 0));
        ClearOldSlot();
        Destroy(gameObject);
    }
    public virtual void ClearOldSlot()
    {
        var slot = startSlotTransform.GetComponent<UISlot>();
        if (slot != null)
        {
            slot.isFree = true;
            if (slot.cell != null)
            {
                if (slot.cell.module != null)
                {
                    Destroy(slot.cell.module.gameObject);
                    slot.currentItem = null;// ошибка ребута пушек
                    //Debug.Log("переместили из корабля, старый слот чист");
                }
                else
                {
                    //Debug.Log("переместили из трюма");
                    slot.currentItem = null;
                }
            }
            else
            {
                //Debug.Log("переместили из трюма/слот фри итем нуль");
                slot.currentItem = null;
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
            var item = Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
            item.transform.SetParent(cell.transform);
            cell.module = item.GetComponent<Modulus>();
            //Debug.Log("Итем вызвал новый модуль : " + c.name);
        }
    }
    public void ShowInfo()
    {
        var obj = GameObject.FindGameObjectWithTag("ItemInfo").GetComponent<PanelsInfo>();
        obj.slotPanel.gameObject.SetActive(false);
        obj.itemPanel.gameObject.SetActive(true);
        ItemInfo info = obj.itemPanel;
        //if (info == null) { Debug.Log("info == null"); }
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
