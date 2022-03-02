using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : State
{
    private Unit _unit;

    public Encounter(Unit unit)
    {
        _unit = unit;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {
        
    }
}
