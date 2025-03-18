using System.Runtime.InteropServices;

namespace FFXIVConnector.Network.Structures.Custom
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ItemMarketBoardInfo
    {
        public int Sequence;
        public int ContainerID;
        public int SlotNumber;
        public int Unknown0;
        public int UnitPrice;
    }
}