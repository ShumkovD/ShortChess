using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンの親クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T :MonoBehaviour
{
    protected static T instance;

    //シングルトンのinstanceを返す
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if(instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                  " is needed in the scene, but there is none.");
                }
            }
            return instance;
        }
    }
}
