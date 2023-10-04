using System.Data;
using Data.Classes.Auctions;

namespace Data.Classes;

public class PrivateUser : User
{
    private static bool HasSufficientFunds(decimal balance, decimal amount)
    {
        return balance - amount >= 0;
    }
    
    public int PrivateUserId { get; set; }
    public string Cpr { get; set; }

    public PrivateUser(int id, string cpr, User user) : base(user.UserId, user.Username, user.Password, user.Zipcode, user.Balance)
    {
        PrivateUserId = id;
        Cpr = cpr;
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
        if (!HasSufficientFunds(buyer.Balance, auction.CurrentPrice))
            return false;

        if (newBid < auction.CurrentPrice)
            return false;

        // Checks if the newBid is higher than the current highest bid.
        try
        {
            auction = DatabaseManager.DatabaseManager.GetAuctionById(auction.AuctionId);

            List<Bid> bids;
            try
            {
                bids = DatabaseManager.DatabaseManager.GetBidsByAuction(auction);
            }
            catch (Exception e) when (e is DataException)
            {
                Console.WriteLine($"Warning: {e.Message}");

                bids = new List<Bid>();
            }

            var ourBid = DatabaseManager.DatabaseManager.CreateBid(new Bid(0, DateTime.Now, newBid, buyer, auction));

            var highestBid = bids.Max(b => b.Amount);
            if (ourBid.Amount > highestBid)
            {
                auction.CurrentPrice = ourBid.Amount;
            }

            DatabaseManager.DatabaseManager.UpdateAuction(auction);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in PlaceBid: " + e.Message);
            throw;
        }
        
        return true;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"CPR: {Cpr}\n";
    }
}
