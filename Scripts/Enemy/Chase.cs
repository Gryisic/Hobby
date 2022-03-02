using System.Collections;
using UnityEngine;

public class Chase : State
{
    private Enemy _unit;

    private float _speed;

    private CustomCoroutine _routine;

    public Chase(Enemy unit, float speed)
    {
        _unit = unit;
        _speed = speed;

        _routine = new CustomCoroutine(_unit, ChaseRoutine());
    }

    public override void Enter()
    {
        _routine?.Start();
    }

    public override void Exit()
    {
        _routine?.Stop();
    }

    private IEnumerator ChaseRoutine()
    {
        while (true)
        {
            _unit.Move(_unit.FieldOfView.Target.GetPosition, _speed);

            yield return new WaitForFixedUpdate();
        }
    }
}
