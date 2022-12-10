using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightComponent : InteractableObject
{
    //[SerializeField] AudioClip clickSound;
    [SerializeField] GameObject playerFlashlight;

    protected override void Awake()
    {
        //InteractableObject attributes
        playerFlashlight.SetActive(false);
        base.Awake();
        SingleUsage = false; //�viter de call l'animator pour rien

    }
    private void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
    protected override void OnInteract()
    {
        playerFlashlight.SetActive(true);
        this.gameObject.SetActive(false);
        Debug.Log("oui");
     
    }
}
