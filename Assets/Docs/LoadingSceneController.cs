using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private string defaultScene = "Home";
    [SerializeField] private Slider loadingProgress;
    [SerializeField] private TextMeshProUGUI loadingText;
    public static string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            sceneToLoad = defaultScene;
        }
        StartCoroutine(LoadingScene());
    }

    public IEnumerator LoadingScene()
    {
        loadingProgress.value = 0;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;
        float progress = 0;

        while (!asyncLoad.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncLoad.progress, Time.deltaTime);
            loadingProgress.value = progress;
            loadingText.text = "Loading..." + (loadingProgress.value* 100).ToString("0") + "%";
            if(progress >= 0.9f)
            {
                loadingProgress.value = 1;
                loadingText.text = "Loading...100%";
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
