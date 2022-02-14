using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement 
{
    protected float _speed;

    protected Transform _transform;

    public UnitMovement(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public virtual void Move(Vector2 destination)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, destination, _speed * Time.fixedDeltaTime);
    }

    public virtual void Move(Vector2 destination, float speed)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, destination, speed * Time.fixedDeltaTime);
    }
}
