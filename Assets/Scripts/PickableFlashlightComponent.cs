using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableFlashlightComponent : InteractableObject
{
    //[SerializeField] AudioClip clickSound;
    [SerializeField] private GameObject playerFlashlight;

    protected override void Awake()
    {
        //InteractableObject attributes
        playerFlashlight.SetActive(false);
        base.Awake();
        SingleUsage = false; //�viter de call l'animator pour rien

    }
    protected override void OnInteract()
    {
        playerFlashlight.SetActive(true);
        gameObject.SetActive(false);
    }
}
