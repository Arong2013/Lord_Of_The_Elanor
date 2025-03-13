using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAstarGrid
{
    List<AstarNode> PathFinding(IGridObject gridObject, Vector2Int targetPos, bool allowDiagonal);
    List<AstarNode> PathFinding(Vector2Int startPos, Vector2Int targetPos, bool allowDiagonal);
}

public class AstarGrid : IAstarGrid
{
    private readonly int width;
    private readonly int height;
    private readonly MovementGrid movementGrid;
    private readonly AstarNode[,] nodeArray;
    
    public AstarGrid(int width, int height, MovementGrid movementGrid)
    {
        this.width = width;
        this.height = height;
        this.movementGrid = movementGrid;
        nodeArray = new AstarNode[width, height];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool isWall = !movementGrid.GetValue(new Vector3Int(x, 0, y));
                nodeArray[x, y] = new AstarNode(isWall, x, y);
            }
        }
    }

    public List<AstarNode> PathFinding(IGridObject gridObject, Vector2Int targetPos, bool allowDiagonal)
    {InitializeGrid();
        return PathFindingWithSize(Utils.ToVector2Int(gridObject.Pos), targetPos, allowDiagonal, gridObject.SizeList);
    }

    public List<AstarNode> PathFinding(Vector2Int startPos, Vector2Int targetPos, bool allowDiagonal)
    {InitializeGrid();
        return PathFindingWithSize(startPos, targetPos, allowDiagonal, new List<Vector2Int> { Vector2Int.zero });
    }

    private List<AstarNode> PathFindingWithSize(Vector2Int startPos, Vector2Int targetPos, bool allowDiagonal, List<Vector2Int> sizeList)
    {
        if (!IsValidPosition(startPos) || !IsValidPosition(targetPos) || !CanMoveWithSize(targetPos, sizeList))
        {
            Debug.LogWarning("Invalid start or target position.");
            return null;
        }

        var startNode = nodeArray[startPos.x, startPos.y];
        var targetNode = nodeArray[targetPos.x, targetPos.y];

        var openList = new SortedSet<AstarNode>(new AstarNodeComparer()) { startNode };
        var closedSet = new HashSet<AstarNode>();

        startNode.G = 0;
        startNode.H = GetHeuristic(startNode, targetNode);

        while (openList.Count > 0)
        {
            var curNode = openList.Min;
            openList.Remove(curNode);
            closedSet.Add(curNode);

            if (curNode == targetNode)
            {
                return ConstructPath(startNode, targetNode);
            }
            

            foreach (var neighbor in GetNeighbors(curNode, allowDiagonal, sizeList))
            {
                
                if (closedSet.Contains(neighbor) || !CanMoveWithSize(new Vector2Int(neighbor.x, neighbor.y), sizeList))
                    continue;

                int moveCost = curNode.G + GetMoveCost(curNode, neighbor);
                if (moveCost < neighbor.G || !openList.Contains(neighbor))
                {
                    neighbor.G = moveCost;
                    neighbor.H = GetHeuristic(neighbor, targetNode);
                    neighbor.ParentNode = curNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        Debug.LogWarning("No path found.");
        return new List<AstarNode>();
    }

    private bool IsValidPosition(Vector2Int pos) => pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;

    private int GetHeuristic(AstarNode a, AstarNode b) => (Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y)) * 10;

    private int GetMoveCost(AstarNode from, AstarNode to) => (from.x == to.x || from.y == to.y) ? 10 : 14;

private bool CanMoveWithSize(Vector2Int pos, List<Vector2Int> sizeList)
{
    foreach (var offset in sizeList)
    {
        int checkX = pos.x + offset.x;
        int checkY = pos.y + offset.y;

        if (!IsValidPosition(new Vector2Int(checkX, checkY)) || nodeArray[checkX, checkY].isWall)
        {
            return false;
        }
    }
    return true;
}

    private List<AstarNode> ConstructPath(AstarNode startNode, AstarNode targetNode)
    {
        var path = new List<AstarNode>();
        var curNode = targetNode;

        while (curNode != startNode)
        {
            path.Add(curNode);
            curNode = curNode.ParentNode;
        }

        path.Add(startNode);
        path.Reverse();
        return path;
    }

    private List<AstarNode> GetNeighbors(AstarNode node, bool allowDiagonal, List<Vector2Int> sizeList)
    {
        var neighbors = new List<AstarNode>();
        int[,] directions = allowDiagonal
            ? new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }, { 1, 1 }, { -1, 1 }, { -1, -1 }, { 1, -1 } }
            : new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int checkX = node.x + directions[i, 0];
            int checkY = node.y + directions[i, 1];

            if (IsValidPosition(new Vector2Int(checkX, checkY)) && CanMoveWithSize(new Vector2Int(checkX, checkY), sizeList))
            {
                neighbors.Add(nodeArray[checkX, checkY]);
            }
        }

        return neighbors;
    }

private class AstarNodeComparer : IComparer<AstarNode>
{
    public int Compare(AstarNode a, AstarNode b)
    {
        int compare = a.F.CompareTo(b.F);
        if (compare == 0)
        {
            compare = a.y.CompareTo(b.y); 
        }
        return compare;
    }
}

}
