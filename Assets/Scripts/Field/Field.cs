using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Field : MonoBehaviour, IMovementGrid
{
   [SerializeField] PlayerUnit playerUnit;

    //TurnController turnController;
    GridMarcine gridMarcine;

    [SerializeField] int width, height;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        gridMarcine = new GridMarcine(width,height);
        playerUnit.Init(this);
    }
    public void UpdateGridPos(IGridObject gridObject, bool newValue) => gridMarcine.UpdateGridPos(gridObject, newValue);
    public bool CanMove(IGridObject gridObject, Vector3Int newPos) => gridMarcine.CanMove(gridObject, newPos);
}
