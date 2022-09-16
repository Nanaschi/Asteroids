using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _playerInputActions; //TODO transfer into DI

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
        Debug.Log("Something");
        print(callbackContext.ReadValue<Vector2>());
    }
}
