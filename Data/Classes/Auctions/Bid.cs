namespace Data.Classes.Auctions;

public class Bid
{
    public int BidId { get; }
    public DateTime Time { get; }
    public decimal Amount { get; }
    public User Bidder { get; }
    public Auction Auction { get; }

    public Bid(int id, DateTime time, decimal amount, User bidder, Auction auction)
    {
        BidId = id;
        Time = time;
        Amount = amount;
        Bidder = bidder;
        Auction = auction;
    }

    public override string ToString()
    {
        return $"BidId: {BidId}\n" +
               $"Time: {Time}\n" +
               $"Amount: {Amount}\n" +
               $"Bidder: {Bidder}\n" +
               $"Auction: {Auction}\n";
    }
}
