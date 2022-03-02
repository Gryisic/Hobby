using UnityEngine;
using System.Linq;

public class FieldOfView 
{
    private float _viewAngle;
    private float _viewDistance;
    private int _targetMask;

    private Transform _origin;

    private Vector3 _viewDirection;

    public ITarget Target { get; private set; }

    public FieldOfView(float viewAngle, float viewDistance, Transform origin)
    {
        _viewAngle = viewAngle;
        _viewDistance = viewDistance;
        _origin = origin;
        _targetMask = (1 << LayerMask.NameToLayer(typeof(Player).ToString()));
    }

    public void SetOrigin(Transform origin) => _origin = origin;

    public void SetDirectionOfView(Vector3 direction) => 
        _viewDirection = new Vector3(direction.x * _origin.right.x, direction.y * _origin.up.y).normalized;

    public bool IsTargetSeen()
    {
        var possibleTargets = Physics2D.OverlapCircleAll(_origin.position, _viewDistance, _targetMask);

        if (possibleTargets.Length != 0)
        {
            var concreteTarget = possibleTargets[0].transform;
            var directionToTarget = (_origin.position - concreteTarget.position).normalized;

            if (Vector3.Angle(-_viewDirection, directionToTarget) < _viewAngle / 2f)
            {
                var distanceToTarget = Vector3.Distance(_origin.position, concreteTarget.position);

                var target = MathExtention.TryGetComponentFromIgnoredRaycast<ITarget>(_origin.position, directionToTarget, 
                    -distanceToTarget, 1);

                if (Target == null) Target = target;

                return target != null;
            }
        }

        if (Target != null) Target = null;

        return false;
    }
}
