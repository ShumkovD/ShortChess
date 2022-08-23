using Unity.Networking.Transport;

public class NetPreparationInput : NetMessage
{
    public int teamID;
    public byte prepOver;

    //�T�[�o�[������Ă���
    public NetPreparationInput()
    {
        Code = OpCode.PreparationInput;
    }
    //�N���C�A���g�͏����K������
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
