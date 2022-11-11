using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Enables all the different actions of the action map
/// </summary>
/// 
[RequireComponent(typeof(CharacterController))]
public class PlayerInputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerControl playerControl;
    private PlayerControl.OnGroundActions onGround;
    private void OnEnable()
    {
        onGround.Enable();
    }
    private void OnDisable()
    {
        onGround.Disable();
    }
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerControl = new PlayerControl();
        onGround = playerControl.OnGround;
    }

    private void Update()
    {
        playerMovement.Move(onGround.Move.ReadValue<Vector2>());
    }

}