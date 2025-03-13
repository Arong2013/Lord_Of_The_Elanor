using System;
using System.Collections.Generic;
using UnityEngine;
public class Grid<T>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs { public int X; public int Z; }

     readonly int width;
     readonly int height;
    protected readonly T[,] gridArray;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridArray = new T[width, height];
    }

    private bool IsWithinBounds(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;

    public void GetGridCoordinates(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x);
        y = Mathf.FloorToInt(worldPosition.y);
    }

    public void SetValue(int x, int z, T value)
    {
        if (IsWithinBounds(x, z))
        {
            gridArray[x, z] = value;
            OnGridValueChanged?.Invoke(this, new OnGridValueChangedEventArgs { X = x, Z = z });
        }
    }
    public void SetValue(Vector3Int gridPosition, T value) => SetValue(gridPosition.x, gridPosition.z, value);

    public T GetValue(int x, int y) => IsWithinBounds(x, y) ? gridArray[x, y] : default;

    public T GetValue(Vector3Int gridPosition) => GetValue(gridPosition.x, gridPosition.z);
}
