using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIItemTitanBox : UIItem
{
    public float itemCount;
    public TextMeshProUGUI tmpTitan;


    void Update()
    {
        tmpTitan.text = itemCount.ToString();
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null && eventData.pointerEnter != null)
        {
            var target = eventData.pointerEnter.gameObject;
            var slot = target.GetComponent<UISlot>();
            if (slot != null)
            {
                if (startSlotTransform.GetComponent<UISlotShop>() != null) //если ты в магазине пробуешь купить
                {
                    if (price <= inventory.kredit)
                    {
                        if (slot.number == 99)
                        {
                            if (slot.currentItem != null && slot.currentItem.GetComponent<UIItemTitanBox>() != null)
                            {
                                inventory.UpdateKredit(-price);
                                slot.currentItem.GetComponent<UIItemTitanBox>().itemCount += itemCount;
                                ReturnBack();
                                return;
                            }
                            if (slot.isFree)
                            {
                                inventory.UpdateKredit(-price);
                                slot.isFree = false;
                                slot.currentItem = this;
                                transform.localPosition = Vector3.zero;
                                group.blocksRaycasts = true;
                                ClearOldSlot();
                                return;
                            }
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
                        Debug.Log("нехватает денег");
                        ReturnBack();
                        return;
                    }
                }

                if (slot.isFree && slot.cell == null)
                {
                    slot.isFree = false;
                    slot.currentItem = this;
                    slot.audioManager.SoundPlay0();
                    ClearOldSlot();
                    Debug.Log("переместили titan");
                }
                else
                {
                    ReturnBack();
                    Debug.Log("занят или слот корабля");
                }
            }
            else if (startSlotTransform.GetComponent<UISlot>() != null && (target.GetComponent<CosmoportPanel>() != null || target.GetComponent<UISlotShop>() != null))
            {
                startSlotTransform.GetComponent<UISlot>().audioManager.SoundPlay1();
                SellScrap();
                return;
            }

            else if (target.GetComponent<UIItemTitanBox>() != null)
            {
                if (startSlotTransform.GetComponent<UISlotShop>() != null) //если ты в магазине пробуешь купить
                {
                    if (price <= inventory.kredit)
                    {
                        inventory.UpdateKredit(-price);
                        target.GetComponent<UIItemTitanBox>().itemCount += itemCount;
                        ReturnBack();
                        return;
                    }
                }
                else
                {
                    target.GetComponent<UIItemTitanBox>().itemCount += itemCount;
                    ClearOldSlot() ;
                    Destroy(this.gameObject);
                    return;
                }
            }
            else
            {
                Debug.Log("слот и итем ноли");
                ReturnBack();
            }
        }
        else
        {
            Debug.Log("нет ивент даты");
            ReturnBack();
        }
        transform.localPosition = Vector3.zero;
        group.blocksRaycasts = true;
    }
    //public override void OnEndDrag(PointerEventData eventData)
    //{
    //    if (eventData != null && eventData.pointerEnter != null)
    //    {
    //        var target = eventData.pointerEnter.gameObject;

    //        var slot = target.GetComponent<UISlot>();

    //        if (startSlotTransform.GetComponent<UISlotShop>() != null) //если ты в магазине пробуешь купить
    //        {
    //            if (price <= inventory.kredit)
    //            {
    //                if (slot != null && slot.number == 99)
    //                {
    //                    if (slot.currentItem != null && slot.currentItem.GetComponent<UIItemTitanBox>() != null)
    //                    {
    //                        inventory.UpdateKredit(-price);
    //                        slot.currentItem.GetComponent<UIItemTitanBox>().itemCount += itemCount;
    //                        ClearOldSlot();
    //                        return;
    //                    }
    //                    if (slot.isFree)
    //                    {
    //                        inventory.UpdateKredit(-price);
    //                        ClearOldSlot();
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.Log("не слот для покупки");
    //                    ReturnBack();
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log("нехватает денег");
    //                ReturnBack();
    //                return;
    //            }
    //        }

    //        if (target.GetComponent<CosmoportPanel>() != null || target.GetComponent<UISlotShop>() != null)
    //        {
    //            SellScrap();
    //            return;
    //        }

    //        if (slot != null)
    //        {
    //            if (slot.isFree && slot.cell == null)
    //            {
    //                slot.isFree = false;
    //                if (startSlotTransform.GetComponent<UISlot>() != null) startSlotTransform.GetComponent<UISlot>().isFree = true;
    //                slot.currentItem = this;
    //                ClearOldSlot();
    //                Debug.Log("переместили titan");
    //            }
    //            else
    //            {
    //                ReturnBack();
    //                Debug.Log("занят или слот корабля");
    //            }
    //        }
    //        else
    //        {
    //            ReturnBack();
    //            Debug.Log("Нет UISlot");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("нет ивент даты");
    //        ReturnBack();
    //    }
    //    transform.localPosition = Vector3.zero;
    //    group.blocksRaycasts = true;
    //}
    public override void SellScrap()
    {
        var slot = startSlotTransform.GetComponent<UISlot>();
        slot.isFree = true;
        slot.currentItem = null;
        inventory.UpdateKredit(itemCount * 5000);
        Debug.Log("продал TitanBox за: " + itemCount * 5);
        Destroy(gameObject);
    }
    public override string ReturnCharacter()
    {
        return "character: for upgrade cells armor";
    }
}
