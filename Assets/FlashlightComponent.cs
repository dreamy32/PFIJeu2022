using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LightComponent), typeof(ItemBob), typeof(AudioSource))]
public class FlashlightComponent : MonoBehaviour
{
    private LightComponent _lightComponent;
    private AudioSource _audioSource;
    private AudioClip _audioClip;
    [Header("Audio")] [SerializeField] private AudioClip turnOnSound;
    [SerializeField] private AudioClip turnOffSound;
    private bool state;

    private void Awake()
    {
        _lightComponent = GetComponent<LightComponent>();
        _audioSource = GetComponent<AudioSource>();
        //
        _lightComponent.toggleOnStart = false;
        // state = _lightComponent.GetState();
    }
    
    public bool ToggleFlashlight()
    {
        state = _lightComponent.GetState();
        _audioClip = state ? turnOnSound : turnOffSound;
        _lightComponent.Toggle(!state, true);
        _audioSource.Play();
        return state;
    }
}