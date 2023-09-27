using Data.Classes.Vehicles;
using Data.Classes.Vehicles.HeavyVehicles;
using Data.Classes.Vehicles.PersonalCars;
using Data.Interfaces;
using DatabaseManager = Data.DatabaseManager.DatabaseManager;

namespace H2_AutoAuction;

internal class Program
{
    private static void Main(string[] args)
    {
        var dbVehicle = DatabaseManager.CreateVehicle(new Vehicle(0, "Test", 0, "1234567", 2021, 500_000, false, LicenseType.A, 0, 0, FuelType.Diesel, EnergyType.A));
        Console.WriteLine(dbVehicle);

        const int totalVehicles = 1_000;

        // Generate 1000 random vehicles.
        var vehicles = new List<Vehicle>();
        for (var i = 0; i < totalVehicles; i++)
        {
            var name = Faker.Name.First();
            var km = new Random().Next(0, 1_000_000);
            var registrationNumber = new Random().Next(1_000_000, 9_999_999).ToString();
            var year = (short)new Random().Next(1900, 2021);
            var newPrice = new Random().Next(0, 1_000_000);
            var hasTowbar = new Random().Next() % 2 == 0;
            var licenseType = LicenseType.A;
            var engineSize = new Random().Next(0, 10_000);
            var kmPerLiter = new Random().Next(1, 100);
            var fuelType = FuelType.Diesel;
            var energyClass = EnergyType.A;

            var vehicle = DatabaseManager.CreateVehicle(new Vehicle(0, name, km, registrationNumber, year, newPrice, hasTowbar, licenseType,
                engineSize, kmPerLiter, fuelType, energyClass));

            vehicles.Add(vehicle);

            Console.WriteLine(vehicle);
        }

        var dimensions = DatabaseManager.CreateDimensions(new Dimensions(0, 0, 0, 0));

        // Generate 100 random PersonalCars.
        var personalCars = new List<PersonalCar>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var numberOfSeats = (byte)new Random().Next(1, 10);
            var vehicle = vehicles[new Random().Next(0, vehicles.Count)];

            var personalCar = DatabaseManager.CreatePersonalCar(new PersonalCar(0, numberOfSeats, dimensions, vehicle));

            personalCars.Add(personalCar);
            vehicles.Remove(vehicle);

            Console.WriteLine(personalCar);
        }

        // Generate 100 random ProfessionalPersonalCars.
        var professionalPersonalCars = new List<ProfessionalPersonalCar>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var hasSafetyBar = new Random().Next() % 2 == 0;
            var loadCapacity = new Random().Next(0, 1_000);
            var personalCar = personalCars[new Random().Next(0, personalCars.Count)];

            var professionalPersonalCar = DatabaseManager.CreateProfessionalPersonalCar(new ProfessionalPersonalCar(0, hasSafetyBar, loadCapacity, personalCar));

            professionalPersonalCars.Add(professionalPersonalCar);
            personalCars.Remove(personalCar);

            Console.WriteLine(professionalPersonalCar);
        }

        // Generate 100 random PrivatePersonalCars.
        var privatePersonalCars = new List<PrivatePersonalCar>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var hasIsofixFittings = new Random().Next() % 2 == 0;
            var personalCar = personalCars[new Random().Next(0, personalCars.Count)];

            var privatePersonalCar = DatabaseManager.CreatePrivatePersonalCar(new PrivatePersonalCar(0, hasIsofixFittings, personalCar));

            privatePersonalCars.Add(privatePersonalCar);
            personalCars.Remove(personalCar);

            Console.WriteLine(privatePersonalCar);
        }

        // Generate 100 random HeavyVehicles.
        var heavyVehicles = new List<HeavyVehicle>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var vehicle = vehicles[new Random().Next(0, vehicles.Count)];
            var heavyVehicle = DatabaseManager.CreateHeavyVehicle(new HeavyVehicle(0, dimensions, vehicle));

            heavyVehicles.Add(heavyVehicle);
            vehicles.Remove(vehicle);

            Console.WriteLine(heavyVehicle);
        }

        // Generate 100 random Trucks.
        var trucks = new List<Truck>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var loadCapacity = new Random().Next(0, 1_000);
            var heavyVehicle = heavyVehicles[new Random().Next(0, heavyVehicles.Count)];

            var truck = DatabaseManager.CreateTruck(new Truck(0, loadCapacity, heavyVehicle));

            trucks.Add(truck);
            heavyVehicles.Remove(heavyVehicle);

            Console.WriteLine(truck);
        }

        // Generate 100 random Buses.
        var buses = new List<Bus>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var numberOfSeats = (byte)new Random().Next(1, 100);
            var numberOfSleepingSpaces = (byte)new Random().Next(1, 100);
            var hasToilet = new Random().Next() % 2 == 0;
            var heavyVehicle = heavyVehicles[new Random().Next(0, heavyVehicles.Count)];

            var bus = DatabaseManager.CreateBus(new Bus(0, numberOfSeats, numberOfSleepingSpaces, hasToilet, heavyVehicle));

            buses.Add(bus);
            heavyVehicles.Remove(heavyVehicle);

            Console.WriteLine(bus);
        }
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
