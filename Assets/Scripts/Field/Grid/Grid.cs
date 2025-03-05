using System;
using System.Collections.Generic;
using UnityEngine;
public interface IGridObject
{
    Vector3Int Pos { get; }
    List<Vector2Int> SizeList { get; }
}
public class Grid<T>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs { public int X; public int Y; }

    private readonly int width;
    private readonly int height;
    private readonly T[,] gridArray;

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

    public void SetValue(int x, int y, T value)
    {
        if (IsWithinBounds(x, y))
        {
            gridArray[x, y] = value;
            OnGridValueChanged?.Invoke(this, new OnGridValueChangedEventArgs { X = x, Y = y });
        }
    }
    public void SetValue(Vector3Int gridPosition, T value) => SetValue(gridPosition.x, gridPosition.y, value);

    public T GetValue(int x, int y) => IsWithinBounds(x, y) ? gridArray[x, y] : default;

    public T GetValue(Vector3Int gridPosition) => GetValue(gridPosition.x, gridPosition.y);
}
