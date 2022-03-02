using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    private Enemy _unit;

    private Vector2 _area;
    private float _radius;

    private float _speed;

    private CustomCoroutine _routine;

    public List<PathFindingNode> DebugNodes;

    public Patrol(Enemy unit, Vector2 area, float radius, float speed)
    {
        _unit = unit;
        _area = area;
        _radius = radius;
        _speed = speed;

        _routine = new CustomCoroutine(_unit, PatrolRoutine());
    }

    public override void Enter()
    {
        _routine?.Start();
    }

    public override void Exit()
    {
        _routine?.Stop();
    }

    private bool IsInDestination(Vector2 destination) => Vector2.Distance(_unit.GetPosition, destination) < 0.1f;

    private Vector2 RandomDestinationPoint()
    {
        var randomX = Random.Range(_area.x - _radius, _area.x + _radius);
        var randomY = Random.Range(_area.y - _radius, _area.y + _radius);

        return new Vector2(randomX, randomY);
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            var destination = RandomDestinationPoint();
            var path = PathFinding.BuildedPath(_unit.GetPosition, destination);

            DebugNodes = path;

            if (path != null)
            {
                foreach (var node in path)
                {
                    while (!IsInDestination(node.GetPosition))
                    {
                        _unit.Move(node.GetPosition, _speed);

                        yield return new WaitForFixedUpdate();
                    }
                }
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
