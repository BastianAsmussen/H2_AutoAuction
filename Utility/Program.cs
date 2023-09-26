using Data.Classes;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.Classes.Vehicles.HeavyVehicles;

namespace Utility;

internal class Program
{
    private static void Main(string[] args)
    {
        var vehicle = new Vehicle(0, "Test Vehicle", 3.4f, "1024WASD", 2014, true, LicenseType.A, 2.4f, 1.2f,
            FuelType.Benzine, EnergyType.B);
        var dimensions = new Dimensions(0, 100f, 3f, 2.5f);
        var heavyVehicle = new HeavyVehicle(0, dimensions, vehicle);
        var bus = new Bus(0, 50, 20, true, heavyVehicle);
        var seller = new User(0, "Seller", "Test123!", 1024);
        var buyer = new User(0, "Buyer", "Test123!", 1024);
        var auction = new Auction(0, bus, seller, buyer, 1000, 10000);
        Console.WriteLine(auction);
    }
}