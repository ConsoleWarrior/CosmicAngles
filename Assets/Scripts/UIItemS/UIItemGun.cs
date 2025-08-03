using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemGun : UIItem
{

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (startSlotTransform.GetComponent<UISlotShop>() != null) //���� �� � �������� �������� ������
            {
                if (startSlotTransform.GetComponent<UISlotShop>().currentItem.price <= inventory.kredit)
                {
                    if (eventData.pointerEnter.transform.GetComponent<UISlot>())
                    {
                        inventory.UpdateKredit(-price);
                    }
                    else
                    {
                        Debug.Log("�� ���� ��� �������");
                        ReturnBack();
                        return;
                    }
                }
                else
                {
                    Debug.Log("����� ���");
                    ReturnBack();
                    return;
                }
            }
            else
            {
                if (eventData.pointerEnter.gameObject.GetComponent<CosmoportPanel>() != null || eventData.pointerEnter.gameObject.GetComponent<UISlotShop>() != null)
                {
                    SellScrap();
                    return;
                }
            }
            if (eventData.pointerEnter.gameObject.GetComponent<UISlot>() != null)
            {
                if (eventData.pointerEnter.gameObject.GetComponent<UISlot>().isFree)
                {
                    eventData.pointerEnter.gameObject.GetComponent<UISlot>().isFree = false;
                    if (eventData.pointerEnter.gameObject.GetComponent<UISlot>().cell != null)
                    {
                        var cell = eventData.pointerEnter.gameObject.GetComponent<UISlot>().cell;
                        GameObject c = (GameObject)Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
                        c.transform.SetParent(cell.transform);
                        cell.module = c.GetComponent<Modulus>();
                        Debug.Log("����������� � �������");
                        ClearOldSlot();
                    }
                    else
                    {
                        Debug.Log("����������� � ���������");
                        ClearOldSlot();
                    }
                }
                else
                {
                    ReturnBack();
                    Debug.Log("���� �����");
                    return;
                }
            }
            else
            {
                Debug.Log("������� ����");
                ReturnBack();
                return;
            }
        }
        else
        {
            Debug.Log("����� ���� ����");
            ReturnBack();
            return;
        }
        transform.localPosition = Vector3.zero;
        group.blocksRaycasts = true;
    }
}

