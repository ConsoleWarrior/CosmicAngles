using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairBlok : MonoBehaviour
{
    public List<Cell> cells = new();
    public int repair1HPCost;
    public TextMeshProUGUI tmpCost;
    Inventory inventory;
    [SerializeField] AudioManager manager;


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
            manager.SoundPlay0();
            inventory.UpdateKredit(-cost);
            RepairNowAll();
            //tmpCost.text = CalculateRepairCellsCost().ToString();
            Debug.Log("выполнен ремонт за " + cost);
        }
        else
        {
            Debug.Log("Ќа ремонт не хватает");
        }
    }
    public float CalculateRepairCellsCost()
    {
        float x = 0;
        foreach (var cell in cells)
        {
            x += cell.CalculateRepairHPCost();
        }
        return (float)System.Math.Round(x * repair1HPCost, 0);
    }
    public void RepairNowAll()
    {
        foreach (var cell in cells)
        {
            cell.Repair();
        }
    }
}
