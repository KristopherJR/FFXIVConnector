using FFXIVConnector.Network.Interfaces;
using System.Runtime.InteropServices;

namespace FFXIVConnector.Network.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CatalogSearchResult : IGameData
    {
        public ZoneProtoDownCatalogSearchData[] CatalogList;
        public uint NextIndex;
        public uint Result;
        public uint Index;
        public byte RequestKey;
        public byte Type;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ZoneProtoDownCatalogSearchData
        {
            public uint CatalogID;
            public ushort StockCount;
            public ushort RequestItemCount;
        };

        public CatalogSearchResult()
        {
            CatalogList = new ZoneProtoDownCatalogSearchData[20];
        }
    };
}