using UnityEngine;

public class ButtonRotate : MonoBehaviour
{
    [SerializeField] UISlot slot;
    int angle = 0;
    public void RotateModule()
    {
        var module = slot.cell.module;
        if (module != null)
        {
            angle -= 90;
            module.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
