namespace Data.Classes;

public class CorporateUser : User
{
    public uint CvrNumber { get; set; }
    public decimal Credit { get; set; }

    public CorporateUser(string userName, string password, uint zipCode, uint cvrNumber, decimal credit) : base(
        userName, password, zipCode)
    {
        CvrNumber = cvrNumber;
        Credit = credit;

        //TODO: U8 - Add to database and set ID
        // throw new NotImplementedException();
    }

    /// <summary>
    /// Overrides the SubBalance method to subtract a specified amount from the sum of balance and credit.
    /// If the subtraction results in a negative value, an ArgumentOutOfRangeException is thrown.
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
               $"Id: {Id}\n" +
               $"UserName: {UserName}\n" +
               $"Balance: {Balance}\n" +
               $"Zipcode: {Zipcode}\n" +
               $"CvrNumber: {CvrNumber}\n" +
               $"Credit: {Credit}\n";
    }
}