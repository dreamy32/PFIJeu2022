using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    IEnumerator LoadScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
            if (asyncLoad.progress >= 0.9f)
            {
                
                asyncLoad.allowSceneActivation = true;
                yield break;
            }
        }

        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndGame")
        {
            StartCoroutine(nameof(LoadScene));
        }
    }

}
