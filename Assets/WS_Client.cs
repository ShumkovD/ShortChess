using WebSocketSharp;
using UnityEngine;

public class WS_Client : MonoBehaviour
{
    WebSocket ws;
    void Start()
    {
        ws = new WebSocket("ws://10.40.80.10:8080");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message received from " + ((WebSocket)sender).Url + ", Data: " + System.Text.Encoding.ASCII.GetString(e.RawData));
        };
        ws.Connect();
    } 

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
            return;

        if(Input.GetKeyDown(KeyCode.P))
        {
            ws.Send("Hello");
        }
    }
}
