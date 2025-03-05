using System;
using System.Collections.Generic;
using UnityEngine;
public interface IIterator<T>
{
    bool HasNext();
    T Next();
}
public interface IAggregate<T>
{
    IIterator<T> CreateIterator();
}
public enum BehaviorState
{
    SUCCESS,
    RUNNING,
    FAILURE
}
public enum ObjectLayerMask
{
    Ladder,
    Player,
    Monster,
    Ground
}
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void UnregisterObserver(IObserver observer);
    void NotifyObservers();
}
public interface IObserver
{
    void UpdateObserver();
}
public interface IPlayerUesableUI
{
    void Initialize(PlayerUnit playerUnit);
}
public interface IHarvestable
{
    bool CanBeHarvested(); // ������ �������� Ȯ��
    void StartHarvest();   // ������ ����
    void EndHarvest();     // ������ ����
    int GetHarvestReward(); // �������� ��� ���� (��: ��� ����)
}