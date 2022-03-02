using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    private CustomCoroutine _routine;
    private Enemy _unit;
    private Patrol _patrol;
    private Chase _chase;
    private Encounter _encounter;

    public EnemyStateMachine(Enemy unit, State initialState, Patrol patrol, Chase chase, Encounter encounter) : 
        base(initialState)
    {
        _unit = unit;
        _patrol = patrol;
        _chase = chase;
        _encounter = encounter;

        _routine = new CustomCoroutine(_unit, TargetCheckRoutine());

        _routine?.Start();
    }

    private IEnumerator TargetCheckRoutine()
    {
        while (true)
        {
            if (_state != _encounter)
            {
                if (_unit.FieldOfView.IsTargetSeen()) ChangeState(_chase);
                else ChangeState(_patrol);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
