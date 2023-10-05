using Data.Classes.Vehicles;
using Data.Interfaces;

namespace Data.Classes.Auctions;

public class Auction
{
    public Auction(int id, decimal currentPrice, decimal startingBid, DateTime startDate, DateTime endDate,
        Vehicle vehicle, ISeller seller, IBuyer? buyer)
    {
        AuctionId = id;

        CurrentPrice = currentPrice;
        StartingBid = startingBid;

        StartDate = startDate;
        EndDate = endDate;

        Vehicle = vehicle;
        Seller = seller;
        Buyer = buyer;
    }

    /// <summary>
    ///     The ID of the auction.
    /// </summary>
    public int AuctionId { get; }

    /// <summary>
    ///     The current price of the auction.
    /// </summary>
    public decimal CurrentPrice { get; set; }

    /// <summary>
    ///     The standing bid of the auction.
    /// </summary>
    public decimal StartingBid { get; set; }

    /// <summary>
    ///     The starting date of the auction.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     The ending date of the auction.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     The vehicle of the auction.
    /// </summary>
    public Vehicle Vehicle { get; set; }

    /// <summary>
    ///     The seller of the auction.
    /// </summary>
    public ISeller Seller { get; set; }

    /// <summary>
    ///     The buyer or potential buyer of the auction.
    /// </summary>
    public IBuyer? Buyer { get; set; }

    public override string ToString()
    {
        return $"Auction ID: {AuctionId}\n" +
               $"Current price: {CurrentPrice}\n" +
               $"Starting Bid: {StartingBid}\n" +
               $"Start Date: {StartDate}\n" +
               $"End Date: {EndDate}\n" +
               $"Vehicle: {Vehicle}\n" +
               $"Seller: {Seller}\n" +
               $"Buyer: {Buyer}";
    }
}