using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public GameObject loadingInterface;
    public Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    void Start()
    {
        scenesToLoad.Clear();
    }

    public void StartScene(string sceneName)
    {
        HideMenu();
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingProgressBar.fillAmount == 1)
            loadingInterface.SetActive(false);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            //scenesToLoad[i].allowSceneActivation = false;
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                Debug.Log(loadingProgressBar.fillAmount);
                yield return null;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
