namespace Data.Classes.Auctions;

public class Bid
{
    public int BidId { get; }
    public DateTime Time { get; }
    public decimal Amount { get; }
    public User Bidder { get; }
    public Auction Auction { get; }

    /// <summary>
    ///     The final price of the auction, or "TBD" if the auction is still ongoing.
    /// </summary>
    public string FinalAuctionPrice
    {
        get
        {
            var bid = DatabaseManager.DatabaseManager.GetBidById(BidId);

            if (bid.Auction.Buyer == null)
            {
                return "TBD";
            }

            return bid.Auction.Buyer.UserId == bid.Bidder.UserId
                ? "Du vandt auktionen!"
                : bid.Auction.CurrentPrice.ToString("C0");
        }
    }

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
