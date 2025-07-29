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
        if (eventData != null)
        {
            if (eventData.pointerEnter.gameObject.GetComponent<CosmoportPanel>() != null || eventData.pointerEnter.gameObject.GetComponent<UISlotShop>() != null)
            {
                SellScrap();
                return;
            }
            if (eventData.pointerEnter.gameObject.GetComponent<UISlot>() != null)
            {
                if (eventData.pointerEnter.gameObject.GetComponent<UISlot>().isFree)
                {
                    eventData.pointerEnter.gameObject.GetComponent<UISlot>().isFree = false;
                    startSlotTransform.GetComponent<UISlot>().isFree = true;
                    Debug.Log("переместили scrap");


                }
                else
                {
                    ReturnBack();
                    Debug.Log("зан€т");
                }
            }
            else
            {
                ReturnBack();
                Debug.Log("Ќет UISlot");
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
        inventory.UpdateKredit(scrapItemCount * 5);
        //inventory.kredit += scrapItemCount * 10;
        //inventory.tmpKredit.text = inventory.kredit.ToString();
        Debug.Log("продал scrap");
        Destroy(gameObject);
    }
}
