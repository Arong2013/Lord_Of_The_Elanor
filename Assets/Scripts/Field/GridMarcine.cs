using System;
using System.Collections.Generic;
using UnityEngine;


class GridMarcine
{
    readonly MovementGrid movementGrid;

    public GridMarcine(int width, int height)
    {
        movementGrid = new MovementGrid(width, height);
    }
    public void SetMovementValue(Vector3Int pos,bool value) => movementGrid.SetValue(pos,value);
    public bool CanMove(IGridObject gridObject, Vector3Int newPosition) => movementGrid.CanMove(gridObject, newPosition);
    public void UpdateGridPos(IGridObject gridObject, bool isOccupied) => movementGrid.SetMovableValue(gridObject, isOccupied);
}


