using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponent : MonoBehaviour
{
    private FlashlightComponent _flashlightComponent;
    private void Awake()
    {
        _flashlightComponent = GetComponentInChildren<FlashlightComponent>(true);
    }

    public void ToggleFlashlight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _flashlightComponent.gameObject.activeInHierarchy)
        {
            _flashlightComponent.ToggleFlashlight();
        }
    }
}
