using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    public Server server;
    public Client client;

    [SerializeField] private Animator       menuAnimator;
    [SerializeField] private TMP_InputField addressInput;

    private void Awake()
    {
        Instance = this;
    }

    public void OnLocalGameButton()
    {
        server.Initiate(1050);
        client.Initiate("127.0.0.1", 1050);
        menuAnimator.SetTrigger("InGameMenu");
    }

    public void OnOnlineGameButton()
    {
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

}
