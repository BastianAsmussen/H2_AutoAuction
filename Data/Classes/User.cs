using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.Interfaces;

namespace Data.Classes;

public class User : IBuyer, ISeller
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Zipcode { get; set; }
    public decimal Balance { get; set; }

    public User(int id, string username, string password, string zipcode, decimal balance = 0)
    {
        UserId = id;
        Username = username;
        Password = password;
        Zipcode = zipcode;
        Balance = balance;
    }

    public virtual void SubBalance(decimal amount)
    {
        throw new NotImplementedException();
    }

    public bool PlaceBid(Auction auction, decimal newBid)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Receives a bid notification and returns a formatted string.
    /// </summary>
    /// <param name="message">The bid message to format.</param>
    /// <returns>A string with the formatted message.</returns>
    public string ReceiveBidNotification(string message)
    {
        return $"New Bid Received: {message}";
    }

    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"UserId: {UserId}\n" +
               $"Username: {Username}\n" +
               $"Password: {Password}\n" +
               $"Zipcode: {Zipcode}\n" +
               $"Balance: {Balance}\n";
    }
    
    /// <summary>
    /// Set's a vehicle for sale
    /// </summary>
    /// <param name="vehicle">property form Vehicle class</param>
    /// <param name="seller">property form User class</param>
    /// <param name="minBid"></param>
    /// <exception cref="ArgumentException">If it fails to create an auction</exception>
    /// <returns>The id of the auction</returns>
    public int SetForSale(Vehicle vehicle, User seller, decimal minBid)
    {
        var auction = new Auction(0, vehicle, seller, null, minBid);

        try
        {
            auction = DatabaseManager.DatabaseManager.CreateAuction(auction);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error in SetForSale: " + e.Message);
            throw;
        }
        
        return auction.AuctionId;
    } 
    
}
