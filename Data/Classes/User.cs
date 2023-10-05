using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.Interfaces;

namespace Data.Classes;

public class User : IBuyer, ISeller
{
    public User(int id, string username, string password, string zipcode, decimal balance = 0)
    {
        UserId = id;
        Username = username;
        Password = password;
        Zipcode = zipcode;
        Balance = balance;
    }

    public User(User user)
    {
        UserId = user.UserId;
        Username = user.Username;
        Password = user.Password;
        Zipcode = user.Zipcode;
        Balance = user.Balance;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Zipcode { get; set; }
    public decimal Balance { get; set; }
    public int UserId { get; set; }

    public virtual void SubBalance(decimal amount)
    {
        throw new NotImplementedException();
    }

    public bool PlaceBid(Auction auction, decimal newBid)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Sets a vehicle for sale.
    /// </summary>
    /// <param name="startingBid">The current bid of the auction.</param>
    /// <param name="startDate">The starting date of the auction.</param>
    /// <param name="endDate">The ending date of the auction.</param>
    /// <param name="vehicle">The vehicle to set for sale.</param>
    /// <param name="seller">The seller of the auction.</param>
    /// <returns>The ID of the auction.</returns>
    /// <exception cref="ArgumentException">
    ///     Thrown when the start date is after the end date, the current bid is less than 0 or
    ///     the auction fails to be created.
    /// </exception>
    public int SetForSale(decimal startingBid, DateTime startDate, DateTime endDate, Vehicle vehicle, ISeller seller)
    {
        // If the start date is after the end date, throw an exception.
        if (startDate > endDate)
            throw new ArgumentException("Start date cannot be after end date!");

        // If the minimum bid is less than 0, throw an exception.
        if (startingBid < 0)
            throw new ArgumentException("Starting bid cannot be less than 0!");

        var auction = new Auction(0, startingBid, startingBid, startDate, endDate, vehicle, seller, null);

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

    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"UserId: {UserId}\n" +
               $"Username: {Username}\n" +
               $"Password: {Password}\n" +
               $"Zipcode: {Zipcode}\n" +
               $"Balance: {Balance}\n";
    }
}