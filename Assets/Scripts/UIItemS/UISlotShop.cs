using UnityEngine;
//using UnityEngine.EventSystems;

public class UISlotShop : MonoBehaviour//, IDropHandler
{
    public UIItem currentItem;
    public bool isFree = true;
    public GameObject UIItemPrefab;

    void Start()
    {
        if (transform.GetComponentInChildren<UIItem>() != null)
        {
            currentItem = transform.GetComponentInChildren<UIItem>();
            UIItemPrefab = transform.GetComponentInChildren<UIItem>().gameObject;
            isFree = false;
        }
    }
    //public void OnDrop(PointerEventData eventData)
    //{
    //    var itemTransform = eventData.pointerDrag.transform;
    //    if(itemTransform.GetComponent<UIItem>() != null)
    //    {
    //        //itemTransform.GetComponent<UIItem>().SellScrap();
    //    }
    //}
}

