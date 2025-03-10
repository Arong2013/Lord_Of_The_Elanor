using System.Collections.Generic;
using UnityEngine;

public abstract class FieldOBJ : MonoBehaviour, IGridObject
{
    public Vector3Int Pos { get; protected set; }
    public List<Vector2Int> SizeList { get; protected set; } = new List<Vector2Int>();

    IMovementGrid movementGrid;
    IAstarGrid astarGrid;

    public bool IsMoving => Vector3.Distance(transform.position, Pos) <= 0.001f;
    public virtual void Init(IMovementGrid movementGrid,IAstarGrid astarGrid)
    {
        this.movementGrid = movementGrid;
        this.astarGrid = astarGrid; 
        Pos = Utils.ToVector3Int(transform.position);
        SizeList.Add(Vector2Int.zero);

        movementGrid.SetMovableValue(this, false);
    }
    public bool CanMove(Vector3Int newPos) => movementGrid.CanMove(this, newPos) && IsMoving;
    public void UpdatePos(Vector3Int newPos)
    {
        movementGrid.SetMovableValue(this, true);
        Pos = newPos;
        movementGrid.SetMovableValue(this, false);
    }

    public void Move(Vector3Int newPos)
    {
        if (Pos != newPos)
            UpdatePos(newPos);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 10 * Time.deltaTime);
    }

    public List<AstarNode>GetAstarNodes(Vector3 targetPos,bool isAllow)
    {
        return astarGrid.PathFinding(this, Utils.ToVector2Int(targetPos), isAllow);
    }
}
