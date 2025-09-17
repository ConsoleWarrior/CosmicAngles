using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerClickHandler //, IDropHandler
{
    public int number;
    public bool isFree = true;
    public Cell cell;
    public UIItem currentItem;
    ItemInfo info;

    void Start()
    {
        if (transform.GetComponentInChildren<UIItem>() != null)
        {
            currentItem = transform.GetComponentInChildren<UIItem>();
            isFree = false;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (cell != null)
        {
            var obj = GameObject.FindGameObjectWithTag("ItemInfo").transform;
            obj.GetChild(0).gameObject.SetActive(false);
            obj.GetChild(1).gameObject.SetActive(true);
            info = obj.GetChild(1).GetComponent<ItemInfo>();
            info.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            info.gameObject.SetActive(true);
            info.type.text = "MaxHp = " + cell.maxHp + "\ncurrentHp = " + cell.currentHp + "\narmorThickness = " + cell.armorThickness;
            info.characteristics.text = "1 Armor Thickness reduce 10% damage.\n1 Armor upgrade cost 1 TitanBox";
            info.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(UpgradeArmor);
        }
    }
    public void UpgradeArmor()
    {
        if (cell.armorThickness < 5) cell.armorThickness += 1;
        info.type.text = "MaxHp = " + cell.maxHp + "\ncurrentHp = " + cell.currentHp + "\narmorThickness = " + cell.armorThickness;
    }
    //public void OnDrop(PointerEventData eventData)
    //{
    //    var itemTransform = eventData.pointerDrag.transform;
    //    itemTransform.SetParent(transform);
    //    itemTransform.localPosition = Vector3.zero;
    //    currentItem = itemTransform.GetComponent<UIItem>();
    //}
}

