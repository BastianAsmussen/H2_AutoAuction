using System.Data;
using Data.Classes.Auctions;

namespace Data.Classes;

public class PrivateUser : User
{
    public PrivateUser(int id, string cpr, User user) : base(user.UserId, user.Username, user.Password, user.Zipcode,
        user.Balance)
    {
        PrivateUserId = id;
        Cpr = cpr;
    }

    public int PrivateUserId { get; set; }
    public string Cpr { get; set; }

    private static bool HasSufficientFunds(decimal balance, decimal amount)
    {
        return balance - amount >= 0;
    }

    /// <summary>
    ///     Subtract a specified amount from the balance.
    /// </summary>
    /// <param name="amount">The amount to subtract from the balance.</param>
    /// <exception cref="DataException">Thrown when the balance is not sufficient.</exception>
    public override void SubBalance(decimal amount)
    {
        if (!HasSufficientFunds(Balance, amount))
            throw new DataException($"Balance is not sufficient. Current balance: {Balance}");

        Balance -= amount;
    }

    /// <summary>
    ///     Receives a bid from a buyer.
    ///     Then checks if new bid is higher than the current highest bid.
    ///     Lastly notifies the seller the when a bid is over current bid.
    /// </summary>
    /// <param name="buyer">The bidder.</param>
    /// <param name="auction">The auction to bid on.</param>
    /// <param name="newBid">The new bid.</param>
    /// <returns>True if the bid was placed, false if not.</returns>
    /// <exception cref="ArgumentException">Thrown when an error occurs in the database.</exception>
    public bool PlaceBid(PrivateUser buyer, Auction auction, decimal newBid)
    {
        // If the new bid is less than or equal to the current price, return false.
        if (newBid <= auction.CurrentPrice)
            return false;

        // If the buyer does not have sufficient funds, return false.
        if (!HasSufficientFunds(buyer.Balance, auction.CurrentPrice))
            return false;

        // If the last bidder is the same as the current bidder, return false.
        try
        {
            var latestBid = DatabaseManager.DatabaseManager.GetBidsByAuction(auction)
                .OrderByDescending(b => b.Time)
                .First();

            if (latestBid.Bidder.UserId == buyer.UserId)
                return false;
        }
        catch (Exception e) when (e is DataException)
        {
            Console.WriteLine($"Warning: {e.Message}");
        }

        // Checks if the newBid is higher than the current highest bid.
        auction = DatabaseManager.DatabaseManager.GetAuctionById(auction.AuctionId);

        try
        {
            var bids = DatabaseManager.DatabaseManager.GetBidsByAuction(auction);

            var highestBid = bids.Max(b => b.Amount);
            if (newBid > highestBid) auction.CurrentPrice = newBid;
        }
        catch (Exception e) when (e is DataException)
        {
            Console.WriteLine($"Warning: {e.Message}");

            auction.CurrentPrice = newBid;
        }

        DatabaseManager.DatabaseManager.CreateBid(new Bid(0, DateTime.Now, newBid, buyer, auction));
        DatabaseManager.DatabaseManager.UpdateAuction(auction);

        return true;
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"CPR: {Cpr}\n";
    }
}