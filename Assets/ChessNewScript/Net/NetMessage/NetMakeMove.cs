using Unity.Networking.Transport;

public class NetMakeMove : NetMessage
{
    public int originalX;
    public int originalY;
    public int destinationX;
    public int destinationY;
    public int teamID;

    //�T�[�o�[������Ă���
    public NetMakeMove()
    {
        Code = OpCode.MakeMove;
    }
    //�N���C�A���g�͏����K������
    public NetMakeMove(DataStreamReader reader)
    {
        Code = OpCode.MakeMove;
        Deserialize(reader);
    }
    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(originalX);
        writer.WriteInt(originalY);
        writer.WriteInt(destinationX);
        writer.WriteInt(destinationY);
        writer.WriteInt(teamID);
    }
    public override void Deserialize(DataStreamReader reader)
    {
        originalX = reader.ReadInt();
        originalY = reader.ReadInt();
        destinationX = reader.ReadInt();
        destinationY = reader.ReadInt();
        teamID = reader.ReadInt();
    }
    public override void ReceivedOnClient()
    {
        NetUtility.CMakeMove?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.SMakeMove?.Invoke(this, cnn);
    }

}
