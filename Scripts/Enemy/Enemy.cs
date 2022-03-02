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

    //private void OnDrawGizmos()
    //{
    //    var _nodes = _patrolState.DebugNodes;

    //    if (_nodes != null)
    //    {
    //        for (int i = 0; i < _nodes.Count; i++)
    //        {
    //            if (i + 1 < _nodes.Count)
    //            {
    //                //Vector2 vector1 = new Vector2(_nodes[i].GetPosition.x - 0.5f, _nodes[i].GetPosition.y - 0.5f);
    //                //Vector2 vector2 = new Vector2(vector1.x + 1f, vector1.y + 1f);

    //                //Gizmos.DrawLine(vector1, new Vector2(vector1.x + 1f, vector1.y));
    //                //Gizmos.DrawLine(vector1, new Vector2(vector1.x, vector1.y + 1f));
    //                //Gizmos.DrawLine(vector2, new Vector2(vector2.x - 1f, vector2.y));
    //                //Gizmos.DrawLine(vector2, new Vector2(vector2.x, vector2.y - 1f));
    //                Gizmos.DrawLine(_nodes[i].GetPosition, _nodes[i + 1].GetPosition);
    //            }
    //        }
    //    }
    //}
}
