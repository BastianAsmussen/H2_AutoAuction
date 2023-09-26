namespace Data.Classes;

public class CorporateUser : User
{
    public uint CorporateUserId { get; set; }
    public string CvrNumber { get; set; }
    public decimal Credit { get; set; }

    public CorporateUser(uint id, string cvrNumber, decimal credit, User user) : base(user.UserId, user.Username, user.PasswordHash, user.Zipcode, user.Balance)
    {
        CorporateUserId = id;
        CvrNumber = cvrNumber;
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
        if ((Balance + Credit) - amount < 0)
            throw new ArgumentOutOfRangeException("Balance + Credit is not sufficent.");

        Balance -= amount;
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"CorporateUserId: {CorporateUserId}\n" +
               $"CvrNumber: {CvrNumber}\n" +
               $"Credit: {Credit}\n";
    }
}
