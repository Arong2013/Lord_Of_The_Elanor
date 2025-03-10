using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerUnit : Unit, ISubject
{
    List<IObserver> observers = new List<IObserver>();
    IBehaviorNode behaviorNode;

    public override void Init(IMovementGrid movementGrid,IAstarGrid astarGrid)
    {
        base.Init(movementGrid, astarGrid);
        LinkUi();
    }
    public void InputMove(Vector3Int _dir)
    {
        behaviorNode = new ChaseAction(this, _dir, false);
    }
    public void FixedUpdate()
    {
        if(behaviorNode?.Execute() == BehaviorState.FAILURE)
            behaviorNode = null;    
        else
            behaviorNode?.Execute(); 
    }
    void LinkUi()
    {
        Utils.GetUI<TouchMovementUI>().AddListener(InputMove);
        Utils.SetPlayerMarcineOnUI().ForEach(x => x.Initialize(this));
    } 
    public void RegisterObserver(IObserver observer) => observers.Add(observer);
    public void UnregisterObserver(IObserver observer) => observers.Remove(observer);
    public void NotifyObservers() => observers.ForEach(observer => observer.UpdateObserver());
}