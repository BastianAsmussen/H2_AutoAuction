using Data.Classes.Auctions;

namespace Data.Interfaces;

public interface IBuyer
{
    public uint UserId { get; set; }

    public void SubBalance(decimal amount);
    uint PlaceBid(Auction auction, decimal newBid);
}
