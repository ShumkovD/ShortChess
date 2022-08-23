using Unity.Networking.Transport;
using UnityEngine;

public class NetWelcome : NetMessage
{
    public int AssignedTeam { set; get; }

    //�T�[�o�[������Ă���
    public NetWelcome()
    {
        Code = OpCode.Welcome;
    }
    //�N���C�A���g�͏����K������
    public NetWelcome(DataStreamReader reader)
    {
        Code = OpCode.Welcome;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(AssignedTeam);
    }
    public override void Deserialize(DataStreamReader reader)
    {
        AssignedTeam = reader.ReadInt();
    }
    public override void ReceivedOnClient()
    {
        NetUtility.CWelcome?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.SWelcome?.Invoke(this, cnn);
    }
}
