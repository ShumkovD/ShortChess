using Unity.Networking.Transport;

public class NetPreparationInput : NetMessage
{
    public int teamID;
    public byte prepOver;

    //サーバーが作っている
    public NetPreparationInput()
    {
        Code = OpCode.PreparationInput;
    }
    //クライアントは情報を習得する
    public NetPreparationInput(DataStreamReader reader)
    {
        Code = OpCode.PreparationInput;
        Deserialize(reader);
    }
    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(teamID);
        writer.WriteByte(prepOver);
    }
    public override void Deserialize(DataStreamReader reader)
    {
        teamID = reader.ReadInt();
        prepOver = reader.ReadByte();
    }
    public override void ReceivedOnClient()
    {
        NetUtility.CPreparationInput?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.SPreparationInput?.Invoke(this, cnn);
    }
}
