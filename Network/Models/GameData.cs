using FFXIVConnector.Network.Interfaces;

namespace FFXIVConnector.Network.Models
{
    public class GameData : IGameData
    {
        public byte[] RawData { get; set; }
        public long Epoch { get; set; }
        public OpCodeValue OpCode { get; set; }
    }
}