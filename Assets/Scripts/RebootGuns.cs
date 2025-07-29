using System.Collections.Generic;
using UnityEngine;

public class RebootGuns : MonoBehaviour
{
    Transform player;
    List<Cell> cells;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

    }
    public void RefreshCells()
    {
        cells = new();
    }
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
                //cell.slot.GetComponent<UISlot>().currentItem = cell.slot.GetComponent<UISlot>().currentItem.CallNewModule(cell);
            }
        }
    }
}
