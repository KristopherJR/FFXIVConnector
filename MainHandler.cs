using FFXIVConnector.Factories;
using FFXIVConnector.Network.Interfaces;
using FFXIVConnector.Network.Models;
using FFXIVConnector.Network.Structures;
using FFXIVConnector.Network.Structures.Custom;
using FFXIVConnector.Utils;
using Machina.FFXIV;
using Machina.FFXIV.Headers;
using Machina.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FFXIVConnector
{
    public class MainHandler : IDisposable
    {
        private FFXIVNetworkMonitor? _monitor;
        private bool _disposed;
        private readonly Dictionary<Type, List<Delegate>> _subscriptions;

        private int count = 1;
        public MainHandler()
        {
            _subscriptions = new();
            _monitor = new FFXIVNetworkMonitor();
            _monitor.UseDeucalion = true;
            _monitor.ProcessID = (uint)Process.GetProcessesByName("ffxiv_dx11")[0].Id;

            _monitor.MessageReceivedEventHandler += MessageReceived;
            _monitor.Start(); 
        }

        public void SubscribeToEvent<T>(Action<T> callback) where T : GameData
        {
            var eventType = typeof(T);

            if (!_subscriptions.ContainsKey(eventType))
            {
                _subscriptions[eventType] = new List<Delegate>();
            }

            _subscriptions[eventType].Add(callback);
        }

        private void PublishEvent<T>(T eventData)
        {
            var eventType = typeof(T);

            if (_subscriptions.ContainsKey(eventType))
            {
                foreach (var callback in _subscriptions[eventType])
                {
                    ((Action<T>)callback)?.Invoke(eventData);
                }
            }
        }

        private void MessageReceived(TCPConnection connection, long epoch, byte[] message)
        {
            if (message.Length < Marshal.SizeOf<Server_MessageHeader>())
                return; // Message is too short to contain a header, invalid

            var header = Memory.ToStruct<Server_MessageHeader>(message);

            var opcodeHandlers = new Dictionary<ushort, Action>
            {
                { (ushort)ServerZoneIpcType.MarketBoardItemListing, () => ProcessMessage<MarketBoardItemListings>(message) },
            };

            //Debug.WriteLine(BitConverter.ToInt32(new byte[] { 226, 46, 0, 0 }));
            //Debug.WriteLine(BitConverter.ToInt32(new byte[] { 250, 3, 0, 0 }));
            //Debug.WriteLine(BitConverter.ToInt32(new byte[] { 223, 4, 0, 0 }));
            //Debug.WriteLine(BitConverter.ToInt32(new byte[] { 155, 5, 0, 0 }));

            //byte[] byteArray = { 226, 46 };

            //PrintBytesAsInt16(byteArray);
            //if (header.MessageType == (ushort)ServerZoneIpcType.ItemInfo)
            //{
            //    var data = Memory.ToStruct<ItemInfo>(message.Skip(0x20).ToArray());

            //    Debug.WriteLine($"ItemInfo Packet Count: {count}");
            //    count++;
            //}

            //if (header.MessageType == (ushort)ServerZoneIpcType.ItemMarketBoardInfo)
            //{
            //    var data = Memory.ToStruct<ItemMarketBoardInfo>(message.Skip(0x20).ToArray());

            //    Debug.WriteLine($"ItemMarketBoardInfo - Slot: {data.SlotNumber} Price: {data.UnitPrice} Sequence: {data.Sequence} ContainerID: {data.ContainerID}");
            //}
            CheckIfValueInEnum<ServerZoneIpcType>(header.MessageType);
            //CheckIfValueInEnum<ClientZoneIpcType>(header.MessageType);
            //CheckIfValueInEnum<ServerLobbyIpcType>(header.MessageType);
            //CheckIfValueInEnum<ClientLobbyIpcType>(header.MessageType);

            if (opcodeHandlers.TryGetValue(header.MessageType, out var handler))
            {
                handler();
            }

        }

        public static void PrintBytesAsInt16(byte[] bytes)
        {
            long value = BitConverter.ToInt16(bytes, 0);
            Debug.WriteLine("Converted Int16: " + value);
        }

        public static void PrintBytesAsInt64(byte[] bytes)
        {
            long value = BitConverter.ToInt64(bytes, 0);
            Debug.WriteLine("Converted Int64: " + value);
        }

        public static void PrintBytesAsDate(byte[] bytes)
        {
            long ticks = BitConverter.ToInt64(bytes, 0);

            DateTime dateTime = new DateTime(ticks);

            Debug.WriteLine("Converted DateTime: " + dateTime);
        }


        public static void CheckIfValueInEnum<TEnum>(ushort value) where TEnum : Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                string enumName = Enum.GetName(typeof(TEnum), value);
                Debug.WriteLine($"Value {value} exists in the enum as: {enumName}");
            }
            else
            {
                Debug.WriteLine($"Value {value} does not exist in the enum {typeof(TEnum)}.");
            }
        }

        private void ProcessMessage<T>(byte[] message) where T : IGameData
        {
            var data = ReadPacket<T>(message.Skip(0x20).ToArray());

            PublishEvent(data);
        }

        private static T ReadPacket<T>(byte[] message)
        {
            var deserializer = PacketDeserialiserFactory.GetDeserialiser<T>();
            return deserializer.Deserialise(message);
        }

        #region "IDisposable"
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_monitor != null)
                    {
                        _monitor.Stop();
                        _monitor.MessageReceivedEventHandler = null;
                        _monitor = null;
                    }
                }

                _disposed = true;
            }
        }
        #endregion
    }
}