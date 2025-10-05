using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemScrap : UIItem
{
    public float scrapItemCount;
    public TextMeshProUGUI tmpScrap;


    void Update()
    {
        tmpScrap.text = scrapItemCount.ToString();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null && eventData.pointerEnter != null)
        {
            var target = eventData.pointerEnter.gameObject;
            if (target.GetComponent<CosmoportPanel>() != null || target.GetComponent<UISlotShop>() != null)
            {
                startSlotTransform.GetComponent<UISlot>().audioManager.SoundPlay1();
                SellScrap();
                return;
            }
            var slot = target.GetComponent<UISlot>();
            if (slot != null)
            {
                if (slot.isFree && slot.cell == null)
                {
                    slot.isFree = false;
                    slot.currentItem = this;
                    slot.audioManager.SoundPlay0();
                    ClearOldSlot();
                    //Debug.Log("переместили scrap");
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
        //var slot = startSlotTransform.GetComponent<UISlot>();
        //slot.isFree = true;
        //if (slot.isFree) Debug.Log("free");
        //slot.currentItem = null;
        ClearOldSlot();
        inventory.UpdateKredit(scrapItemCount * 5);
        Debug.Log("продал металлолом за: " + scrapItemCount * 5);
        Destroy(gameObject);
    }
    public override string ReturnCharacter()
    {
        return "character: sell for 5kr";
    }
}
