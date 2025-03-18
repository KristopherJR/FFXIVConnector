namespace FFXIVConnector.Network.Structures
{
    public struct ItemInfo
    {
        public uint ContainerSequence { get; set; }
        public uint Unknown { get; set; }
        public ushort ContainerId { get; set; }
        public ushort Slot { get; set; }
        public uint Quantity { get; set; }
        public uint CatalogId { get; set; }
        public uint ReservedFlag { get; set; }
        public ulong SignatureId { get; set; }
        public bool HqFlag { get; set; }
        public byte Unknown2 { get; set; }
        public ushort Condition { get; set; }
        public ushort SpiritBond { get; set; }
        public ushort Stain { get; set; }
        public uint GlamourCatalogId { get; set; }
        public ushort[] Materia { get; set; }
        public byte[] MateriaTiers { get; set; }
        public byte Padding { get; set; }
        public uint Unknown10 { get; set; }
    }
}