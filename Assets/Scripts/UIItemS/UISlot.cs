using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    public int number;
    public bool isFree = true;
    public Cell cell;
    //public string index;
    public UIItem currentItem;


    public void OnDrop(PointerEventData eventData)
    {
        var itemTransform = eventData.pointerDrag.transform;
        itemTransform.SetParent(transform);
        itemTransform.localPosition = Vector3.zero;
        currentItem = itemTransform.GetComponent<UIItem>();
        //index = itemTransform.GetComponent<UIItem>().index;
        //if (cell != null && isFree && index == "Gun")
        //{
        //    GameObject c = (GameObject)Instantiate(Resources.Load("Gun", typeof(GameObject)), cell.transform.position, cell.transform.rotation);
        //    c.transform.SetParent(cell.transform);
        //    cell.module = c.GetComponent<Gun>();
        //    isFree = false;

        //}
        //if (cell == null && isFree && index == "Scrap")
        //{
        //Debug.Log("общий юай слот дроп");
        //}
    }
}

