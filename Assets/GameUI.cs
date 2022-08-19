using TMPro;
using UnityEngine;
using System;

public enum CameraAngle
{
    Menu = 0,
    WhiteTeam = 1,
    BlackTeam = 2,
}

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    public Server server;
    public Client client;

    [SerializeField] private Animator       menuAnimator;
    [SerializeField] private TMP_InputField addressInput;
    [SerializeField] private GameObject[]   cameraAngles;

    public Action<bool> setLocalGame;

    private void Awake()
    {
        Instance = this;
        RegisterEvents();
    }

    public void OnLocalGameButton()
    {
        server.Initiate(1050);
        setLocalGame?.Invoke(true);
        client.Initiate("127.0.0.1", 1050);
        menuAnimator.SetTrigger("InGameMenu");
    }

    public void OnOnlineGameButton()
    {
        setLocalGame?.Invoke(false);
        menuAnimator.SetTrigger("OnlineMenu");
    }

    public void OnOnlineHostButton()
    {
        server.Initiate(1050);
        client.Initiate("127.0.0.1", 1050);
        menuAnimator.SetTrigger("HostMenu");
    }

    public void OnOnlineConnectButton()
    {
        client.Initiate(addressInput.text, 1050);
    }

    public void OnOnlineBackButton()
    {
        menuAnimator.SetTrigger("StartMenu");
    }

    public void OnHostBackButton()
    {
        server.Shutdown();
        client.Shutdown();
        menuAnimator.SetTrigger("OnlineMenu");
    }


    public void OnLeaveFromGameMenu()
    {
        SetCameraToTeam(CameraAngle.Menu);
        menuAnimator.SetTrigger("StartMenu");
    }
    public void SetCameraToTeam(CameraAngle angle)
    {
        for(int i = 0; i < cameraAngles.Length; i++)
            cameraAngles[i].SetActive(false);
        cameraAngles[(int)angle].SetActive(true);
    }

    private void RegisterEvents()
    {
        NetUtility.CStartGame += OnStartGameClient;
    }
    private void UnregisterEvents()
    {
        NetUtility.CStartGame -= OnStartGameClient;
    }

    private void OnStartGameClient(NetMessage msg)
    {
        menuAnimator.SetTrigger("InGameMenu");
    }

}
