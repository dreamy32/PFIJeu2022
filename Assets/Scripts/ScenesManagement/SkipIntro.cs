using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SkipIntro : MonoBehaviour
{
    [SerializeField] Text skipText;
    [SerializeField] string sceneToLoad;
    private void Awake()
    {
        skipText.gameObject.SetActive(false);
        StartCoroutine(nameof(LoadScene));
    }
    IEnumerator LoadScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
            if (asyncLoad.progress >= 0.9f)
            {
                skipText.gameObject.SetActive(true);
                yield return new WaitUntil(() => Input.anyKeyDown);
                asyncLoad.allowSceneActivation = true;
                yield break;
            }
        }

        yield return null;
    }
}
