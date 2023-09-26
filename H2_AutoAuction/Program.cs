using Data.Classes.Vehicles;
using Data.Interfaces;
using Utility.DatabaseManager;

namespace H2_AutoAuction;

internal class Program
{
    private static void Main(string[] args)
    {
        var databaseManager = DatabaseManager.Instance;
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
