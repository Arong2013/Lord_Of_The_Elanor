using System;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviorState
{
    SUCCESS,
    FAILURE,
    RUNNING
}
public interface IBehaviorNode
{
    BehaviorState Execute();
}

public interface ITurnState
{
    bool Execute();
}
public class ChaseAction : IBehaviorNode
{
    private readonly FieldOBJ fieldOBJ;
    private List<AstarNode> nodes;
    private int currentStep;
    private Vector3Int NextPos => new Vector3Int(nodes[currentStep].x, fieldOBJ.Pos.y, nodes[currentStep].y);

    public ChaseAction(FieldOBJ fieldOBJ, Vector3Int targetPos, bool isAllow)
    {
        this.fieldOBJ = fieldOBJ;
        currentStep = 1;
        nodes = fieldOBJ.GetAstarNodes(targetPos, isAllow);
        Debug.Log(nodes.Count);
    }

    public BehaviorState Execute()
    {
        if (nodes == null || currentStep >= nodes.Count ) 
        {
            Debug.Log(currentStep);
            return BehaviorState.FAILURE;
        }
        if (IsMoveFinish())
        {
            currentStep++;
            if(currentStep >= nodes.Count && !fieldOBJ.CanMove(NextPos))
            nodes = null;
            return BehaviorState.SUCCESS;
        }
        fieldOBJ.Move(NextPos);
        return BehaviorState.SUCCESS;
    }

    public bool IsMoveFinish() => !fieldOBJ.IsMoving && fieldOBJ.Pos == NextPos;
}
