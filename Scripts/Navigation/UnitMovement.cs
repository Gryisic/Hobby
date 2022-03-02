using UnityEngine;

public class UnitMovement 
{
    protected Transform _transform;

    public UnitMovement(Transform transform)
    {
        _transform = transform;
    }

    public virtual void Move(Vector2 destination, float speed)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, destination, speed * Time.fixedDeltaTime);
    }
}
