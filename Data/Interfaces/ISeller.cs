using Data.Classes.Vehicles;

namespace Data.Interfaces;

public interface ISeller
{
    public int UserId { get; set; }

    int SetForSale(decimal startingBid, DateTime startDate, DateTime endDate, Vehicle vehicle, ISeller seller);
}