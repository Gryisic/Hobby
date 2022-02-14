using System.Collections;
using UnityEngine;

public class Patrol : State
{
    private Enemy _unit;

    private Vector2 _area;
    private float _radius;

    private float _speed;

    public Patrol(Enemy unit, Vector2 area, float radius, float speed)
    {
        _unit = unit;
        _area = area;
        _radius = radius;
        _speed = speed;
    }

    public override void Enter()
    {
        _unit.StartCoroutine(Patrolling());
    }

    public override void Exit()
    {
        _unit.StopCoroutine(Patrolling());
    }

    private Vector2 RandomDestinationPoint()
    {
        float randomX = Random.Range(_area.x - _radius, _area.x + _radius);
        float randomY = Random.Range(_area.y - _radius, _area.y + _radius);

        return new Vector2(randomX, randomY);
    }  

    private bool IsInDestination(Vector2 destination)
    {
        return Vector2.Distance(_unit.transform.position, destination) < 1.5f;
    }

    private IEnumerator Patrolling()
    {
        while (true)
        {
            Vector2 destination = RandomDestinationPoint();

            while (IsInDestination(destination) == false)
            {
                _unit.Move(destination, _speed);

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
