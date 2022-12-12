using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource), typeof(Outline))]
public class LightSwitchComponent : InteractableObject
{
    public LightComponent lightComponent;

    [Header("Audio")]
    //
    [SerializeField]
    private AudioClip turnOnSound;

    [SerializeField] private AudioClip turnOffSound;
    private Animator _animator;

    private AudioSource _audioSource;
    private Outline _outline;

    private AudioClip _audioClip;
    private static readonly int IsOn = Animator.StringToHash("isOn");

    public bool switchState;
    private static List<LightSwitchComponent> instances;

    protected override void Awake()
    {
        base.Awake();
        //
        instances ??= new List<LightSwitchComponent>();
        instances.Add(this);
        //
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _outline = GetComponent<Outline>();
        //
        switchState = lightComponent.GetState();
        _animator.SetBool(IsOn, switchState);
    }

    private void OnDestroy()
    {
        instances = null;
    }

    protected override void OnInteract()
    {
        _animator.SetBool(IsOn, !switchState);
    }

    public void InteractAnimEventOn()
    {
        switchState = true;
        _audioClip = turnOnSound;
        lightComponent.Toggle(switchState);
        _audioSource.Play();
    }

    public void InteractAnimEventOff()
    {
        switchState = false;
        _audioClip = turnOffSound;
        lightComponent.Toggle(switchState);
        _audioSource.Play();
    }

    public static List<LightSwitchComponent> GetLightSwitches()
    {
        if (instances == null)
        {
            Debug.LogError($"There are no {nameof(LightSwitchComponent)} in the scene.");
            return null;
        }

        //
        return new List<LightSwitchComponent>(instances);
    }
}