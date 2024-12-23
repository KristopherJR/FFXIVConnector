using FFXIVConnector.Network.Handlers;
using FFXIVConnector.Network.Models;

namespace FFXIVConnector.Network.Processors
{
    public class MarketBoardItemListingsHandler : HandlerBase
    {
        public override void Handle(byte[] message)
        {
            var listings = ReadPacket<MarketBoardItemListings>(message);

            foreach (MarketBoardItemListing itemListing in listings.Items)
            {
                Console.WriteLine(itemListing.PricePerUnit);
            }
        }
    }
}