using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{

    void Start()
    {
        // Je sais...
        SkipIntro.AllowActivation = true;
        Debug.Log(SkipIntro.AllowActivation);
    }

}
