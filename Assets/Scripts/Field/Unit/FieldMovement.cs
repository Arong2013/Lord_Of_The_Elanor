using UnityEngine;

public class FieldMovement
{
    private readonly FieldOBJ fieldObj;
    private readonly IMovementGrid movementGrid;
    public bool IsMoving => Vector3.Distance(fieldObj.transform.position, fieldObj.Pos) > 0.01f;

    public FieldMovement(FieldOBJ fieldObj, IMovementGrid movementGrid)
    {
        this.fieldObj = fieldObj;
        this.movementGrid = movementGrid;
    }
    public bool CanMovetoPos(Vector3Int newPos) => movementGrid.CanMovetoPos(newPos);
    public bool IsMoveFinish(Vector3Int newPos) => !IsMoving && fieldObj.Pos == newPos;
    public void Move(Vector3Int newPos)
    {
        fieldObj.UpdatePos(newPos);
        fieldObj.transform.position = Vector3.MoveTowards(fieldObj.transform.position, newPos, 10 * Time.deltaTime);
    }
}
