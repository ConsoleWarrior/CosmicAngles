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
                SellScrap();
                return;
            }
            if (target.GetComponent<UISlot>() != null)
            {
                if (target.GetComponent<UISlot>().isFree && target.GetComponent<UISlot>().cell == null)
                {
                    target.GetComponent<UISlot>().isFree = false;
                    startSlotTransform.GetComponent<UISlot>().isFree = true;
                    Debug.Log("����������� scrap");
                }
                else
                {
                    ReturnBack();
                    Debug.Log("����� ��� ���� �������");
                }
            }
            else
            {
                ReturnBack();
                Debug.Log("��� UISlot");
            }
        }
        else
        {
            Debug.Log("��� ����� ����");
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
        Debug.Log("������ scrap");
        Destroy(gameObject);
    }
    public override string ReturnCharacter()
    {
        return "character: For sale in ports";
    }
}
