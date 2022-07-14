using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadClass : Singleton<SceneLoadClass>
{
    /// <summary>
    /// �V���O���g��
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
    /// �V�[�����
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

    //�V�[���̉��Z
    public void SceneLoadAdditive(string code)
    {
        if(sceneDictionary.TryGetValue(code, out var sceneData))
        {
            if (SceneManager.GetSceneByName(sceneData.sceneName).IsValid())
            {
                return;
            }
            Debug.LogWarning(sceneData.sceneName);

            ActivateLoadingScene(sceneData.sceneName);
        }
        else
        {
            Debug.LogError("SceneChange is failed");
        }
    }

    //�V�[����Active�؂�ւ�
    public void SceneSetActive(string code,bool active)
    {
        //������
    }

    //�V�[���̔j��
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
    /// Loading���
    /// </summary>
    [Header("LoadingPanel")]
    [SerializeField] GameObject nowLoadingObj;
    [SerializeField] Slider progress;
    [SerializeField] Text loadText;
    AsyncOperation async;

    void LoadInit()
    {
        //Instantiate(nowLoadingObj);
        progress = nowLoadingObj.GetComponentInChildren<Slider>();
        loadText = nowLoadingObj.GetComponentInChildren<Text>();
        loadText.text = "NowLoading...";
        nowLoadingObj.SetActive(false);
    }
    void ActivateLoadingScene(string name)
    {
        //���[�h��ʂ�\��
        nowLoadingObj.SetActive(true);
        Debug.LogWarning("NowLoading");
        Debug.LogWarning(nowLoadingObj.activeSelf);

        StartCoroutine(LoadNextScene(name));
    }

    IEnumerator LoadNextScene(string name)
    {
        async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while(!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            progress.value = progressVal;
            yield return null;
        }

        if (async.isDone)
        {
            yield return new WaitForSeconds(0.2f);
            nowLoadingObj.SetActive(false);
            progress.value = 0f;
            yield break;
        }
    }
}
