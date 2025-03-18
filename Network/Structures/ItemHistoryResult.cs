using FFXIVConnector.Network.Interfaces;

namespace FFXIVConnector.Network.Structures
{
    public struct ItemHistoryResult : IGameData
    {
        public uint CatalogID;
        public ZoneProtoDownItemHistoryData[] ItemHistoryList;

        public ItemHistoryResult()
        {
            ItemHistoryList = new ZoneProtoDownItemHistoryData[20];
        }

        public struct ZoneProtoDownItemHistoryData
        {
            public uint CatalogID;
            public uint SellPrice;
            public uint BuyRealDate;
            public uint Stack;
            public byte SubQuality;
            public byte MateriaCount;
            public char[] BuyCharacterName;

            public ZoneProtoDownItemHistoryData()
            {
                BuyCharacterName = new char[32];
            }
        };
    }
}