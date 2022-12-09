using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDoorComponent : InteractableObject
{
    private Animator _animator;
    private static readonly int OpenTrigger = Animator.StringToHash("Open");

    protected override void Awake()
    {
        //InteractableObject attributes
        base.Awake();
        SingleUsage = true; //Éviter de call l'animator pour rien
        _animator = GetComponent<Animator>();
    }

    protected override void OnInteract()
    {
        Debug.Log("oui");
        _animator.SetTrigger("Open");
    }
}
