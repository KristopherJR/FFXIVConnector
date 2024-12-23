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

        public MainHandler()
        {
            _monitor = new FFXIVNetworkMonitor();
            _monitor.MessageReceivedEventHandler += MessageReceived;
            _monitor.Start();
        }

        public void MessageReceived(TCPConnection connection, long epoch, byte[] message)
        {
            if (message.Length < Marshal.SizeOf<Server_MessageHeader>())
                return; // Message is too short to contain a header, invalid

            IHandler handler = null;

            var header = Memory.ToStruct<Server_MessageHeader>(message);

            switch (header.MessageType)
            {
                case (ushort)OpCodes.MarketBoardItemListing:
                    handler = new MarketBoardItemListingsHandler();
                    break;
            }

            handler?.Handle(message.Skip(0x20).ToArray()); // skip the header
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

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}