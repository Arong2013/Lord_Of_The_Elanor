using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Unit : FieldOBJ
{
    public Func<bool> TurnActionFuncs { get; protected set; }
    public  float TURNSPEED { get;  protected set; }
}
