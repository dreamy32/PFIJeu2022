using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDoorComponent : InteractableObject
{
    private Animator _animator;
    bool isOpen;
    private AudioSource audio;

    [SerializeField] AudioClip openingSound;
    [SerializeField] AudioClip closingSound;

    //private static readonly int OpenTrigger = Animator.StringToHash("Open");
    protected override void Awake()
    {
        //InteractableObject attributes
        base.Awake();
        SingleUsage = false; //Éviter de call l'animator pour rien
        isOpen = false;
        _animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    private void PlaySound(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
    protected override void OnInteract()
    {
        Debug.Log("oui");
        if (!isOpen)
        {
            PlaySound(openingSound);

            _animator.SetTrigger("Open");
            _animator.ResetTrigger("Close");
            isOpen = true;
        }
        else
        {
            PlaySound(closingSound);

            _animator.SetTrigger("Close");
            _animator.ResetTrigger("Open");
            isOpen = false;
        }
    }
}
