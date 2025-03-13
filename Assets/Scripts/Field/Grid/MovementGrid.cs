using UnityEngine;
using System.Collections.Generic;
public interface IMovementGrid
{
    public bool CanMovetoPos(Vector3Int pos);
    
    public void SetPosToGrid(Vector3Int pos,bool value);
}

public class MovementGrid : Grid<bool> , IMovementGrid
{
    public MovementGrid(int width, int height) : base(width, height)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                SetValue(x, y, true);
    }
    public bool CanMovetoPos(Vector3Int pos) => GetValue(pos);
    public void SetPosToGrid(Vector3Int pos,bool value) => SetValue(pos,value);
}
