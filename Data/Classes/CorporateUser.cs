using System.Data;
using Data.Classes.Auctions;
namespace Data.Classes;
public class CorporateUser : User
{
    /// <summary>
    /// Checks if the balance + credit is sufficient for the amount
    /// </summary>
    /// <param name="balance">The balance.</param>
    /// <param name="credit">The credit.</param>
    /// <param name="amount">The amount that subtracts from your credit and balance.</param>
    /// <returns></returns>
    private static bool HasSufficientFunds(decimal balance, decimal credit, decimal amount)
    {
        return (balance + credit) - amount >= 0;
    }

    public int CorporateUserId { get; set; }
    public string Cvr { get; set; }
    public decimal Credit { get; set; }

    public CorporateUser(int id, string cvr, decimal credit, User user) : base(user.UserId, user.Username, user.Password, user.Zipcode, user.Balance)
    {
        CorporateUserId = id;
        Cvr = cvr;
        Credit = credit;
    }

    /// <summary>
    ///     Subtracts the amount from the balance. the balance will go negative if the credit is sufficient.
    /// </summary>
    /// <param name="amount">The amount to subtract from the balance.</param>
    /// <exception cref="DataException">Thrown when Credit threshold exceeded.</exception>
    public override void SubBalance(decimal amount)
    {
        if (!HasSufficientFunds(Balance, Credit, amount))
            throw new DataException("Credit threshold exceeded!");

        Balance -= amount;
    }
    
    /// <summary>
    ///  Receives a bid from a buyer.
    ///  Then checks if new bid is higher than the current highest bid.
    ///  Lastly notifies the seller the when a bid is over minimum bid.
    /// </summary>
    /// <param name="buyer">What the user is.</param>
    /// <param name="auction">The auction that get bidded on.</param>
    /// <param name="newBid">The bid that gets submitted.</param>
    /// <returns>True if place bid was succesful</returns>
    public bool PlaceBid(CorporateUser buyer, Auction auction, decimal newBid)
    {
        auction = DatabaseManager.DatabaseManager.GetAuctionById(auction.AuctionId);

        // If the new bid is less than or equal to the current price, return false.
        if (newBid <= auction.CurrentPrice)
            return false;

        // If the buyer does not have sufficient funds, return false.
        if (!HasSufficientFunds(buyer.Balance, buyer.Credit, auction.CurrentPrice))
            return false;

        // If the last bidder is the same as the current bidder, return false.
        var latestBid = DatabaseManager.DatabaseManager.GetBidsByAuction(auction)
            .OrderByDescending(b => b.Time)
            .First();
        if (latestBid.Bidder.UserId == buyer.UserId)
            return false;

        try
        {
            var ourBid = DatabaseManager.DatabaseManager.CreateBid(new Bid(0, DateTime.Now, newBid, buyer, auction));
            
            auction.CurrentPrice = ourBid.Amount;
            
            DatabaseManager.DatabaseManager.UpdateAuction(auction);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error in PlaceBid: " + e.Message);
            throw;
        }
        
        return true;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"CorporateUserId: {CorporateUserId}\n" +
               $"CvrNumber: {Cvr}\n" +
               $"Credit: {Credit}\n";
    }
}
