using Data.Classes;
using Data.Classes.Vehicles;
using Data.Interfaces;
using Utility.DatabaseManager;

namespace H2_AutoAuction;

internal class Program
{
    private static void Main(string[] args)
    {
        var databaseManager = DatabaseManager.Instance;

        // AuctionHouse objects init

        #region init user objects

        User user1 = new PrivateUser("lkri", "password1", 7400, 0000000000);
        //user1.Balance = 35000M;
        User user2 = new CorporateUser("fros", "password2", 9000, 99999999, 40000M);
        //user2.Balance = 35000M;

        #endregion

        // calls of all the auction and search methods

        /*
        AuctionHouse.SetForSale(privateCar1, user1, 10000M);
        AuctionHouse.SetForSale(privateCar2, user1, 10000M);
        AuctionHouse.SetForSale(professionalCar, user2, 20000M,
            new NotificationDelegate(msg => "delegate message from user - " + msg));
        AuctionHouse.SetForSale(bus, user1, 20000M);

        AuctionHouse.ReceiveBid(user2, 0, 30000M);
        AuctionHouse.ReceiveBid(user1, 1, 30000M);
        AuctionHouse.ReceiveBid(user2, 2, 50500M);

        AuctionHouse.AcceptBid(user1, 1);

        Console.WriteLine("________ Search examples _________");
        Console.WriteLine("________ Search Vehicles by name _________");
        PrintVehicleList(AuctionHouse.FindVehiclesByName("swift").Result);
        Console.WriteLine("________ Search Vehicles by seats and toilet _________");
        PrintVehicleList(AuctionHouse.FindVehiclesByNumberOfSeats(10, true).Result);
        Console.WriteLine("________ Search Vehicles by Drivers License and weight _________");
        PrintVehicleList(AuctionHouse.FindVehiclesByDriversLicense(2.60).Result);
        Console.WriteLine("________ Search Vehicles by km and price _________");
        PrintVehicleList(AuctionHouse.FindVehiclesByKmAndPrice(310.0, 12000M).Result);
        Console.WriteLine("________ Search Sellers by range of zipcode _________");
        PrintISellerList(AuctionHouse.FindSellersByZipcodeRange(7000, 500).Result);
        */
    }

    /// <summary>
    ///     Makes a string of vehicles from a list.
    /// </summary>
    /// <param name="vehicles"></param>
    /// <returns> A string of vehicle information </returns>
    public static string PrintVehicleList(List<Vehicle> vehicles)
    {
        //TODO: return formatted string with vehicles from list
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Makes a string of ISellers from a list.
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    public static string PrintISellerList(List<ISeller> users)
    {
        //TODO: return formatted string with users from list
        throw new NotImplementedException();
    }
}