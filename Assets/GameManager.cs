using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Transform> flashlightSpawns;

    [SerializeField] GameObject flashlight;

    //LightComponent
    [SerializeField] private bool initGlobalPowerState;

    //Attributs statiques
    private static bool _globalPowerState;

    public static bool GlobalPowerState
    {
        get => _globalPowerState;
        set
        {
            _globalPowerState = value;
            //
            foreach (var light in LightComponent.GetLights())
                light.Toggle(value);
        }
    }

    private void Awake()
    {
        SpawnFlashlight();
    }

    private void Start()
    {
        GlobalPowerState = initGlobalPowerState;
    }

    void SpawnFlashlight()
    {
        if (SceneManager.GetActiveScene().name.Equals("Lighting Preview"))
            return; //Delete this line at release
        Transform spawn = flashlightSpawns[Random.Range(0, flashlightSpawns.Count)];
        spawn.gameObject.SetActive(true);
    }

    //Light Management
    public static void ToggleAllLights(bool state)
    {
        if (!GlobalPowerState)
        {
            Debug.LogWarning(
                $"Can't pursue action because there is no power. Try affecting the state to {GlobalPowerState} instead.");
        }

        //
        foreach (var light in LightComponent.GetLights())
            light.Toggle(state);
    }

    public static void FlickAllLights(float flickTime, float duration, bool? endState = null)
    {
        foreach (var light in LightComponent.GetLights())
            light.Flick(flickTime, duration, endState);
    }
}