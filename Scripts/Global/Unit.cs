using UnityEngine;

[RequireComponent(typeof(Transform))]
public abstract class Unit : MonoBehaviour, IDamagable, ITarget
{
    public const float IN_BATTLE_MOVEMENT_SPEED = 6f;

    protected StatSystem _statSystem;
    protected UnitMovement _movement;
    protected StateMachine _stateMachine;

    protected Transform _transform;

    public Vector2 GetPosition => new Vector2(_transform.position.x, _transform.position.y);

    public virtual void Move(Vector2 destination, float speed) => _movement.Move(destination, speed);

    public void TakeDamage(int damage) { }
}
