using UnityEngine;
using UnityEngine.EventSystems;

public class UISlotShop : MonoBehaviour, IDropHandler
{
    public UIItem currentItem;
    public bool isFree = true;
    public GameObject UIItemPrefab;
    public void OnDrop(PointerEventData eventData)
    {
        var itemTransform = eventData.pointerDrag.transform;
        if(itemTransform.GetComponent<UIItem>() != null)
        {
            //itemTransform.GetComponent<UIItem>().SellScrap();
        }
    }
}

