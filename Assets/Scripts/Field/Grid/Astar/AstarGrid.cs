using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAstarGrid
{
    List<AstarNode> PathFinding(IGridObject gridObject, Vector2Int targetPos, bool allowDiagonal);
    List<AstarNode> PathFinding(Vector2Int startPos, Vector2Int targetPos, bool allowDiagonal);
}

public class AstarGrid : IAstarGrid
{
    readonly int width;
    readonly int height;
    private MovementGrid movementGrid;
    private AstarNode[,] nodeArray;

    public AstarGrid(int width, int height, MovementGrid movementGrid)
    {
        this.movementGrid = movementGrid;
        this.width = width;
        this.height = height;
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        nodeArray = new AstarNode[width, height];
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
    {
        var startPos = Utils.ToVector2Int(gridObject.Pos);
        return PathFindingWithSize(startPos, targetPos, allowDiagonal, gridObject.SizeList);
    }

    public List<AstarNode> PathFinding(Vector2Int startPos, Vector2Int targetPos, bool allowDiagonal)
    {
        return PathFindingWithSize(startPos, targetPos, allowDiagonal, new List<Vector2Int> { Vector2Int.zero });
    }

    private List<AstarNode> PathFindingWithSize(Vector2Int startPos, Vector2Int targetPos, bool allowDiagonal, List<Vector2Int> sizeList)
    {
        InitializeGrid();

        if (startPos.x < 0 || startPos.y < 0 || targetPos.x < 0 || targetPos.y < 0 ||
            startPos.x >= width || startPos.y >= height || targetPos.x >= width || targetPos.y >= height)
        {
            Debug.LogWarning("Start or target position is out of bounds.");
            return new List<AstarNode>();
        }

        AstarNode startNode = nodeArray[startPos.x, startPos.y];
        AstarNode targetNode = nodeArray[targetPos.x, targetPos.y];

        if (!CanMoveWithSize(targetPos, sizeList))
        {
            Debug.LogWarning("Target position is not walkable.");
            return new List<AstarNode>();
        }

        var openList = new List<AstarNode> { startNode };
        var closedSet = new HashSet<AstarNode>();
        var finalNodeList = new List<AstarNode>();

        while (openList.Count > 0)
        {
            var curNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < curNode.F || (openList[i].F == curNode.F && openList[i].H < curNode.H))
                {
                    curNode = openList[i];
                }
            }

            openList.Remove(curNode);
            closedSet.Add(curNode);

            if (curNode == targetNode)
            {
                AstarNode targetCurNode = targetNode;
                while (targetCurNode != startNode)
                {
                    finalNodeList.Add(targetCurNode);
                    targetCurNode = targetCurNode.ParentNode;
                }
                finalNodeList.Add(startNode);
                finalNodeList.Reverse();
                return finalNodeList;
            }

            OpenListAdd(curNode.x, curNode.y + 1, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);
            OpenListAdd(curNode.x + 1, curNode.y, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);
            OpenListAdd(curNode.x, curNode.y - 1, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);
            OpenListAdd(curNode.x - 1, curNode.y, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);

            if (allowDiagonal)
            {
                OpenListAdd(curNode.x + 1, curNode.y + 1, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);
                OpenListAdd(curNode.x - 1, curNode.y + 1, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);
                OpenListAdd(curNode.x - 1, curNode.y - 1, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);
                OpenListAdd(curNode.x + 1, curNode.y - 1, openList, closedSet, curNode, targetNode, allowDiagonal, sizeList);
            }
        }

        Debug.LogWarning("No path found.");
        return new List<AstarNode>();
    }

    private bool CanMoveWithSize(Vector2Int pos, List<Vector2Int> sizeList)
    {
        foreach (var offset in sizeList)
        {
            int checkX = pos.x + offset.x;
            int checkY = pos.y + offset.y;
            if (checkX < 0 || checkX >= width || checkY < 0 || checkY >= height || nodeArray[checkX, checkY].isWall)
                return false;
        }
        return true;
    }

    private void OpenListAdd(int checkX, int checkY, List<AstarNode> openList, HashSet<AstarNode> closedSet, AstarNode curNode, AstarNode targetNode, bool allowDiagonal, List<Vector2Int> sizeList)
    {
        if (checkX < 0 || checkX >= width || checkY < 0 || checkY >= height)
            return;

        if (!CanMoveWithSize(new Vector2Int(checkX, checkY), sizeList))
            return;

        AstarNode neighborNode = nodeArray[checkX, checkY];
        if (closedSet.Contains(neighborNode))
            return;

        if (allowDiagonal)
        {
            if (nodeArray[curNode.x, checkY].isWall && nodeArray[checkX, curNode.y].isWall)
                return;
        }

        int moveCost = curNode.G + (curNode.x == checkX || curNode.y == checkY ? 10 : 14);
        if (moveCost < neighborNode.G || !openList.Contains(neighborNode))
        {
            neighborNode.G = moveCost;
            neighborNode.H = (Mathf.Abs(neighborNode.x - targetNode.x) + Mathf.Abs(neighborNode.y - targetNode.y)) * 10;
            neighborNode.ParentNode = curNode;

            if (!openList.Contains(neighborNode))
                openList.Add(neighborNode);
        }
    }
}
