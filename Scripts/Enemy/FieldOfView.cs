using UnityEngine;

public class FieldOfView 
{
    private float _viewAngle;
    private float _viewDistance;
    private LayerMask _targetMask;

    private Transform _origin;

    private Vector3 _viewDirection;

    public FieldOfView(float viewAngle, float viewDistance)
    {
        _viewAngle = viewAngle;
        _viewDistance = viewDistance;
    }

    public void SetOrigin(Transform origin) => _origin = origin;

    public void SetDirectionOfView(Vector3 direction) => 
        _viewDirection = new Vector3(direction.x * _origin.right.x, direction.y * _origin.up.y).normalized;

    public bool IsTargetSeen()
    {
        var targets = Physics2D.OverlapCircleAll(_origin.position, _viewDistance, _targetMask);

        if (targets.Length != 0)
        {
            Vector3 target = targets[0].transform.position;
            Vector3 directionToTarget = (_origin.position - target).normalized;

            if (Vector3.Angle(-_viewDirection, directionToTarget) < _viewAngle / 2f)
            {
                var distanceToTarget = Vector3.Distance(_origin.position, target);

                var rays = Physics2D.RaycastAll(_origin.position, directionToTarget, -distanceToTarget);

                rays[1].collider.TryGetComponent(out Player isPlayerHitted);

                Debug.Log(isPlayerHitted);

                return isPlayerHitted;
            }
        }

        return false;
    }
}
