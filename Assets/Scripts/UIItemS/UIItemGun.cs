using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemGun : UIItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null && eventData.pointerEnter != null)
        {
            var point = eventData.pointerEnter.gameObject;
            var slot = point.GetComponent<UISlot>();

            if (startSlotTransform.GetComponent<UISlotShop>() != null) //���� �� � �������� �������� ������
            {
                if (price <= inventory.kredit)
                {
                    if (point.GetComponent<UISlot>() != null && slot.isFree && slot.number != 0)
                    {
                        inventory.UpdateKredit(-price);
                    }
                    else
                    {
                        Debug.Log("�� ���� ��� �������, ����� ��� ��� ����");
                        ReturnBack();
                        return;
                    }
                }
                else
                {
                    Debug.Log("��������� �����");
                    ReturnBack();
                    return;
                }
            }
            else
            {
                if (point.GetComponent<CosmoportPanel>() != null || point.GetComponent<UISlotShop>() != null)
                {
                    startSlotTransform.GetComponent<UISlot>().audioManager.SoundPlay1();
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
                            slot.audioManager.SoundPlay0();
                            Debug.Log("���������� �� �������");
                            ClearOldSlot();
                        }
                        else
                        {
                            ReturnBack();
                            Debug.Log("�� ������ ����");
                            return;
                        }
                    }
                    else
                    {
                        slot.currentItem = this;
                        slot.isFree = false;
                        slot.audioManager.SoundPlay0();
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
            if(startSlotTransform.GetComponent<UISlotShop>() != null)
            {
                Debug.Log("eventData ��� eventData.pointerEnter ����� null");
                ReturnBack();
                return;
            }
            else
            {
                trashPanel.GetComponent<TrashPanel>().item = gameObject;
                trashPanel.SetActive(true);
            }

        }
        transform.localPosition = Vector3.zero;
        group.blocksRaycasts = true;
    }
}

