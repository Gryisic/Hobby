using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : UnitMovement
{
    public PlayerMovement(Transform transform, float speed) : base(transform, speed) { }

    public override void Move(Vector2 direction)
    {
        _transform.Translate(direction * _speed * Time.fixedDeltaTime);
    }
}
