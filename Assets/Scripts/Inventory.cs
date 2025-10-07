using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float kredit;
    public TextMeshProUGUI tmpKredit;
    public Transform inventoryGrid;
    //[SerializeField] AudioManager audioManager;


    public void UpdateKredit(float change)
    {
        if ((kredit + change) >= 0)
        {
            kredit += change;
            tmpKredit.text = System.Math.Round(kredit, 0).ToString();
        }
        else Debug.Log("(kredit + change) < 0");

    }
    public bool TryCollectScrap(Scrap scrap)
    {
        for (int i = 0; i < inventoryGrid.childCount; i++) //пробую запихнуть в неполный стак
        {
            var inventoryCellUISlot = inventoryGrid.GetChild(i).gameObject.GetComponent<UISlot>();
            if (inventoryCellUISlot != null)
            {
                if (!inventoryCellUISlot.isFree && inventoryCellUISlot.currentItem.name == "ScrapItem(Clone)")
                {
                    if (((UIItemScrap)inventoryCellUISlot.currentItem).scrapItemCount + scrap.value <= 1000)
                    {
                        ((UIItemScrap)inventoryCellUISlot.currentItem).scrapItemCount += scrap.value;
                        //Debug.Log("collect scrap value : " + scrap.value);
                        return true;
                    }
                }
            }
        }
        for (int j = 0; j < inventoryGrid.childCount; j++) //пробую создать новый стак
        {
            var inventoryCellUISlot2 = inventoryGrid.GetChild(j).gameObject.GetComponent<UISlot>();
            if (inventoryCellUISlot2 != null && inventoryCellUISlot2.isFree)
            {

                GameObject newStack = (GameObject)Instantiate(Resources.Load("UIItems/ScrapItem", typeof(GameObject)), inventoryCellUISlot2.transform.position, inventoryCellUISlot2.transform.rotation);
                newStack.transform.SetParent(inventoryCellUISlot2.transform);
                newStack.transform.localScale = Vector3.one;
                inventoryCellUISlot2.currentItem = newStack.GetComponent<UIItem>();
                ((UIItemScrap)inventoryCellUISlot2.currentItem).scrapItemCount = scrap.value;
                inventoryCellUISlot2.isFree = false;
                //Debug.Log("collect scrap in new stack : " + scrap.value);
                return true;
            }
        }
        return false;
    }
    public bool TryCollectDrop(GameObject drop)
    {
        for (int i = 0; i < inventoryGrid.childCount; i++)
        {
            var inventoryCellUISlot = inventoryGrid.GetChild(i).gameObject.GetComponent<UISlot>();
            if (inventoryCellUISlot != null && inventoryCellUISlot.isFree)
            {
                GameObject newItem = (GameObject)Instantiate(drop, inventoryCellUISlot.transform.position, inventoryCellUISlot.transform.rotation);
                newItem.transform.SetParent(inventoryCellUISlot.transform);
                newItem.transform.localScale = Vector3.one;
                inventoryCellUISlot.currentItem = newItem.GetComponent<UIItem>();

                newItem.GetComponent<UIItemGun>().enabled = true;
                newItem.GetComponent<CanvasGroup>().enabled = true;//  затычка для бага с некликабельностью

                inventoryCellUISlot.isFree = false;
                Debug.Log("collected successfully : " + newItem.name);
                return true;
            }
        }
        Debug.Log("свободных слотов нет, ДРОП НЕ СОБРАТЬ");
        return false;
    }
    public bool TryCollectTitan()
    {
        for (int i = 0; i < inventoryGrid.childCount; i++) //пробую запихнуть в неполный стак
        {
            var inventoryCellUISlot = inventoryGrid.GetChild(i).gameObject.GetComponent<UISlot>();
            if (!inventoryCellUISlot.isFree && inventoryCellUISlot.currentItem.name == "TitanBoxItem(Clone)")
            {
                if (((UIItemTitanBox)inventoryCellUISlot.currentItem).itemCount + 1 <= 1000)
                {
                    ((UIItemTitanBox)inventoryCellUISlot.currentItem).itemCount += 1;
                    Debug.Log("collect TitanBox");
                    return true;
                }
            }
        }
        for (int j = 0; j < inventoryGrid.childCount; j++) //пробую создать новый стак
        {
            var inventoryCellUISlot2 = inventoryGrid.GetChild(j).gameObject.GetComponent<UISlot>();
            if (inventoryCellUISlot2 != null && inventoryCellUISlot2.isFree)
            {

                GameObject newStack = (GameObject)Instantiate(Resources.Load("UIItems/TitanBoxItem", typeof(GameObject)), inventoryCellUISlot2.transform.position, inventoryCellUISlot2.transform.rotation);
                newStack.transform.SetParent(inventoryCellUISlot2.transform);
                newStack.transform.localScale = Vector3.one;
                inventoryCellUISlot2.currentItem = newStack.GetComponent<UIItem>();
                ((UIItemTitanBox)inventoryCellUISlot2.currentItem).itemCount = 1;
                inventoryCellUISlot2.isFree = false;
                Debug.Log("collect TitanBox");
                return true;
            }
        }
        Debug.Log("свободных слотов нет, ДРОП НЕ СОБРАТЬ");
        return false;
    }
    public bool TryBuyCellUpgrade(int cost)
    {
        for (int i = 0; i < inventoryGrid.childCount; i++)
        {
            var inventoryCellUISlot = inventoryGrid.GetChild(i).gameObject.GetComponent<UISlot>();
            if (!inventoryCellUISlot.isFree && inventoryCellUISlot.currentItem.name == "TitanBoxItem(Clone)")// || inventoryCellUISlot.currentItem.name == "TitanBoxItem")
            {
                if (((UIItemTitanBox)inventoryCellUISlot.currentItem).itemCount - cost >= 0)
                {
                    ((UIItemTitanBox)inventoryCellUISlot.currentItem).itemCount -= cost;
                    Debug.Log("buy upgrade for:" + cost);
                    if (((UIItemTitanBox)inventoryCellUISlot.currentItem).itemCount == 0)
                    {
                        Destroy(inventoryCellUISlot.currentItem.gameObject);
                        inventoryCellUISlot.isFree = true;
                    }
                    return true;
                }
            }
        }
        return false;
    }
}
