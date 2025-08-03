using System;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Modulus
{
    public float bonus;
    Transform player;
    List<Cell> cells = new();

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        ConnectToShip();
    }
    void Update()
    {

    }
    void OnDestroy()
    {
        if (player != null && !player.GetComponent<Player>().isDestroing)
            DisconnectFromShip();
    }
    public void ConnectToShip()
    {
        RememberCell();
        foreach (Cell cell in cells)
        {
            cell.UpdateArmor(bonus);
            //Debug.Log("+updategood+");
        }
    }
    public void DisconnectFromShip()
    {
        RememberCell();
        foreach (Cell cell in cells)
        {
            cell.UpdateArmor(-(bonus));
            //Debug.Log("-updategood-");
        }
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
        catch (NullReferenceException) { Debug.Log("поймал нуль эксепшн, все ок"); }

    }
    public override string GetCharacter()
    {
        return "HPBonusCells: +" + bonus;
    }
}
