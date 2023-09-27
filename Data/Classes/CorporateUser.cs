using Data.Classes.Auctions;
namespace Data.Classes;
public class CorporateUser : User
{
    
    /// <summary>
    /// Checks if the balance + credit is sufficient for the amount
    /// </summary>
    /// <param name="balance"></param>
    /// <param name="credit"></param>
    /// <param name="amount"></param>
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
    ///     Overrides the SubBalance method to subtract a specified amount from the sum of balance and credit.
    ///     If the subtraction results in a negative value, an ArgumentOutOfRangeException is thrown.
    /// </summary>
    /// <param name="amount">The amount to subtract from the balance.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the Balance + Credit is not sufficent.</exception>
    public override void SubBalance(decimal amount)
    {
        if (!HasSufficientFunds(Balance, Credit, amount))
            throw new ArgumentOutOfRangeException("Balance + Credit is not sufficent.");

  
        var startingValues = (Credit, Balance);
        
        // Start by subtracting the amount from the credit.
        if (Credit >= amount)
        {
            Credit -= amount;

            return;
        }
        else
        {
            amount -= Credit;
            
            Credit = 0;
        }
        
        // If the amount is still greater than 0, then we need to take it from the balance.
        if (Balance >= amount)
        {
            Balance -= amount;

            amount = 0;

            return;
        }
        
        // Reset the values.
        Credit = startingValues.Credit;
        Balance = startingValues.Balance;
        
        throw new ArgumentOutOfRangeException("Balance + Credit is not sufficent.");
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
    public bool PlaceBid(CorporateUser buyer, Auction auction, decimal newBid)
    {
        if (!HasSufficientFunds(buyer.Balance, buyer.Credit, auction.MinimumPrice))
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
               $"CorporateUserId: {CorporateUserId}\n" +
               $"CvrNumber: {Cvr}\n" +
               $"Credit: {Credit}\n";
    }
}
