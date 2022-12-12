using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponent : MonoBehaviour
{
    private FlashlightComponent _flashlightComponent;
    public static bool flashlightIsOn;
    private void Awake()
    {
        _flashlightComponent = GetComponentInChildren<FlashlightComponent>(true);
    }

    public void ToggleFlashlight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _flashlightComponent.gameObject.activeInHierarchy)
        {

            flashlightIsOn = _flashlightComponent.ToggleFlashlight();
            flashlightIsOn = true;
        }
        flashlightIsOn = false;
    }
}
