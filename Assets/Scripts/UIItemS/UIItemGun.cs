using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemGun : UIItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            var point = eventData.pointerEnter.gameObject;
            var slot = point.GetComponent<UISlot>();

            if (startSlotTransform.GetComponent<UISlotShop>() != null) //если ты в магазине пробуешь купить
            {
                if (price <= inventory.kredit)
                {
                    if (point.GetComponent<UISlot>() != null && slot.isFree && slot.number != 0)
                    {
                        inventory.UpdateKredit(-price);
                    }
                    else
                    {
                        Debug.Log("не слот для покупки, занят или это ядро");
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
                        if (slot.number != 0)
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
                            Debug.Log("не ядро");
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
}

