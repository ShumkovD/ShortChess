using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ActionCKey();
        }
    }

    void ActionCKey()
    {
        SceneLoadClass.Instance.SceneLoadAdditive("�{�[�h", true);
    }
}
