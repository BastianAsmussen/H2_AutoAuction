namespace H2_AutoAuction.Classes;

public class CorporateUser : User
{
    public CorporateUser(string userName, string password, uint zipCode, uint cvrNumber, decimal credit) : base(
        userName, password, zipCode)
    {
        //TODO: U7 - Set constructor
        //TODO: U8 - Add to database and set ID
        throw new NotImplementedException();
    }

    public uint CvrNumber { get; set; }
    public decimal Credit { get; set; }
}