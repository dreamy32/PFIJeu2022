using System.Collections.Generic;
using UnityEditor;
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

    private static readonly int IsOn = Animator.StringToHash("isOn");
    private static readonly int OnAwakeTrigger = Animator.StringToHash("OnAwake");

    [HideInInspector] public bool switchState;

    private bool _onAwake = true;
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
        //
        switchState = lightComponent.GetState();
        //Set the state of the switch by the animator on Awake
        _animator.SetTrigger(OnAwakeTrigger);
    }

    protected override void Start()
    {
        base.Start();
        _animator.SetBool(IsOn, switchState);
    }

    private void OnDestroy()
    {
        instances = null;
    }

    protected override void Reset()
    {
        base.Reset();
        GetComponent<AudioSource>().playOnAwake = false;
        turnOnSound = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Audio/LightSwitch/lightswitch-on.ogg");

    }
    protected override void OnInteract()
    {
        _animator.SetBool(IsOn, !switchState);
    }

    public void InteractAnimEventOn()
    {
        switchState = true;
        _audioSource.clip = turnOnSound;
        lightComponent.Toggle(switchState);
        //
        if (_onAwake)
        {
            _onAwake = false;
            _animator.ResetTrigger(OnAwakeTrigger);
            return;
        }
        _audioSource.Play();
    }

    public void InteractAnimEventOff()
    {
        switchState = false;
        _audioSource.clip = turnOffSound;
        lightComponent.Toggle(switchState);
        //
        if (_onAwake)
        {
            _onAwake = false;
            _animator.ResetTrigger(OnAwakeTrigger);
            return;
        }
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