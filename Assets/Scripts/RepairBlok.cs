using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairBlok : MonoBehaviour
{
    public List<Cell> cells = new();
    public int repair1HPCost;
    public List<TextMeshProUGUI> tmpCosts;
    [SerializeField] AudioManager manager;

    Inventory inventory;
    Player player;
    TempValues tempValues;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        tempValues = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<TempValues>();
    }
    //void Update()
    //{
    //    foreach(var tmp in tmpCosts)
    //    {
    //        tmp.text = CalculateRepairCellsCost().ToString();
    //    }
    //}
    public void RefreshCells(Player _player)
    {
        player = _player;
        cells = new();
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).GetComponent<Cell>() != null)
            {
                cells.Add(player.transform.GetChild(i).GetComponent<Cell>());
            }
        }
    }
    public void PushTheButton()
    {
        var cost = CalculateRepairCellsCost();
        if (inventory.kredit >= cost)
        {
            manager.SoundPlay0();
            inventory.UpdateKredit(-cost);
            RepairNowAll();
            tempValues.accelerationModule.RefillFullTank();
            //tmpCost.text = CalculateRepairCellsCost().ToString();
            Debug.Log("выполнен ремонт за " + cost);
            OutputRepairAndRefillCost();
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
        if (tempValues.accelerationModule != null) x += tempValues.accelerationModule.CostRefillFullTank();
        return (float)System.Math.Round(x * repair1HPCost, 0);
    }
    public void OutputRepairAndRefillCost()
    {
        var cost = CalculateRepairCellsCost();
        foreach (var tmp in tmpCosts)
        {
            tmp.text = cost.ToString();
        }
    }
    public void RepairNowAll()
    {
        foreach (var cell in cells)
        {
            cell.FullRepair();
        }
    }
}
