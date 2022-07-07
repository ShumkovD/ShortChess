using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadClass : Singleton<SceneLoadClass>
{
    /// <summary>
    /// シングルトン
    /// </summary>
    private void Awake()
    {
        if(this != Instance)
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        SceneInit();
        LoadInit();
    }

    /// <summary>
    /// シーン情報
    /// </summary>
    [System.Serializable]
    public class SceneData
    {
        public string codeName;
        public string sceneName;
        public int index;
    }
    [SerializeField] SceneData[] sceneDatas;
    Dictionary<string, SceneData> sceneDictionary = new Dictionary<string, SceneData>();

    void SceneInit()
    {
        foreach (SceneData s in sceneDatas)
        {
            sceneDictionary.Add(s.codeName, s);
        }
    }

    //シーンの加算
    public void SceneLoadAdditive(string code,bool isLoad)
    {
        if(sceneDictionary.TryGetValue(code, out var sceneData))
        {
            if (SceneManager.GetSceneByName(sceneData.sceneName).IsValid())
            {
                return;
            }
            Debug.LogWarning(sceneData.sceneName);

            async = SceneManager.LoadSceneAsync(sceneData.sceneName, LoadSceneMode.Additive);

            if (isLoad)
                ActivateLoadingScene();
        }
        else
        {
            Debug.LogError("SceneChange is failed");
        }
    }

    //シーンのActive切り替え
    public void SceneSetActive(string code,bool active)
    {
        //未実装
    }

    //シーンの破棄
    public void UnLoadScene(string code, bool unloadAssets)
    {
        if (sceneDictionary.TryGetValue(code, out var sceneData))
        {
            SceneManager.UnloadSceneAsync(sceneData.sceneName);

            if(unloadAssets)
            {
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Debug.LogWarning("UnUsedAssets didn't Unload");
            }
        }
        else
        {
            Debug.LogError("SceneUnLoad is failed");
        }
    }

    /// <summary>
    /// Loading画面
    /// </summary>
    [Header("LoadingPanel")]
    [SerializeField] GameObject nowLoadingObj;
    [SerializeField] Slider progress;
    [SerializeField] Text loadText;
    AsyncOperation async;

    void LoadInit()
    {
        Instantiate(nowLoadingObj);
        nowLoadingObj.SetActive(false);
        progress = nowLoadingObj.GetComponentInChildren<Slider>();
        loadText = nowLoadingObj.GetComponentInChildren<Text>();
        loadText.text = "NowLoading...";
    }
    void ActivateLoadingScene()
    {
        //ロード画面を表示
        nowLoadingObj.SetActive(true);
        Debug.LogWarning("NowLoading");

        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        while(!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            progress.value = progressVal;
            yield return new WaitForSeconds(1.0f);
        }

        if (async.isDone)
        {
            nowLoadingObj.SetActive(false);
            yield break;
        }
    }
}
