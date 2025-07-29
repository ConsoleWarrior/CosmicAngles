using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairBlok : MonoBehaviour
{
    public List<Cell> cells = new();
    public int repair1HPCost;
    public TextMeshProUGUI tmpCost;
    Inventory inventory;


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }
    void Update()
    {
        tmpCost.text = CalculateRepairCellsCost().ToString();
    }
    public void RefreshCells()
    {
        cells = new();
    }
    public void PushTheButton()
    {
        var cost = CalculateRepairCellsCost();
        if (inventory.kredit >= cost)
        {
            inventory.UpdateKredit(-cost);
            RepairNowAll();
            //tmpCost.text = CalculateRepairCellsCost().ToString();
            Debug.Log("ремонт успешен, остаток кредита =" + inventory.kredit);
        }
        else
        {
            Debug.Log("На ремонт не хватает");
        }
    }
    public float CalculateRepairCellsCost()
    {
        float x = 0;
        foreach (var cell in cells)
        {
            x += cell.CalculateRepairHPCost();
        }
        return x * repair1HPCost;
    }
    public void RepairNowAll()
    {
        foreach (var cell in cells)
        {
            cell.Repair();
        }
    }
}
