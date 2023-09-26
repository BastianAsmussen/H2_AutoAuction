using Data.Classes.Vehicles;
using Data.Interfaces;

namespace Data.Classes.Auctions;

public class Auction
{
    /// <summary>
    ///     ID of the auction
    /// </summary>
    public uint AuctionId { get; }

    /// <summary>
    ///     The minimum price of the auction
    /// </summary>
    public decimal MinimumPrice { get; set; }

    /// <summary>
    ///     The standing bid of the auction
    /// </summary>
    public decimal StandingBid { get; set; }

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

    public Auction(uint id, Vehicle vehicle, ISeller seller, IBuyer? buyer, decimal minimumPrice, decimal standingBid = 0)
    {
        AuctionId = id;
        Vehicle = vehicle;
        Seller = seller;
        Buyer = buyer;
        MinimumPrice = minimumPrice;
        StandingBid = standingBid;
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}