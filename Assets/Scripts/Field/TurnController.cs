using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public interface ITurnable
{
    float TURNSPEED { get; }
    Func<bool> TurnActionFuncs { get; }
    bool TurnPassable();
}
class TurnController
{
    List<ITurnable> turnableList;

    public TurnController(List<ITurnable> _turnableList)
    {
        turnableList = _turnableList;
    }

    public IEnumerator UpdateTurn()
    {
        while (true)
        {
            while (turnableList.Count == 0)
            {
                yield return null;
            }
            foreach (var turnable in turnableList)
            {
                while (!turnable.TurnPassable())
                    yield return null;
            }
            yield return null;
        }
    }
}
