using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name.Equals("Lighting Preview"))
            return; //Delete this line at release
        Transform spawn = flashlightSpawns[Random.Range(0, flashlightSpawns.Count)];
        spawn.gameObject.SetActive(true);
    }
}