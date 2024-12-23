using FFXIVConnector.Factories;
using FFXIVConnector.Network.Interfaces;

namespace FFXIVConnector.Network.Handlers
{
    public abstract class HandlerBase : IHandler
    {
        public virtual void Handle(byte[] message)
        {
            throw new NotImplementedException("Implement in child handler class");
        }

        public T ReadPacket<T>(byte[] message)
        {
            var deserializer = PacketDeserialiserFactory.GetDeserialiser<T>();
            return deserializer.Deserialise(message);
        }
    }
}