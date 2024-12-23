using FFXIVConnector.Network.Interfaces;
using FFXIVConnector.Network.Models;
using FFXIVConnector.Network.Processors;
using FFXIVConnector.Utils;
using Machina.FFXIV;
using Machina.FFXIV.Headers;
using Machina.Infrastructure;
using System.Runtime.InteropServices;

namespace FFXIVConnector
{
    public class MainHandler : IDisposable
    {
        private FFXIVNetworkMonitor? _monitor;
        private bool _disposed;
        private readonly Dictionary<ushort, Func<IHandler>> _handlerFactories;

        public MainHandler()
        {
            _monitor = new FFXIVNetworkMonitor();
            _monitor.MessageReceivedEventHandler += MessageReceived;
            _monitor.Start();

            _handlerFactories = new Dictionary<ushort, Func<IHandler>>
            {
                { (ushort)OpCodes.MarketBoardItemListing, () => new MarketBoardItemListingsHandler() },
            };
        }

        public void MessageReceived(TCPConnection connection, long epoch, byte[] message)
        {
            if (message.Length < Marshal.SizeOf<Server_MessageHeader>())
                return; // Message is too short to contain a header, invalid

            var header = Memory.ToStruct<Server_MessageHeader>(message);

            if (_handlerFactories.TryGetValue(header.MessageType, out var handlerFactory))
            {
                handlerFactory().Handle(message.Skip(0x20).ToArray()); // Skip the header
            }
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

                    _handlerFactories.Clear();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}