namespace FFXIVConnector.Network.Interfaces
{
    public interface IHandler
    {
        void Handle(byte[] message);
    }
}