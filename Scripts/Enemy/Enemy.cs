using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Enemy : Unit
{
    public FieldOfView FieldOfView => _fieldOfView;
    public StateMachine StateMachine => _stateMachine;

    [Header("Field Of View")]
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewDistance;

    [Header("Patrol")]
    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _patrolSpeed;
    private Vector2 _patrolArea;

    [Header("Chase")]
    [SerializeField] private float _chaseSpeed;

    private FieldOfView _fieldOfView;

    public override void Move(Vector2 destination, float speed)
    {
        base.Move(destination, speed);

        _fieldOfView.SetOrigin(transform);
        _fieldOfView.SetDirectionOfView(destination - (Vector2)transform.position);
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        var patrol = new Patrol(this, _patrolArea, _patrolRadius, _patrolSpeed);
        var chase = new Chase(this, _chaseSpeed);
        var encounter = new Encounter(this);

        _transform = GetComponent<Transform>();

        _patrolArea = transform.position;

        _movement = new UnitMovement(transform);
        _fieldOfView = new FieldOfView(_viewAngle, _viewDistance, transform);

        _stateMachine = new EnemyStateMachine(this, patrol, patrol, chase, encounter);
    }
}
