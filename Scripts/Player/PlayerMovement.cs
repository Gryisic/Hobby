using UnityEngine;

public class PlayerMovement : UnitMovement
{
    public PlayerMovement(Transform transform) : base(transform) { }

    public override void Move(Vector2 direction, float speed)
    {
        _transform.Translate(direction * speed * Time.fixedDeltaTime);
    }
}
