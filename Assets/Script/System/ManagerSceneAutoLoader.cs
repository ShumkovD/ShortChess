using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// マネージャーシーンの自動ロードクラス
/// </summary>
public class ManagerSceneAutoLoader
{
    //ゲーム開始時(シーン読み込み前)に実行される　
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadManagerScene()
    {
        string managerSceneName = "ManagerScene";

        //マネージャーシーンがないときだけ読み込み
        if(!SceneManager.GetSceneByName(managerSceneName).IsValid())
        {
            SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);
        }
    }
}
