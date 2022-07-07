using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityManager : Singleton<EntityManager>
{
    [SerializeField] string[] startScene;
    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
    }

    
    private void Start()
    {
        for (int i = 0; i < startScene.Length; ++i)
        {
            SceneLoadClass.instance.SceneLoadAdditive(startScene[i]);
        }
    }

}
