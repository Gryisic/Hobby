using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    [SerializeField] private Transform _enemyCheck;
    [SerializeField] private float _checkRadius;

    private UnitMovement _movement;
    private PlayerControl _control;

    private void OnEnable()
    {
        AttachControl();
    }

    private void OnDisable()
    {
        DetachControl();
    }

    private void Awake()
    {
        _movement = new PlayerMovement(GetComponent<Transform>(), _movementSpeed);
        _control = new PlayerControl();
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = _control.Player.Move.ReadValue<Vector2>();

        _movement.Move(moveDirection);
    }

    private void Attack()
    {
        var damagableObjects = Physics2D.OverlapCircleAll(_enemyCheck.position, _checkRadius);

        foreach (var damagableObject in damagableObjects)
        {
            if (damagableObject.TryGetComponent(out IDamagable damagable) && damagable.GetType() == typeof(Unit))
            {
                InitializeBattle();
            }
        }
    }

    private void InitializeBattle()
    {
        Debug.Log("Think mazefaka");
    }

    private void AttachControl()
    {
        _control.Enable();

        _control.Player.Attack.performed += context => Attack();
    }

    private void DetachControl()
    {
        _control.Disable();

        _control.Player.Attack.performed -= context => Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_enemyCheck.position, _checkRadius);
    }
}
