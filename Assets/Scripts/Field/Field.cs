using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Field : MonoBehaviour
{
   [SerializeField] PlayerUnit playerUnit;

    //TurnController turnController;
    MovementGrid movementGrid;
    AstarGrid astarGrid;
    [SerializeField] int width, height;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        movementGrid = new MovementGrid(width, height);
        astarGrid = new AstarGrid(width, height,movementGrid);



        var list =  astarGrid.PathFinding(new Vector2Int(0, 0), new Vector2Int(20, 20), false);
        foreach (var item in list)
        {
            Debug.Log(new Vector2(item.x,item.y));    
        }
        playerUnit.Init(movementGrid,astarGrid);
    }
}
