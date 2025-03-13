using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FieldOBJ : MonoBehaviour
{
    public Vector3Int Pos { get; private set; }
    FieldMovement fieldMovement;
    IMovementGrid movementGrid;
    IAstarGrid astarGrid;
    
    public virtual void Init(IMovementGrid movementGrid,IAstarGrid astarGrid)
    {
        this.movementGrid = movementGrid;
        this.astarGrid = astarGrid; 
        Pos = Utils.ToVector3Int(transform.position);

        fieldMovement = new FieldMovement(this,movementGrid);
    }
        public void UpdatePos(Vector3Int newPos)
    {
        if(Pos != newPos)
        {
            Pos = newPos;
            movementGrid.SetPosToGrid(Pos,false);
        }
    }
    public bool IsMoving => transform.position != Pos;
    public bool CanMoveToGrid(Vector3Int newPos) => fieldMovement.CanMovetoPos(newPos);
    public bool IsMoveFinish(Vector3Int newPos) => fieldMovement.IsMoveFinish(newPos);
    public void Move(Vector3Int newPos) => fieldMovement.Move(newPos);
    public List<AstarNode>GetAstarNodes(Vector3 targetPos,bool isAllow) => astarGrid.PathFinding(Utils.ToVector2Int(Pos),Utils.ToVector2Int(targetPos), isAllow);
}
