using UnityEngine;

public class MoveState : ObjectState
{
    Vector3Int newPos;
    public MoveState(FieldOBJ fieldOBJ, Vector3Int newPos):base(fieldOBJ)
    {
        this.newPos = newPos;
    }
    public override void Enter()
    {
        
    }
    public override void Execute()
    {
        fieldOBJ.Move(newPos);  
    }
    public override void Exit()
    {
        
    }
}