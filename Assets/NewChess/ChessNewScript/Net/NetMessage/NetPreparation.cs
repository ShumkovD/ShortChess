using Unity.Networking.Transport;


public class NetPreparation : NetMessage
{
    public int teamID;
    public byte areReady;
    public int positionX;
    public int positionY;
    public int destinationX;
    public int destinationY;
    public NetPreparation()
    {
        Code = OpCode.Preparation;
    }
    //クライアントは情報を習得する
    public NetPreparation(DataStreamReader reader)
    {
        Code = OpCode.Preparation;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(teamID);
        writer.WriteByte(areReady);
        writer.WriteInt(positionX);
        writer.WriteInt(positionY);
        writer.WriteInt(destinationX);
        writer.WriteInt(destinationY);
    }
    public override void Deserialize(DataStreamReader reader)
    {
        teamID = reader.ReadInt();
        areReady = reader.ReadByte();
        positionX = reader.ReadInt();
        positionY = reader.ReadInt();
        destinationX = reader.ReadInt();
        destinationY = reader.ReadInt();
    }
    public override void ReceivedOnClient()
    {
        NetUtility.CPreparation?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.SPreparation?.Invoke(this, cnn);
    }
}
