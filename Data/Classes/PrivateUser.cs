namespace Data.Classes;

public class PrivateUser : User
{
    public uint PrivateUserId { get; set; }
    public string Cpr { get; set; }

    public PrivateUser(uint id, string cpr, User user) : base(user.UserId, user.Username, user.PasswordHash, user.Zipcode, user.Balance)
    {
        PrivateUserId = id;
        Cpr = cpr;
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

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"CPR: {Cpr}\n";
    }
}
