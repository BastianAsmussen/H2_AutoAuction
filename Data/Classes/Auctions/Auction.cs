using Data.Classes.Vehicles;
using Data.Interfaces;

namespace Data.Classes.Auctions;

public class Auction
{
    /// <summary>
    ///     ID of the auction
    /// </summary>
    public int AuctionId { get; }

    /// <summary>
    ///     The minimum price of the auction
    /// </summary>
    public decimal MinimumPrice { get; set; }

    /// <summary>
    ///     The standing bid of the auction
    /// </summary>
    public decimal StartingBid { get; set; }

    /// <summary>
    ///     The vehicle of the auction
    /// </summary>
    public Vehicle Vehicle { get; set; }

    /// <summary>
    ///     The seller of the auction
    /// </summary>
    public ISeller Seller { get; set; }

    /// <summary>
    ///     The buyer or potential buyer of the auction
    /// </summary>
    public IBuyer? Buyer { get; set; }

    public Auction(int id, Vehicle vehicle, ISeller seller, IBuyer? buyer, decimal minimumPrice, decimal startingBid = 0)
    {
        AuctionId = id;
        Vehicle = vehicle;
        Seller = seller;
        Buyer = buyer;
        MinimumPrice = minimumPrice;
        StartingBid = startingBid;
    }

    public override string ToString()
    {
        return $"Auction ID: {AuctionId}\n" +
               $"Vehicle: {Vehicle}\n" +
               $"Seller: {Seller}\n" +
               $"Buyer: {Buyer}\n" +
               $"Minimum Price: {MinimumPrice}\n" +
               $"Starting Bid: {StartingBid}";
    }
}