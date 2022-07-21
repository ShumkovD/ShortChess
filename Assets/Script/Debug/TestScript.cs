using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.Play("BGM");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ActionCKey();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneLoadClass.Instance.SceneLoadAdditive("test");
        }


        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.Play("クリック");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isMute = SoundManager.Instance.settings.mute;
            if (isMute)
            {
                SoundManager.Instance.settings.mute = false;
            }
            else
            {
                SoundManager.Instance.settings.mute = true;
            }
            SoundManager.Instance.SwitchMute();
        }
    }

    void ActionCKey()
    {
        SceneLoadClass.Instance.SceneLoadAdditive("ボード");
    }
}