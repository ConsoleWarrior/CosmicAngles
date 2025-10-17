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
    public AudioManager audioManager;


    void Start()
    {
        if (transform.GetComponentInChildren<UIItem>() != null)
        {
            currentItem = transform.GetComponentInChildren<UIItem>();
            isFree = false;
        }
        audioManager.a.volume = 0.5f;
    }
    public void ChooseCell()
    {
        if (cell != null)
        {
            var obj = GameObject.FindGameObjectWithTag("ItemInfo").transform;
            obj.GetChild(0).gameObject.SetActive(false);
            obj.GetChild(1).gameObject.SetActive(true);
            info = obj.GetChild(1).GetComponent<ItemInfo>();
            info.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            info.gameObject.SetActive(true);
            info.type.text = "                  "+cell.name + "\nMaxHp= " + cell.maxHp + ",currentHp= " + cell.currentHp + "\narmorThickness= " + cell.armorThickness;
            info.characteristics.text = "1 Armor = -10% damage, Max -50%\nNext Upgrade cost " + (cell.armorThickness + 1) + " TitanBox";
            info.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(UpgradeArmor);

            //transform.GetChild(0).GetComponent<Image>().color = Color.green;
        }
    }
    public void OnPointerClick(PointerEventData eventData)//
    {
        ChooseCell();
    }
    public void UpgradeArmor()
    {
        if (cell.armorThickness < 5 && cell.inventory.TryBuyCellUpgrade(cell.armorThickness + 1))
        {
            cell.armorThickness += 1;
            cell.UpgradeCellSprite(cell.armorThickness);
            info.type.text = cell.name + "\nMaxHp= " + cell.maxHp + ",currentHp= " + cell.currentHp + "\narmorThickness= " + cell.armorThickness;
            info.characteristics.text = "1 Armor = -10% damage, Max -50%\nNext Upgrade cost " + (cell.armorThickness + 1) + " TitanBox";
        }
    }
    //public void OnDrop(PointerEventData eventData)
    //{
    //    var itemTransform = eventData.pointerDrag.transform;
    //    itemTransform.SetParent(transform);
    //    itemTransform.localPosition = Vector3.zero;
    //    currentItem = itemTransform.GetComponent<UIItem>();
    //}
}

