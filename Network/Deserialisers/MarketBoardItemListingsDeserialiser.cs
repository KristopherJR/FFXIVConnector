using FFXIVConnector.Network.Interfaces;
using FFXIVConnector.Network.Models;
using System.Text;
using static FFXIVConnector.Network.Models.MarketBoardItemListings;

namespace FFXIVConnector.Network.Deserialisers
{
    public class MarketBoardItemListingsDeserialiser : IPacketDeserialiser<MarketBoardItemListings>
    {
        public MarketBoardItemListings Deserialise(byte[] message)
        {
            var output = new MarketBoardItemListings
            {
                RawData = message,
                Epoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                OpCode = new OpCodeValue((ServerZoneIpcType)BitConverter.ToUInt16(message, 18)),
            };

            using var stream = new MemoryStream(message);
            using var reader = new BinaryReader(stream);
            output.Items = new List<MarketBoardItemListing>();

            for (int i = 0; i < 10; i++)
            {
                var listingEntry = new MarketBoardItemListing
                {
                    ListingId = reader.ReadUInt64(),
                    RetainerId = reader.ReadUInt64(),
                    RetainerOwnerId = reader.ReadUInt64(),
                    ArtisanId = reader.ReadUInt64(),
                    PricePerUnit = reader.ReadUInt32(),
                    TotalTax = reader.ReadUInt32(),
                    ItemQuantity = reader.ReadUInt32(),
                    CatalogId = reader.ReadUInt32(),
                    // Removed in 7.0
                    LastReviewTime = DateTime.UtcNow,
                };

                reader.ReadUInt16(); // retainer slot
                reader.ReadUInt16(); // durability
                reader.ReadUInt16(); // spiritbond

                listingEntry.Materia = new List<ItemMateria>();
                for (var materiaIndex = 0; materiaIndex < 5; materiaIndex++)
                {
                    var materiaVal = reader.ReadUInt16();
                    var materiaEntry = new ItemMateria
                    {
                        MateriaId = (materiaVal & 0xFF0) >> 4,
                        Index = materiaVal & 0xF,
                    };

                    if (materiaEntry.MateriaId != 0)
                    {
                        listingEntry.Materia.Add(materiaEntry);
                    }
                }

                // 6 bytes of padding
                reader.ReadUInt16();
                reader.ReadUInt32();

                listingEntry.RetainerName = Encoding.UTF8.GetString(reader.ReadBytes(32)).TrimEnd('\u0000');

                // Empty as of 7.0
                listingEntry.PlayerName = Encoding.UTF8.GetString(reader.ReadBytes(32)).TrimEnd('\u0000');

                listingEntry.IsHq = reader.ReadBoolean();
                listingEntry.MateriaCount = reader.ReadByte();
                listingEntry.OnMannequin = reader.ReadBoolean();
                listingEntry.RetainerCityId = reader.ReadByte();
                listingEntry.StainId = reader.ReadUInt16();

                // 4 bytes of padding
                reader.ReadUInt32();

                if (listingEntry.CatalogId != 0)
                {
                    output.Items.Add(listingEntry);
                }
            }

            output.ListingIndexEnd = reader.ReadByte();
            output.ListingIndexStart = reader.ReadByte();
            output.RequestId = reader.ReadUInt16();

            return output;
        }
    }
}