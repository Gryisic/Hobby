using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Enemy : Unit
{
    [Header("Patrol")]
    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _patrolSpeed;
    private Vector2 _patrolArea;

    [Header("Chase")]
    [SerializeField] private float _chaseSpeed;

    [Header("Field Of View")]
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewDistance;

    private FieldOfView _fieldOfView;

    private Patrol _patrolState;

    public void Move(Vector2 destination, float speed)
    {
        _movement.Move(destination, speed);

        _fieldOfView.SetOrigin(transform);
        _fieldOfView.SetDirectionOfView(destination - (Vector2)transform.position);
    }

    private void Awake() => Initialize();

    private void Initialize()
    {
        _patrolArea = transform.position;

        _movement = new UnitMovement(transform, _patrolSpeed);

        _fieldOfView = new FieldOfView(_viewAngle, _viewDistance);
        _patrolState = new Patrol(this, _patrolArea, _patrolRadius, _patrolSpeed);
        _stateMachine = new StateMachine(_patrolState);
    }
}
