using UnityEngine;

public class TrashPanel : MonoBehaviour
{
    public GameObject item;
    [SerializeField] AudioManager audioManager;

    public void ConfirmDelete()
    {
        audioManager.SoundPlay0();
        item.GetComponent<UIItem>().ClearOldSlot();
        Destroy(item);
        gameObject.SetActive(false);
    }
    public void CancelDelete()
    {
        item = null;
        gameObject.SetActive(false);
    }
}
