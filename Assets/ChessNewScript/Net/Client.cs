using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using System;

public class Client : MonoSingleton<Client>
{
    public NetworkDriver driver;
    private NetworkConnection connection;

    private bool isActive = false;

    public Action connectionDropped;

    public void Initiate(string ip, ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.Parse(ip, port);

        connection = driver.Connect(endpoint);

        Debug.Log("Attempting to connect to " + endpoint.Address);

        isActive = true;

        RegisterToEvent();
    }
    public void Shutdown()
    {
        if (isActive)
        {
            UnregisterToEvent();
            driver.Dispose();
            isActive = false;
            connection = default(NetworkConnection);
        }
    }

    public void OnDestroy()
    {
        Shutdown();
    }

    public void Update()
    {
        if (!isActive)
            return;

        CheckAlive();

        driver.ScheduleUpdate().Complete();
        
        UpdateMessagePump();
    }

    private void UpdateMessagePump()
    {
        DataStreamReader stream;
        NetworkEvent.Type cmd;
        while ((cmd = connection.PopEvent(driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                SendToServer(new NetWelcome());
            }
            else if(cmd == NetworkEvent.Type.Data)
            {
                NetUtility.OnData(stream, default(NetworkConnection));
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client disconnected from the server");
                connection = default(NetworkConnection);
                connectionDropped?.Invoke();
            }
        }
    }

    //Client
    private void CheckAlive()
    {
        if(!connection.IsCreated && isActive)
        {
            Debug.Log("Lost connection to the server");
            connectionDropped?.Invoke();
            Shutdown();
        }
    }

    public void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }

    private void RegisterToEvent()
    {
        NetUtility.CKeepAlive += OnKeepAlive;
    }
    private void UnregisterToEvent()
    {
        NetUtility.CKeepAlive -= OnKeepAlive;
    }

    private void OnKeepAlive(NetMessage nm)
    {
        SendToServer(nm);
    }
}
