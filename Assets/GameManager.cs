using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Transform> flashlightSpawns;

    [SerializeField] GameObject flashlight;

    //LightComponent
    [SerializeField] private bool initGlobalPowerState;

    //Attributs statiques
    private static bool _globalPowerState;

    //Light components instances
    private static List<LightComponent> _lightComponents;
    private static List<LightSwitchComponent> _lightSwitchComponents;

    public static bool GlobalPowerState
    {
        get => _globalPowerState;
        set
        {
            _globalPowerState = value;
            //Iterate through all the light components
            foreach (var light in _lightComponents)
            {
                //If the global power state is on
                if (value)
                {
                    //Iterate through all light switch components
                    foreach (var lightSwitch in _lightSwitchComponents)
                    {
                        //If the light component is associated with the light switch
                        if (lightSwitch.lightComponent.Equals(light))
                        {
                            //Toggle the light according to the switch state
                            light.Toggle(lightSwitch.switchState, true);
                        }
                        else
                            //Toggle the light according to the global power state
                            light.Toggle(value, true);
                    }
                }
                else
                    //Toggle the light according to the global power state (always false)
                    light.Toggle(value, true);
            }
        }
    }

    private void Awake()
    {
        _lightComponents = LightComponent.GetLights();
        _lightSwitchComponents = LightSwitchComponent.GetLightSwitches();
        //
        SpawnFlashlight();
    }

    private void Start()
    {
        GlobalPowerState = initGlobalPowerState;
    }

    void SpawnFlashlight()
    {
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