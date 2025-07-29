using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemGun : UIItem
{

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (startSlotTransform.GetComponent<UISlotShop>() != null) //если ты в магазине пробуешь купить
            {
                if (startSlotTransform.GetComponent<UISlotShop>().currentItem.price <= inventory.kredit)
                {
                    if (eventData.pointerEnter.transform.GetComponent<UISlot>())
                    {
                        inventory.kredit -= price;
                        inventory.tmpKredit.text = inventory.kredit.ToString();
                        Debug.Log("купил за :"+price);
                    }
                    else
                    {
                        Debug.Log("не слот для покупки");
                        ReturnBack();
                        return;
                    }
                }
                else
                {
                    Debug.Log("денег нет");
                    ReturnBack();
                    return;
                }
            }
            else
            {
                Debug.Log("startSlotTransform.GetComponent<UISlotShop>() == null");
                if (eventData.pointerEnter.gameObject.GetComponent<CosmoportPanel>() != null || eventData.pointerEnter.gameObject.GetComponent<UISlotShop>() != null)
                {
                    SellScrap();
                    
                    return;
                }
            }

            //if (eventData.pointerEnter.gameObject.GetComponent<CosmoportPanel>() != null || eventData.pointerEnter.gameObject.GetComponent<UISlotShop>() != null && startSlotTransform.GetComponent<UISlotShop>() == null)
            //{
            //    SellScrap();
            //    Debug.Log("startSlotTransform.GetComponent<UISlotShop>() == null");
            //    return;
            //}
            if (eventData.pointerEnter.gameObject.GetComponent<UISlot>() != null)
            {
                if (eventData.pointerEnter.gameObject.GetComponent<UISlot>().isFree)///////////////////////
                {
                    eventData.pointerEnter.gameObject.GetComponent<UISlot>().isFree = false;
                    if (eventData.pointerEnter.gameObject.GetComponent<UISlot>().cell != null)
                    {
                        var cell = eventData.pointerEnter.gameObject.GetComponent<UISlot>().cell;
                        GameObject c = (GameObject)Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
                        c.transform.SetParent(cell.transform);
                        cell.module = c.GetComponent<Modulus>();
                        Debug.Log("переместили в корабль");
                        ClearOldSlot();
                    }
                    else
                    {
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
                //startSlotTransform.GetComponent<UISlot>().isFree = true;
                //Destroy(startSlotTransform.GetComponent<UISlot>().cell.module.gameObject);
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
            Debug.Log("ивент дата ноль");
            ReturnBack();
            return;
        }
        transform.localPosition = Vector3.zero;
        group.blocksRaycasts = true;
    }
    //public void SellScrap()
    //{
    //    //var slot = startSlotTransform.GetComponent<UISlot>();
    //    //slot.isFree = true;
    //    //slot.currentItem = null;
    //    inventory.UpdateKredit(price / 2);
    //    //inventory.kredit += scrapItemCount * 10;
    //    //inventory.tmpKredit.text = inventory.kredit.ToString();
    //    Debug.Log("продал gun за полцены");
    //    //Destroy(gameObject);
    //    ClearOldSlot();
    //    Destroy(gameObject);

    //}
    //void ClearOldSlot()
    //{
    //    if (startSlotTransform.GetComponent<UISlot>() != null)
    //    {
    //        startSlotTransform.GetComponent<UISlot>().isFree = true;
    //        if (startSlotTransform.GetComponent<UISlot>().cell != null)
    //        {
    //            if (startSlotTransform.GetComponent<UISlot>().cell.module != null)
    //            {
    //                Destroy(startSlotTransform.GetComponent<UISlot>().cell.module.gameObject);
    //                startSlotTransform.GetComponent<UISlot>().currentItem = null;// ошибка ребута пушек
    //                Debug.Log("переместили из корабля");
    //            }
    //            else
    //            {
    //                Debug.Log("переместили из трюма");
    //                startSlotTransform.GetComponent<UISlot>().currentItem = null;
    //            }

    //        }
    //    }
    //    if (startSlotTransform.GetComponent<UISlotShop>() != null)
    //    {
    //        //startSlotTransform.GetComponent<UISlotShop>().isFree = true;
    //        startSlotTransform.GetComponent<UISlotShop>().currentItem = Instantiate(startSlotTransform.GetComponent<UISlotShop>().UIItemPrefab, startSlotTransform).GetComponent<UIItem>();
    //        Debug.Log("Instantiate(startSlotTransform.GetComponent<UISlotShop>().UIItemPrefab");
    //    }
    //}
    public override GameObject CallNewModule(Cell cell)
    {
        //if (currentSlotTransform.GetComponent<UISlot>().cell.module != null)
        if (cell.module == null)
        {
            GameObject c = (GameObject)Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
            c.transform.SetParent(cell.transform);
            cell.module = c.GetComponent<Guns>();
            Debug.Log("Итем вызвал новый ствол");
            return c;
        }
        return null;
    }
    public override string ReturnCharacter()
    {
        //itemPrefab.GetComponent<Guns>().GetCharacter();
        //characteristics += itemPrefab.GetComponent<Guns>().GetCharacter();

        return "character: " + itemPrefab.GetComponent<Modulus>().GetCharacter();
    }
}

