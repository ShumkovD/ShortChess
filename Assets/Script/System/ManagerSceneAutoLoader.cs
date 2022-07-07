using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �}�l�[�W���[�V�[���̎������[�h�N���X
/// </summary>
public class ManagerSceneAutoLoader
{
    //�Q�[���J�n��(�V�[���ǂݍ��ݑO)�Ɏ��s�����@
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadManagerScene()
    {
        string managerSceneName = "ManagerScene";

        //�}�l�[�W���[�V�[�����Ȃ��Ƃ������ǂݍ���
        if(!SceneManager.GetSceneByName(managerSceneName).IsValid())
        {
            SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);
        }
    }
}
