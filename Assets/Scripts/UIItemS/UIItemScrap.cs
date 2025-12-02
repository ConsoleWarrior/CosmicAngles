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
                    transform.localPosition = Vector3.zero;
                    group.blocksRaycasts = true;
                    Debug.Log("переместили scrap");
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
            trashPanel.GetComponent<TrashPanel>().item = gameObject;
            trashPanel.SetActive(true);

            ReturnBack();
            Debug.Log("eventData null, выбросить?");
        }

    }
    public override void SellScrap()
    {
        //var slot = startSlotTransform.GetComponent<UISlot>();
        //slot.isFree = true;
        //if (slot.isFree) Debug.Log("free");
        //slot.currentItem = null;
        ClearOldSlot();
        inventory.UpdateKredit(scrapItemCount * price * 0.3f);
        Debug.Log("продал металлолом за: " + System.Math.Round(scrapItemCount * price * 0.3f, 0));
        Destroy(gameObject);
    }
    public override string ReturnCharacter()
    {
        return "character: for sell";
    }
}
