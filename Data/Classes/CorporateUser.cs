using System.Data;
using Data.Classes.Auctions;

namespace Data.Classes;

public class CorporateUser : User
{
    #region static methods

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

    #endregion

    #region properties

    public int CorporateUserId { get; set; }
    public string Cvr { get; set; }
    public decimal Credit { get; set; }

    #endregion
    
    public CorporateUser(int id, string cvr, decimal credit, User user) : base(user.UserId, user.Username,
        user.Password, user.Zipcode, user.Balance)
    {
        CorporateUserId = id;
        Cvr = cvr;
        Credit = credit;
    }  
    public CorporateUser(CorporateUser c, User user) : base(user)
    {
        CorporateUserId = c.CorporateUserId;
        Cvr = c.Cvr;
        Credit = c.Credit;
    }
    

    /// <summary>
    ///     Overrides the SubBalance method to subtract a specified amount from the sum of balance and credit.
    ///     If the subtraction results in a negative value, an DataException is thrown.
    /// </summary>
    /// <param name="amount">The amount to subtract from the balance.</param>
    /// <exception cref="DataException">Thrown when the Balance + Credit is not sufficent.</exception>
    public override void SubBalance(decimal amount)
    {
        if (!HasSufficientFunds(Balance, Credit, amount))
            throw new DataException("Balance and Credit is not sufficient!");

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

        throw new DataException("Balance + Credit is not sufficient.");
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

        if (newBid < auction.CurrentPrice)
            return false;

        if (!HasSufficientFunds(buyer.Balance, buyer.Credit, auction.CurrentPrice))
            return false;

        try
        {
            var ourBid = DatabaseManager.DatabaseManager.CreateBid(new Bid(0, DateTime.Now, newBid, buyer, auction));

            auction.CurrentPrice = ourBid.Amount;
            auction.Buyer = buyer;

            DatabaseManager.DatabaseManager.UpdateAuction(auction);
        }
        catch (ArgumentException e)
        {
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