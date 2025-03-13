using UnityEngine;

public class Block : FieldOBJ
{
    public override void Init(IMovementGrid movementGrid, IAstarGrid astarGrid)
    {
        base.Init(movementGrid, astarGrid);
        movementGrid.SetPosToGrid(Pos, false);
    }
}
