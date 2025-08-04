using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float kredit;
    public TextMeshProUGUI tmpKredit;
    public Transform inventoryGrid;

    public void UpdateKredit(float change)
    {
        if ((kredit + change) >= 0)
        {
            kredit += change;
            tmpKredit.text = System.Math.Round(kredit, 0).ToString();
        }
        else Debug.Log("(kredit + change) < 0");

    }
    public void CollectScrap(float value)
    {
        for (int i = 0; i < inventoryGrid.childCount; i++)
        {
            var inventoryCellUISlot = inventoryGrid.GetChild(i).gameObject.GetComponent<UISlot>();
            if (inventoryCellUISlot != null)
            {
                if (!inventoryCellUISlot.isFree && inventoryCellUISlot.currentItem.type != null && inventoryCellUISlot.currentItem.itemName == "Scrap")
                {
                    if (((UIItemScrap)inventoryCellUISlot.currentItem).scrapItemCount + value <= 100)
                    {
                        ((UIItemScrap)inventoryCellUISlot.currentItem).scrapItemCount += value;
                        Debug.Log("collect scrap value : " + value);
                        return;
                    }
                }
            }
        }

        for (int j = 0; j < inventoryGrid.childCount; j++)
        {
            var inventoryCellUISlot2 = inventoryGrid.GetChild(j).gameObject.GetComponent<UISlot>();
            if (inventoryCellUISlot2 != null && inventoryCellUISlot2.isFree)
            {

                GameObject newStack = (GameObject)Instantiate(Resources.Load("UIItems/ScrapItem", typeof(GameObject)), inventoryCellUISlot2.transform.position, inventoryCellUISlot2.transform.rotation);
                newStack.transform.SetParent(inventoryCellUISlot2.transform);
                newStack.transform.localScale = Vector3.one;
                inventoryCellUISlot2.currentItem = newStack.GetComponent<UIItem>();
                ((UIItemScrap)inventoryCellUISlot2.currentItem).scrapItemCount = value;
                inventoryCellUISlot2.isFree = false;
                //Debug.Log("collect scrap good in new stack");
                return;
            }
        }
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
                Debug.Log("collected successfully : "+ newItem.name);
                return true;
            }
        }
        Debug.Log("свободных слотов нет, ДРОП НЕ СОБРАТЬ");
        return false;
    }
}
