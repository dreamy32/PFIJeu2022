using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    //Custom Event
    public delegate void InteractAction();
    public static event InteractAction OnInteract;
    //
    [SerializeField] private UnityEngine.UI.Image iconPlaceholder;
    public static UnityEngine.UI.Image IconPlaceholder;
    public const string InteractionTag = "Interactable";

    private void Awake()
    {
        IconPlaceholder = iconPlaceholder;
    }

    //
    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            OnInteract?.Invoke();
    }
}