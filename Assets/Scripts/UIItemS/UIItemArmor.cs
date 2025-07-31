using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemArmor : UIItem
{

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (startSlotTransform.GetComponent<UISlotShop>() != null)
            {
                if (startSlotTransform.GetComponent<UISlotShop>().currentItem.price <= inventory.kredit)
                {
                    if (eventData.pointerEnter.transform.GetComponent<UISlot>())
                    {
                        inventory.kredit -= price;
                        inventory.tmpKredit.text = inventory.kredit.ToString();
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
            if (eventData.pointerEnter.gameObject.GetComponent<CosmoportPanel>() != null || eventData.pointerEnter.gameObject.GetComponent<UISlotShop>() != null && startSlotTransform.GetComponent<UISlotShop>() == null)
            {
                SellScrap();
                return;
            }
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
                        cell.module = c.GetComponent<Armor>();
                        //(cell.module as Armor).ConnectToShip();
                        Debug.Log("���������� � �������");
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
                //startSlotTransform.GetComponent<UISlot>().isFree = true;
                //Destroy(startSlotTransform.GetComponent<UISlot>().cell.module.gameObject);
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

    public override GameObject CallNewModule(Cell cell)
    {
        //if (currentSlotTransform.GetComponent<UISlot>().cell.module != null)
        if (cell.module == null)
        {
            GameObject c = (GameObject)Instantiate(itemPrefab, cell.transform.position, cell.transform.rotation);
            c.transform.SetParent(cell.transform);
            cell.module = c.GetComponent<Armor>();
            //cell.module
            Debug.Log("���� ������ ����� �����");
            return c;
        }
        return null;
    }
    public override string ReturnCharacter()
    {
        return "character: " + itemPrefab.GetComponent<Armor>().GetCharacter();
    }
}

