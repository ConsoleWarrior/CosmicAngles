using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    public GameObject trashPanel;
    void Start()
    {
        inventoryPanel.SetActive(true);
        //inventoryPanel.SetActive(false);
        Invoke("DisableObject", 0.001f);
    }

    private void DisableObject()
    {
        inventoryPanel.SetActive(false);
    }
}
