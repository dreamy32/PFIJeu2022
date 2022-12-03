using System;
using System.Collections.Generic;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    [Header("The object looks for the 'Lights' in his children.")]
    [SerializeField, Tooltip("If checked, lights will be turned on.")]
    private bool state = true;

    private bool _actualState;
    private Light[] _lights;

    private void Awake()
    {
        _lights = GetComponentsInChildren<Light>();
        Toggle(state);
    }

    public void Toggle(bool state)
    {
        _actualState = state;
        //Jouer un son ?
        foreach (var light in _lights)
        {
            light.enabled = state;
        }
    }

    private void Update()
    {
        if(state != _actualState)
            Toggle(state);
    }
}