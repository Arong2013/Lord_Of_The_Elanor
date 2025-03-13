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
}


