using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
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
            SceneManager.LoadSceneAsync(sceneData.sceneName, LoadSceneMode.Additive);
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
}
