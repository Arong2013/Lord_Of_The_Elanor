using UnityEngine;
using System.Collections.Generic;
public interface IMovementGrid
{
    bool CanMove(IGridObject gridObject, Vector3Int newPos);
    void UpdateGridPos(IGridObject gridObject, bool isOccupied);
}

public class MovementGrid : Grid<bool>
{
    public MovementGrid(int width, int height) : base(width, height)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                SetValue(x, y, true);
    }
    public bool CanMove(IGridObject gridObject, Vector3Int newPosition)
    {
        bool canMove = true;
        SetMovableValue(gridObject, true);

        foreach (var tileOffset in gridObject.SizeList)
        {
            Vector3Int targetTile = newPosition + new Vector3Int(tileOffset.x, tileOffset.y, 0);
            if (!GetValue(targetTile))
            {
                canMove = false;
                break;
            }
        }

        SetMovableValue(gridObject, false);
        return canMove;
    }
    public void SetMovableValue(IGridObject gridObject, bool isOccupied)
    {
        foreach (var tileOffset in gridObject.SizeList)
        {
            Vector3Int currentTile = gridObject.Pos + new Vector3Int(tileOffset.x, tileOffset.y, 0);
            SetValue(currentTile, isOccupied);
        }
    }
}
