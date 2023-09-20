namespace Data.Classes;

public class PrivateUser : User
{
    public uint CprNumber { get; set; }

    public PrivateUser(string userName, string password, uint zipCode, uint cprNumber) : base(userName, password,
        zipCode)
    {
        CprNumber = cprNumber;

        //TODO: U11 - Add to database and set ID
        // throw new NotImplementedException();
    }

    /// <summary>
    /// Overrides the SubBalance method to subtract a specified amount from the sum of balance and credit.
    /// If the subtraction results in a negative value, an ArgumentOutOfRangeException is thrown.
    /// </summary>
    /// <param name="amount">The amount to subtract from the balance.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the Balance is not sufficient.</exception>
    public override void SubBalance(decimal amount)
    {
        if (Balance - amount < 0)
            throw new ArgumentOutOfRangeException($"Balance is not sufficient. Current balance: {Balance}");

        Balance -= amount;
    }
}