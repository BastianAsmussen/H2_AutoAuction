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
    /// Overrides the SubBalance method to subtract a specified amount from the sum of balance and credit.
    /// If the subtraction results in a negative value, an ArgumentOutOfRangeException is thrown.
    /// </summary>
    /// <param name="amount">The amount to subtract from the balance.</param>
    /// <exception cref="DataException">Thrown when the Balance is not sufficient.</exception>
    public override void SubBalance(decimal amount)
    {
        if (!HasSufficientFunds(Balance, amount))
            throw new DataException($"Balance is not sufficient. Current balance: {Balance}");

        Balance -= amount;
    }

    
    /// <summary>
    ///  Receives a bid from a buyer.
    ///  Then checks if new bid is higher than the current highest bid.
    ///  Lastly notifies the seller the when a bid is over minimum bid.
    /// </summary>
    /// <param name="buyer"></param>
    /// <param name="auction"></param>
    /// <param name="newBid"></param>
    /// <returns></returns>
    public bool PlaceBid(PrivateUser buyer, Auction auction, decimal newBid)
    {
        if (!HasSufficientFunds(buyer.Balance, auction.MinimumPrice))
            return false;

        if (newBid < auction.MinimumPrice)
            return false;
        
        
        //checks if the newBid is higher than the current highest bid
        try
        {
            auction = DatabaseManager.DatabaseManager.GetAuctionById(auction.AuctionId);
            
            var ourbid = DatabaseManager.DatabaseManager.CreateBid(new Bid(0, DateTime.Now, newBid, buyer, auction));

            var bids = DatabaseManager.DatabaseManager.GetBidsByAuction(auction);

            var highestBid = bids.Max(b => b.Amount);

            if (ourbid.Amount > highestBid)
            {
                auction.Buyer = buyer;
            }

            DatabaseManager.DatabaseManager.UpdateAuction(auction);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in PlaceBid: " + e.Message);
            throw;
        }

        auction.Seller.ReceiveBidNotification($"New bid of {newBid} on {auction.Vehicle}.");

        return true;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"CPR: {Cpr}\n";
    }
}
