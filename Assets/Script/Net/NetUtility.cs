using System;
using UnityEngine;
using Unity.Networking.Transport;


public enum OpCode
{
    KeepAlive = 1,
    Welcome = 2,
    StartGame = 3,
    MakeMove = 4,
    Rematch = 5,
}


public static class NetUtility
{
    public static void OnData(DataStreamReader stream,NetworkConnection cnn, Server server = null)
    {
        NetMessage msg = null;
        var opCode = (OpCode)stream.ReadByte();
        switch (opCode)
        {
            case OpCode.KeepAlive:  msg = new NetKeepAlive(stream); break; 
            case OpCode.Welcome:  msg = new NetWelcome(stream); break; 
            case OpCode.StartGame:  msg = new NetStartGame(stream); break; 
            case OpCode.MakeMove:  msg = new NetMakeMove(stream); break; 
            case OpCode.Rematch:  msg = new NetRematch(stream); break;
            default:
                    break;
                
        }

        if (server != null)
            msg.ReceivedOnServer(cnn);
        else msg.ReceivedOnClient();

    }

    //クライアント
    public static Action<NetMessage> CKeepAlive;
    public static Action<NetMessage> CWelcome;
    public static Action<NetMessage> CStartGame;
    public static Action<NetMessage> CMakeMove;
    public static Action<NetMessage> CRematch;
    //サーバー
    public static Action<NetMessage, NetworkConnection> SKeepAlive;
    public static Action<NetMessage, NetworkConnection> SWelcome;
    public static Action<NetMessage, NetworkConnection> SStartGame;
    public static Action<NetMessage, NetworkConnection> SMakeMove;
    public static Action<NetMessage, NetworkConnection> SRematch;
}
