using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "GameStage";

    public static Title Instance;

    private SaveNLoad theSaveNLoad;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void ClickStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {
        Debug.Log("로드");

        StartCoroutine(LoadCoroutine());
        SceneManager.LoadScene(sceneName);

        theSaveNLoad.LoadData();
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            yield return null;
        }

        theSaveNLoad = FindAnyObjectByType<SaveNLoad>();
        theSaveNLoad.LoadData();

        Destroy(gameObject);
    }

    public void ClickExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
