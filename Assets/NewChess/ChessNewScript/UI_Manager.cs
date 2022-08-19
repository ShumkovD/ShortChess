using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Manager : Singleton<UI_Manager>
{
    //シングルトン
    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        for (int i = 0; i < sceneNames.Length; ++i)
        {
            canvasList.Add(sceneNames[i], canvases[i]);
            canvases[i].SetActive(false);
        }
    }

    string currentActiveScene;

    [SerializeField] string[] sceneNames;
    [SerializeField] GameObject[] canvases;
    Dictionary<string, GameObject> canvasList = new Dictionary<string, GameObject>();
    GameObject currentActiveCanvas;

    private void Start()
    {
        
    }

    public void SetActiveScene(string name)
    {
        ActivateCanvas(name);
    }

    void ActivateCanvas(string name)
    {  
        if(canvasList.TryGetValue(name,out var canvas))
        {
            if (currentActiveCanvas != null) { currentActiveCanvas.SetActive(false); }

            canvas.SetActive(true);
            currentActiveCanvas = canvas;
        }
        else
        {
            Debug.LogWarning($"There is no UIobj such a : {name}");
        }
    }

    public void OnClickStartButton()
    {
        SceneLoadClass.Instance.SceneLoadAdditive("Game");
        SceneLoadClass.Instance.UnLoadScene("タイトル");
    }
}
