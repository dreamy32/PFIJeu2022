using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SkipIntro : MonoBehaviour
{
    [SerializeField] private Text skipText;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Text endOfIntro;

    public static bool AllowActivation;
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
                
                yield return new WaitUntil(() => Input.anyKeyDown || endOfIntro.isActiveAndEnabled );
                asyncLoad.allowSceneActivation = true;

                yield break;
            }
        }
        yield return null;
    }
}
