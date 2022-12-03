using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public const string InteractionTag = "Interactable";

    //
    public delegate void InteractAction();

    public static event InteractAction OnInteract;

    //
    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            OnInteract?.Invoke();
    }
}