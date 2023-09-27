using Data.Classes.Auctions;

namespace Data.Interfaces;

public interface IBuyer
{
    public int UserId { get; set; }

    public void SubBalance(decimal amount);
    bool PlaceBid(Auction auction, decimal newBid);
}
