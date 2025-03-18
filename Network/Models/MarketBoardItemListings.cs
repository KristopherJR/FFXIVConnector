namespace FFXIVConnector.Network.Models
{
    public class MarketBoardItemListings : GameData
    {
        public List<MarketBoardItemListing> Items;

        public int ListingIndexEnd;
        public int ListingIndexStart;
        public int RequestId;

        public class MarketBoardItemListing
        {
            public ulong ListingId;
            public ulong RetainerId;
            public ulong RetainerOwnerId;
            public ulong ArtisanId;
            public uint PricePerUnit;
            public uint TotalTax;
            public uint ItemQuantity;
            public uint CatalogId;
            public DateTime LastReviewTime;
            public List<ItemMateria> Materia;
            public string RetainerName;
            public string PlayerName;
            public bool IsHq;
            public int MateriaCount;
            public bool OnMannequin;
            public int RetainerCityId;
            public int StainId;
        }
    }
}