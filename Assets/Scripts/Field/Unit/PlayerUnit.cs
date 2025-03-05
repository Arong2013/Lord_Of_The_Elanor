using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerUnit : Unit, ISubject
{
    List<IObserver> observers = new List<IObserver>();
    public override void Init(IMovementGrid movementGrid)
    {
        base.Init(movementGrid);
        LinkUi();
    }
    public void InputMove(Vector2Int _dir)
    {
        var newPos = Pos + (Vector3Int)_dir;
        if (CanMove(newPos))
            ChangeState(new MoveState(this, newPos));
    }
    void LinkUi() => Utils.SetPlayerMarcineOnUI().ForEach(x => x.Initialize(this));
    public void RegisterObserver(IObserver observer) => observers.Add(observer);
    public void UnregisterObserver(IObserver observer) => observers.Remove(observer);
    public void NotifyObservers() => observers.ForEach(observer => observer.UpdateObserver());
}