using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    private Enemy _unit;

    private float _speed;

    public Chase(Enemy unit, float speed)
    {
        _unit = unit;
        _speed = speed;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    private IEnumerator Chasing()
    {


        yield return new WaitForFixedUpdate();
    }
}
