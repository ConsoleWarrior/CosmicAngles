using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemCoreModule : UIItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null && eventData.pointerEnter != null)
        {
            var point = eventData.pointerEnter.gameObject;
            var slot = point.GetComponent<UISlot>();

            if (startSlotTransform.GetComponent<UISlotShop>() != null) //если ты в магазине пробуешь купить
            {
                if (price <= inventory.kredit)
                {
                    if (point.GetComponent<UISlot>() != null && slot.isFree && (slot.number == 0 || slot.number == 99))
                    {
                        inventory.UpdateKredit(-price);
                    }
                    else
                    {
                        Debug.Log("не слот для покупки, занят или не ядро");
                        ReturnBack();
                        return;
                    }
                }
                else
                {
                    Debug.Log("нехватает денег");
                    ReturnBack();
                    return;
                }
            }
            else
            {
                if (point.GetComponent<CosmoportPanel>() != null || point.GetComponent<UISlotShop>() != null)
                {
                    SellScrap();
                    return;
                }
            }
            if (slot != null)
            {
                if (slot.isFree)
                {
                    if (slot.cell != null)
                    {
                        if (slot.number == 0)
                        {
                            var cell = slot.cell;
                            GameObject c = (GameObject)Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
                            c.transform.SetParent(cell.transform);
                            cell.module = c.GetComponent<Modulus>();
                            slot.currentItem = this;
                            slot.isFree = false;
                            Debug.Log("установили на корабль");
                            ClearOldSlot();
                        }
                        else
                        {
                            ReturnBack();
                            Debug.Log("это не ядро");
                            return;
                        }
                    }
                    else
                    {
                        slot.currentItem = this;
                        slot.isFree = false;
                        Debug.Log("переместили в инвентарь");
                        ClearOldSlot();
                    }
                }
                else
                {
                    ReturnBack();
                    Debug.Log("слот занят");
                    return;
                }
            }
            else
            {
                Debug.Log("юайслот нуль");
                ReturnBack();
                return;
            }
        }
        else
        {
            Debug.Log("ивент дата нуль");
            ReturnBack();
            return;
        }
        transform.localPosition = Vector3.zero;
        group.blocksRaycasts = true;
    }
    public override void CallNewModule(Cell cell)
    {
        if (cell.module == null)
        {
            GameObject c = (GameObject)Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
            c.transform.SetParent(cell.transform);
            cell.module = c.GetComponent<Modulus>();

            if (c.GetComponent<Acceleration>() != null)
                c.GetComponent<Acceleration>().RefillFullTank();
            //Debug.Log("Итем вызвал новый модуль : " + c.name);
        }
    }
}
