using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.Interfaces;

namespace Data.Classes;

public class User : IBuyer, ISeller
{
    public uint UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public uint Zipcode { get; set; }
    public decimal Balance { get; set; }

    public User(uint id, string username, string password, uint zipcode, decimal balance = 0)
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

    public uint PlaceBid(Auction auction, decimal newBid)
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
    /// <param name="Seller">property form User class</param>
    /// <param name="minBid"></param>
    /// <exception cref="ArgumentException">If it fails to create an auction</exception>
    /// <returns>The id of the auction</returns>
    public uint SetForSale(Vehicle vehicle, User Seller, decimal minBid)
    {
        Auction auction = new Auction(0, vehicle, Seller, null, minBid);
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
