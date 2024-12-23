namespace FFXIVConnector.Network.Interfaces
{
    public interface IPacketDeserialiser<T>
    {
        T Deserialise(byte[] message);
    }
}