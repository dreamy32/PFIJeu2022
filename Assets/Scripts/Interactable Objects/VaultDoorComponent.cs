using UnityEngine;

[RequireComponent(typeof(Animator), typeof(VaultDoorAnimEvents))]
public class VaultDoorComponent : InteractableObject
{
    private Animator _animator;
    private static readonly int OpenTrigger = Animator.StringToHash("Open");

    protected override void Awake()
    {
        //InteractableObject attributes
        base.Awake();
        SingleUsage = false; //Éviter de call l'animator pour rien
        //
        _animator = GetComponent<Animator>();
    }

    protected override void OnInteract()
    {
        _animator.SetTrigger(OpenTrigger);
    }
}