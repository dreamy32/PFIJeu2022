using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] List<Transform> flashlightSpawns;
    [SerializeField] GameObject flashlight;
    void Awake()
    {
        SpawnFlashlight();
    }

    void SpawnFlashlight()
    {
        Transform spawn = flashlightSpawns[Random.Range(0, flashlightSpawns.Count)];
        spawn.gameObject.SetActive(true);

    }
}
