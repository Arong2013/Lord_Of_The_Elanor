using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
public interface IIterator<T>
{
    bool HasNext();
    T Next();
}
public interface IAggregate<T>
{
    IIterator<T> CreateIterator();
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
    bool CanBeHarvested(); // 갈무리 가능한지 확인
    void StartHarvest();   // 갈무리 시작
    void EndHarvest();     // 갈무리 종료
    int GetHarvestReward(); // 갈무리로 얻는 보상 (예: 재료 수량)
}
public interface IController
{
    public Vector3Int TouchPos {  get;}  
}