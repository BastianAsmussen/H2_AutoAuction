using Data.Interfaces;

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
}
