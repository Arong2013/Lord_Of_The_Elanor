using UnityEngine;

public class Block : FieldOBJ
{
    public override void Init(IMovementGrid movementGrid, IAstarGrid astarGrid)
    {
        base.Init(movementGrid, astarGrid);
        SizeList.Add(Vector2Int.zero);
        movementGrid.SetMovableValue(this, false);
    }
}
