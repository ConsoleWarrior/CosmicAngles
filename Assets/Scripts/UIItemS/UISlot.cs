using UnityEngine;
//using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour//, IDropHandler
{
    public int number;
    public bool isFree = true;
    public Cell cell;
    public UIItem currentItem;

    void Start()
    {
        if(transform.GetComponentInChildren<UIItem>() != null)
        {
            currentItem = transform.GetComponentInChildren<UIItem>();
            isFree = false;
        }
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    var itemTransform = eventData.pointerDrag.transform;
    //    itemTransform.SetParent(transform);
    //    itemTransform.localPosition = Vector3.zero;
    //    currentItem = itemTransform.GetComponent<UIItem>();
    //}
}

