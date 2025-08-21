using UnityEngine;

public class SellAllScrap : MonoBehaviour
{
    Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

    }
    public void Sell()
    {
        for (int i = 0; i < inventory.inventoryGrid.childCount; i++) 
        {
            var inventoryCellUISlot = inventory.inventoryGrid.GetChild(i).gameObject.GetComponent<UISlot>();
            if (inventoryCellUISlot != null)
            {
                if (!inventoryCellUISlot.isFree && inventoryCellUISlot.currentItem.name == "ScrapItem(Clone)")
                {
                    //if (((UIItemScrap)inventoryCellUISlot.currentItem).scrapItemCount + value <= 100)
                    //{
                    //    ((UIItemScrap)inventoryCellUISlot.currentItem).scrapItemCount += value;
                    //    Debug.Log("collect scrap value : " + value);
                    //    return;
                    //}
                    inventoryCellUISlot.currentItem.SellScrap();
                }
            }
        }
    }
}
