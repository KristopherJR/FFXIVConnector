using FFXIVConnector.Network.Deserialisers;
using FFXIVConnector.Network.Interfaces;
using FFXIVConnector.Network.Models;

namespace FFXIVConnector.Factories
{
    public static class PacketDeserialiserFactory
    {
        public static IPacketDeserialiser<T> GetDeserialiser<T>()
        {
            if (typeof(T) == typeof(MarketBoardItemListings))
            {
                return (IPacketDeserialiser<T>)new MarketBoardItemListingsDeserialiser();
            }

            throw new NotSupportedException($"No deserialiser found for type {typeof(T).Name}");
        }
    }
}