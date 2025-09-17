using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (target.GetComponent<CosmoportPanel>() != null || target.GetComponent<UISlotShop>() != null)
            {
                SellScrap();
                return;
            }
            if (target.GetComponent<UISlot>() != null)
            {
                if (target.GetComponent<UISlot>().isFree && target.GetComponent<UISlot>().cell == null)
                {
                    target.GetComponent<UISlot>().isFree = false;
                    startSlotTransform.GetComponent<UISlot>().isFree = true;
                    target.GetComponent<UISlot>().currentItem = this;

                    Debug.Log("переместили titan");
                }
                else
                {
                    ReturnBack();
                    Debug.Log("занят или слот корабля");
                }
            }
            else
            {
                ReturnBack();
                Debug.Log("Нет UISlot");
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
