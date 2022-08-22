using Unity.Networking.Transport;

public class NetKeepAlive : NetMessage
{
    //�T�[�o�[������Ă���
    public NetKeepAlive()
    {
        Code = OpCode.KeepAlive;
    }
    //�N���C�A���g�͏����K������
    public NetKeepAlive(DataStreamReader reader)
    {
        Code = OpCode.KeepAlive;
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
        NetUtility.CKeepAlive?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.SKeepAlive?.Invoke(this, cnn);
    }
}

