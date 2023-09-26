using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.Interfaces;
using Utility.DatabaseManager;

namespace Data.Classes;

public class User : IBuyer, ISeller
{
    public uint UserId { get; set; }
    public string PasswordHash { get; }
    public string Username { get; set; }
    public uint Zipcode { get; set; }
    public decimal Balance { get; set; }

    public User(uint id, string username, string password, uint zipcode, decimal balance = 0)
    {
        UserId = id;
        Username = username;
        PasswordHash = ""; // Cryptography.Hashing.HashPassword(password);
        Zipcode = zipcode;
        Balance = balance;
    }

    public virtual void SubBalance(decimal amount)
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
               $"UserName: {Username}\n" +
               $"PasswordHash: {PasswordHash}\n" +
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
            auction = DatabaseManager.CreateAuction(auction);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error in SetForSale: " + e.Message);
            throw;
        }
        
        return auction.AuctionId ;
    }
    
    
    /// <summary>
    ///  Notifies the seller the when a bid is over minimum bid
    /// </summary>
    /// <param name="auction"></param>
    /// <param name="newBid"></param>
    /// <returns></returns>
    public uint PlaceBid(Auction auction, decimal newBid)
    {
        auction.Seller.ReceiveBidNotification($"New bid of {newBid} on {auction.Vehicle}.");

        return 0;
    }
}