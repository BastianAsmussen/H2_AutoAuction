using Data.Classes;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;

namespace Data.Interfaces;

public interface IBuyer
{
    public uint UserId { get; set; }

    public void SubBalance(decimal amount);
    uint PlaceBid(Auction auction, decimal newBid);
}
