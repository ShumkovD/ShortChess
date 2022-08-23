using Unity.Networking.Transport;

public class NetRematch : NetMessage
{
    public int teamID;
    public byte wantRematch;
    public NetRematch()
    {
        Code = OpCode.Rematch;
    }
    //クライアントは情報を習得する
    public NetRematch(DataStreamReader reader)
    {
        Code = OpCode.Rematch;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(teamID);
        writer.WriteByte(wantRematch);
    }
    public override void Deserialize(DataStreamReader reader)
    {
        teamID = reader.ReadInt();
        wantRematch = reader.ReadByte();
    }
    public override void ReceivedOnClient()
    {
        NetUtility.CRematch?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.SRematch?.Invoke(this, cnn);
    }
}
