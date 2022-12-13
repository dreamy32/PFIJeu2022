using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponent : MonoBehaviour
{
    private FlashlightComponent _flashlightComponent;
    public static bool FlashlightIsOn;
    private void Awake()
    {
        _flashlightComponent = GetComponentInChildren<FlashlightComponent>(true);
    }

    public void ToggleFlashlight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _flashlightComponent.gameObject.activeInHierarchy)
        {
            FlashlightIsOn = _flashlightComponent.ToggleFlashlight();
        }
    }
}
