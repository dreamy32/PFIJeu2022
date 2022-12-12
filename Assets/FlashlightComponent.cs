using UnityEngine;

[RequireComponent(typeof(LightComponent), typeof(ItemBob), typeof(AudioSource))]
public class FlashlightComponent : MonoBehaviour
{
    private LightComponent _lightComponent;
    private AudioSource _audioSource;
    [Header("Audio")] 
    [SerializeField] private AudioClip turnOnSound;
    [SerializeField] private AudioClip turnOffSound;
    private bool _state;

    private void Awake()
    {
        _lightComponent = GetComponent<LightComponent>();
        _audioSource = GetComponent<AudioSource>();
        //
        _lightComponent.toggleOnStart = false;
    }
    
    public bool ToggleFlashlight()
    {
        _state = _lightComponent.GetState();
        _lightComponent.Toggle(!_state, true);
        //
        _audioSource.clip = _state ? turnOnSound : turnOffSound;
        _audioSource.Play();
        //
        return _state;
    }
}