using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartActiveScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "End")
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}
