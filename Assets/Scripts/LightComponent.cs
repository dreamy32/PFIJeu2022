using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class LightComponent : MonoBehaviour
{
    [Header("The object looks for the 'Lights' in his children.")]
    [SerializeField, Tooltip("If checked, lights will be turned on Awake.")]
    private bool state;

    [SerializeField] LayerMask triggeringMask;

    [Header("Materials")]
    //
    [SerializeField]
    private Material materialOn;

    [SerializeField] private Material materialOff;
    [Header("Flicking")] [SerializeField] private bool flickOnAwake;
    [SerializeField] private float time;
    [SerializeField] private float flickDuration;
    private Light[] _lights;
    private Renderer[] _renderers;
    private static List<LightComponent> instances;

    private void Awake()
    {
        //??= est l'équivalent de faire une condition pour 'checker' si c'est null avant d'instancier l'objet 
        instances ??= new List<LightComponent>();
        instances.Add(this);
        //
        _lights = GetComponentsInChildren<Light>(true);
        _renderers = GetComponentsInChildren<Renderer>();
        //
        if (flickOnAwake)
            Flick(time, flickDuration);
        Toggle(state);
    }

    private void Reset()
    {
        triggeringMask = LayerMask.GetMask("Player");
        GetComponent<BoxCollider>().isTrigger = true;
        materialOn =
            Resources.Load<Material>(
                "Dnk_Dev/HospitalHorrorPack/Models/Materials/Mat_Lamp");
        materialOff =
            Resources.Load<Material>(
                "Dnk_Dev/HospitalHorrorPack/Models/Materials/Mat_Lamp_Off");
    }

    private void OnDestroy()
    {
        instances = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameTools.CompareLayers(triggeringMask, other.gameObject.layer))
            SanitySystem.InTheDark = !state;
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameTools.CompareLayers(triggeringMask, other.gameObject.layer))
            SanitySystem.InTheDark = !state;

        Debug.Log(PlayerComponent.FlashlightIsOn);
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameTools.CompareLayers(triggeringMask, other.gameObject.layer))
            SanitySystem.InTheDark = false;
    }

    public bool GetState() => state;

    /// <summary>
    /// Turn on/off all the lights
    /// </summary>
    /// <param name="state">The desired state to affect to all lights children.</param>
    /// <param name="ignoreGlobalPstate">Ignore the global power state. Should only be used for objects like a flashlight that are independant.</param>
    public void Toggle(bool state, bool ignoreGlobalPstate = false)
    {
        if (!GameManager.GlobalPowerState && !ignoreGlobalPstate)
        {
            Debug.LogWarning(
                $"Can't pursue action because there is no power. Try affecting the state to {nameof(GameManager.GlobalPowerState)} instead or set {nameof(ignoreGlobalPstate)} to true.");
            return;
        }

        this.state = state;
        //Jouer un son ?
        for (var i = 0; i < _lights.Length; i++)
        {
            _lights[i].enabled = state;
            if (materialOn != null && materialOff != null)
                _renderers[i].material = state ? materialOn : materialOff;
        }
    }

    //Flicking
    private bool _stateBeforeFlicking;
    private Coroutine _flickerRoutine;

    /// <summary>
    /// Flick lights over a period of time.
    /// </summary>
    /// <param name="flickTime">The time between each flick.</param>
    /// <param name="duration">The duration of the flicking.</param>
    /// <param name="endState">The state the lights after the flicking.</param>
    public void Flick(float flickTime, float duration, bool? endState = null)
    {
        _flickerRoutine = StartCoroutine(FlickerRoutine(flickTime, duration, endState));
    }


    private IEnumerator FlickerRoutine(float flickTime, float duration, bool? endState = null)
    {
        _stateBeforeFlicking = state;
        StartCoroutine(FlickerStopAfterDuration(duration, endState));
        while (true)
        {
            Toggle(!state, true);
            yield return new WaitForSeconds(flickTime);
        }
    }

    private IEnumerator FlickerStopAfterDuration(float duration, bool? endState = null)
    {
        yield return new WaitForSeconds(duration);
        StopCoroutine(_flickerRoutine);
        //Équivaut à endState != null ? endState.Value : _stateBeforeFlicking
        Toggle(endState ?? _stateBeforeFlicking);
    }

    /// <summary>
    /// For the other scripts to get a copy of the instances list and work with them.
    /// </summary>
    /// <returns>A copy of the instances list.</returns>
    public static List<LightComponent> GetLights()
    {
        if (instances == null)
        {
            Debug.LogError($"There are no {nameof(LightComponent)} in the scene.");
            return null;
        }

        //
        return new List<LightComponent>(instances);
    }
}