using Unity.Networking.Transport;
using UnityEngine;

public class NetStartGame : NetMessage
{
    //�T�[�o�[������Ă���
    public NetStartGame()
    {
        Code = OpCode.StartGame;
    }
    //�N���C�A���g�͏����K������
    public NetStartGame(DataStreamReader reader)
    {
        Code = OpCode.StartGame;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }
    public override void Deserialize(DataStreamReader reader)
    {
    }
    public override void ReceivedOnClient()
    {
        NetUtility.CStartGame?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.SStartGame?.Invoke(this, cnn);
    }
}
