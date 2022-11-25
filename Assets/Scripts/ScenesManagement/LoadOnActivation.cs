using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Map_Hosp1", LoadSceneMode.Single);
    }
}
