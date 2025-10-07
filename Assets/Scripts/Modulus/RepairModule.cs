using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairModule : Modulus
{
    [SerializeField] float repairValue;
    [SerializeField] float repairTime;

    Transform player;
    List<Cell> cells = new();

    void Start()
    {
        ConnectToShip();
    }

    void OnDestroy()
    {
        //if (player != null && !player.GetComponent<Player>().isDestroing)
            DisconnectFromShip();
    }
    public void ConnectToShip()
    {
        RememberCell();
        StartCoroutine(AvtoRepairCoro());
    }
    public void DisconnectFromShip()
    {
        //RememberCell();
        //foreach (Cell cell in cells)
        //{
        //    //cell.UpdateArmor(-(bonus));
        //    //Debug.Log("-updategood-");
        //}
        StopAllCoroutines();
        Debug.Log("repair disconnect StopAllCoroutines");

    }
    void RememberCell()
    {
        cells = new();
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            for (int i = 0; i < player.childCount; i++)
            {
                var cell = player.GetChild(i).GetComponent<Cell>();
                if (cell != null)//вместе с ядром
                {
                    cells.Add(cell);
                }
            }
        }
        catch (NullReferenceException) { Debug.Log("поймал нуль эксепшн, все ок?"); }

    }
    IEnumerator AvtoRepairCoro()
    {
        while (true)
        {
            yield return new WaitForSeconds(repairTime);
            foreach (Cell cell in cells)
            {
                if (!cell.isDestroyed)
                {
                    cell.currentHp += repairValue;
                    if (cell.currentHp > cell.maxHp)
                        cell.currentHp = cell.maxHp;
                    cell.ReturnColor();
                }
            }
            Debug.Log("repair cicle done");
        }
    }
    public override string GetCharacter()
    {
        return "Repair " + repairValue + "hp every " + repairTime + "sec";
    }
}
