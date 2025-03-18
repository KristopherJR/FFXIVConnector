using FFXIVConnector.Network.Interfaces;
using System.Runtime.InteropServices;

namespace FFXIVConnector.Network.Structures
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct RetainerInformation : IGameData
    {
        [FieldOffset(0)] public fixed byte unknown0[8];
        [FieldOffset(8)] public UInt64 retainerId;
        [FieldOffset(0)] public byte hireOrder;
        [FieldOffset(0)] public byte itemCount;
        [FieldOffset(0)] public fixed byte unknown5[2];
        [FieldOffset(0)] public uint gil;
        [FieldOffset(0)] public byte sellingCount;
        [FieldOffset(0)] public byte cityId;
        [FieldOffset(0)] public byte classJob;
        [FieldOffset(0)] public byte level;
        [FieldOffset(0)] public fixed byte unknown11[4];
        [FieldOffset(0)] public uint retainerTask;
        [FieldOffset(0)] public uint retainerTaskComplete;
        [FieldOffset(0)] public byte unknown14;
        [FieldOffset(0)] public fixed char retainerName[20];
    };
}