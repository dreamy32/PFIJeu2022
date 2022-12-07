using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    [Header("The object looks for the 'Lights' in his children.")]
    [SerializeField, Tooltip("If checked, lights will be turned on Awake.")]
    private bool state = true;

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
        _lights = GetComponentsInChildren<Light>();
        _renderers = GetComponentsInChildren<Renderer>();
        Toggle(state);
        //
        if (flickOnAwake)
            Flick(time, flickDuration);
    }

    /// <summary>
    /// Turn on/off all the lights
    /// </summary>
    /// <param name="state">The desired state to affect to all lights children.</param>
    public void Toggle(bool state)
    {
        this.state = state;
        //Jouer un son ?
        for (var i = 0; i < _lights.Length; i++)
        {
            _lights[i].enabled = state;
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
            Toggle(!state);
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
        return new List<LightComponent>(instances);
    }
}