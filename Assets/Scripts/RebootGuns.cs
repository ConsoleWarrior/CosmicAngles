using System.Collections.Generic;
using UnityEngine;

public class RebootGuns : MonoBehaviour
{
    Transform player;
    List<Cell> cells;

    public void DoReboot()
    {
        cells = new();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        RememberGunsSetup();
        ClearGunS();
        LoadGuns();
    }
    void RememberGunsSetup()
    {
        for (int i = 0; i < player.childCount; i++)
        {
            var cell = player.GetChild(i).GetComponent<Cell>();
            if (cell != null && cell.slot != null)
            {
                cells.Add(cell);
            }
        }
    }
    void ClearGunS()
    {
        foreach (var cell in cells)
        {
            if (cell.module != null)
            {
                Destroy(cell.module.gameObject);
                cell.module = null;
                //Debug.Log("очистил " + cell.gameObject.name);
            }
        }
    }
    void LoadGuns()
    {
        foreach (var cell in cells)
        {
            if (cell.slot.GetComponent<UISlot>().currentItem != null)
            {
                cell.slot.GetComponent<UISlot>().currentItem.CallNewModule(cell);
                //Debug.Log("вызвал " + cell.slot.GetComponent<UISlot>().currentItem.gameObject.name);
            }
        }
    }
}
