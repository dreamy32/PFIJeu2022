using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SphereCollider))]
public abstract class InteractableObject : MonoBehaviour
{
    [Header(
        "You must set the tag 'Interactable' to the parent or a child of it.\n\n" +
        "An interactable object looks for 'Outline' components\nin itself and/or his children." +
        "\n*Not Required*")]
    //
    [SerializeField, Tooltip("If none, nothing will be display.")]
    private Sprite infoIcon;

    [SerializeField, FormerlySerializedAs("layerMask")]
    private LayerMask triggeringMask;

    //
    private bool _canInteract;
    private bool _isTriggered;
    protected RaycastHit RaycastHit;

    private SphereCollider _triggerCollider;

    //
    private Outline[] _outlines; //Peut avoir plus que un outline

    //
    protected bool SingleUsage = false;
    private bool _hasBeenUsed = false;

    protected virtual void Awake()
    {
        //Trigger
        _triggerCollider = GetComponent<SphereCollider>();
        if (!_triggerCollider.isTrigger)
            Debug.LogWarning("The collider of the Vault Door must have 'isTrigger' checked.");

        //Outline(s)
        _outlines = GetComponentsInChildren<Outline>();
        Debug.Log(_outlines.Length);

        //Abonnement à InteractionManager
        InteractionManager.OnInteract += () =>
        {
            //Si l'usage n'est pas unique, _hasBeenUsed sera toujours à false et on pourra toujours interragir.
            if (!_hasBeenUsed)
            {
                Interact();
            }

            //
            if (SingleUsage)
            {
                _hasBeenUsed = true;
                //On s'assure que les paramètres et outlines sont remis à false/désactiver
                _isTriggered = false;
                _canInteract = false;
                ToggleInfo(false);
            }
        };
    }

    protected void Start()
    {
        ToggleInfo(false);
    }

    protected virtual void OnTriggerEnter(Collider c)
    {
        if (!_hasBeenUsed)
        {
            if (GameTools.CompareLayers(triggeringMask, c.gameObject.layer))
            {
                _isTriggered = true;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider c)
    {
        if (!_hasBeenUsed)
        {
            if (GameTools.CompareLayers(triggeringMask, c.gameObject.layer))
            {
                _isTriggered = false;
                _canInteract = false;
                foreach (var outline in _outlines)
                    outline.enabled = false;
            }
        }
    }

    // protected virtual void Update()
    // {
    //     if (_isTriggered && !_hasBeenUsed)
    //     {
    //         var camTransform = Camera.main.transform;
    //         if (Physics.Raycast(camTransform.position, camTransform.transform.forward, out _raycastHit, Mathf.Infinity))
    //         {
    //             if (_raycastHit.collider.CompareTag(InteractionManager.InteractionTag))
    //             {
    //                 _canInteract = true;
    //                 ToggleOutlines(true);
    //             }
    //             else
    //             {
    //                 _canInteract = false;
    //                 ToggleOutlines(false);
    //             }
    //         }
    //         else
    //         {
    //             _canInteract = false;
    //             ToggleOutlines(false);
    //         }
    //     }
    // }

    protected virtual void Update()
    {
        if (!_hasBeenUsed)
        {
            if (_isTriggered)
            {
                if (Camera.main == null) //Temp fix
                    return;
                var camTransform = Camera.main.transform;
                if (Physics.Raycast(camTransform.position, camTransform.forward, out var hit,
                        2f))
                {
                    _canInteract = hit.collider.CompareTag(InteractionManager.InteractionTag) &&
                                   hit.collider != _triggerCollider;
                    ToggleInfo(_canInteract);
                }
                else
                {
                    _canInteract = false;
                    ToggleInfo(false);
                }
            }
        }
    }

    private void Interact()
    {
        if (Camera.main == null)
            return; //temp fix
        var camTransform = Camera.main.transform;
        if (_canInteract && Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit,
                2f))
        {
            if (RaycastHit.collider.CompareTag(InteractionManager.InteractionTag))
            {
                OnInteract();
            }
        }
    }

    private void ToggleInfo(bool state)
    {
        //Outlines
        foreach (var outline in _outlines)
        {
            if (outline.enabled != state)
                outline.enabled = state;
        }

        //InfoIcon
        if (infoIcon != null && InteractionManager.IconPlaceholder != null)
        {
            InteractionManager.IconPlaceholder.sprite = infoIcon;
        }

        InteractionManager.IconPlaceholder.enabled = state && infoIcon != null;
    }

    protected abstract void OnInteract();
}