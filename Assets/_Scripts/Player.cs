using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _playerInputActions; //TODO transfer into DI
    private bool _thrusting;
    private float _turnDirection;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    #region Enable/Disable

    private void OnEnable()
    {
        _playerInputActions.Player.Movement.performed += MovementPerformed;
    }


    private void OnDisable()
    {
        _playerInputActions.Player.Movement.performed -= MovementPerformed;
    }

    #endregion


    private void MovementPerformed(InputAction.CallbackContext callbackContext)
    {
        var readVector2 = callbackContext.ReadValue<Vector2>();
        _thrusting = readVector2.y > 0;
        _turnDirection = 0;
        if (readVector2.x < 0)
        {
            _turnDirection = 1f; print("Left");
        }
        else if (readVector2.x > 0)
        {
            _turnDirection = -1f; print("Right");
        }
    }
}
