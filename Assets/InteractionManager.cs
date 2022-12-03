using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    //
    [SerializeField] private UnityEngine.UI.Image iconPlaceholder;
    public static UnityEngine.UI.Image IconPlaceholder;
    public const string InteractionTag = "Interactable";

    private void Awake()
    {
        IconPlaceholder = iconPlaceholder;
    }

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