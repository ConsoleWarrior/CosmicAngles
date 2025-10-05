using UnityEngine;

public class SellAllScrap : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] AudioManager audioManager;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

    }
    public void Sell()
    {
        bool flag = false; float summ = 0;
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
                    flag = true;
                    summ += (inventoryCellUISlot.currentItem as UIItemScrap).scrapItemCount;
                    inventoryCellUISlot.currentItem.SellScrap();
                    inventoryCellUISlot.isFree = true;
                    inventoryCellUISlot.currentItem = null;         //затычка
                }
            }
        }
        if (flag) audioManager.SoundPlay0();
        Debug.Log("продал " + summ + " scrap за: " + summ * 5);
    }
}
