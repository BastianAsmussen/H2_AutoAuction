﻿using Data.Classes;
using Data.Classes.Auctions;
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
        // Create a new thread for the database manager.
        new Thread(() =>
        {
            while (true)
            {
                var startTime = DateTime.Now;

                DatabaseManager.ResolveAuctions();

                Console.WriteLine($"Resolved auctions in {DateTime.Now - startTime}.");

                // Sleep for 1 minute.
                Thread.Sleep(1_000 * 60);
            }
        }).Start();

        /*
        const string username = "test";
        const string password = "test";

        var signupTimes = new List<TimeSpan>();
        var loginTimes = new List<TimeSpan>();

        for (var i = 0; i < 100; i++)
        {
            try
            {
                var signupStartTime = DateTime.Now;
                var signup =
                    DatabaseManager.SignUp(
                        new PrivateUser(0, "123456-7890", new User(0, username, password, "1234", 0)));
                var signupEndTime = DateTime.Now - signupStartTime;

                signupTimes.Add(signupEndTime);

                Console.WriteLine($"Signup took {signupEndTime.TotalMilliseconds} ms.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var loginStartTime = DateTime.Now;
                var login = DatabaseManager.Login(username, password);
                var loginEndTime = DateTime.Now - loginStartTime;

                loginTimes.Add(loginEndTime);

                Console.WriteLine($"Login took {loginEndTime.TotalMilliseconds} ms.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Console.WriteLine($"Average signup time: {signupTimes.Average(time => time.TotalMilliseconds)} ms.");
        Console.WriteLine($"Average login time: {loginTimes.Average(time => time.TotalMilliseconds)} ms.");
        */
        /*
        var random = new Random();
        const int totalUsers = 1_000;
        const string defaultPassword = "1234";

        var users = new List<User>();
        for (var i = 0; i < totalUsers; i++)
        {
            var username = $"{Faker.Name.First()}-{random.Next(1_000, 9_999)}";
            var password = defaultPassword;
            var zipcode = $"{random.Next(1_000, 9_999)}";
            var balance = random.Next(0, 1_000_000);

            var user = new User(0, username, password, zipcode, balance);

            var isCorporate = random.Next() % 2 == 0;
            if (isCorporate)
            {
                var cvr = $"{random.Next(100_000, 999_999)}";
                var credit = random.Next(0, 1_000_000);
                var corporateUser = DatabaseManager.SignUp(new CorporateUser(0, cvr, credit, user));

                users.Add(corporateUser);

                Console.WriteLine(corporateUser + "\n");
            }
            else
            {
                var cpr = $"{random.Next(100_000, 999_999)}-{random.Next(1_000, 9_999)}";
                var privateUser = DatabaseManager.SignUp(new PrivateUser(0, cpr, user));

                users.Add(privateUser);

                Console.WriteLine(privateUser + "\n");
            }
        }

        const int totalVehicles = 1_000;

        var licenseTypes = Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().ToList();
        var fuelTypes = Enum.GetValues(typeof(FuelType)).Cast<FuelType>().ToList();
        var energyTypes = Enum.GetValues(typeof(EnergyType)).Cast<EnergyType>().ToList();

        // Generate n random vehicles.
        var vehicles = new List<Vehicle>();
        for (var i = 0; i < totalVehicles; i++)
        {
            var name = Faker.Name.First();
            var km = random.Next(0, 1_000_000);
            // A registration number is 2 letters followed by 5 numbers.
            var registrationNumber = $"{(char)random.Next('A', 'Z')}{(char)random.Next('A', 'Z')}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}";
            var year = (short)random.Next(1900, 2021);
            var newPrice = random.Next(0, 1_000_000);
            var hasTowbar = random.Next() % 2 == 0;
            var licenseType = licenseTypes[random.Next(licenseTypes.Count)];
            var engineSize = random.Next(0, 10_000);
            var kmPerLiter = random.Next(1, 100);
            var fuelType = fuelTypes[random.Next(fuelTypes.Count)];
            var energyClass = energyTypes[random.Next(energyTypes.Count)];

            var vehicle = DatabaseManager.CreateVehicle(new Vehicle(0, name, km, registrationNumber, year, newPrice, hasTowbar, licenseType,
                engineSize, kmPerLiter, fuelType, energyClass));

            vehicles.Add(vehicle);

            Console.WriteLine(vehicle + "\n");
        }

        // Generate n / 10 random Dimensions.
        var dimensions = new List<Dimensions>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var length = random.Next(0, 10);
            var width = random.Next(0, 10);
            var height = random.Next(0, 10);

            var dimension = DatabaseManager.CreateDimensions(new Dimensions(0, length, width, height));

            dimensions.Add(dimension);

            Console.WriteLine(dimension + "\n");
        }

        // Generate n / 10 random PersonalCars.
        var personalCars = new List<PersonalCar>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var numberOfSeats = (byte)random.Next(1, 10);
            var trunkDimensions = dimensions[random.Next(dimensions.Count)];
            var vehicle = vehicles[random.Next(vehicles.Count)];

            var personalCar = DatabaseManager.CreatePersonalCar(new PersonalCar(0, numberOfSeats, trunkDimensions, vehicle));

            personalCars.Add(personalCar);
            dimensions.Remove(trunkDimensions);
            vehicles.Remove(vehicle);

            Console.WriteLine(personalCar + "\n");
        }

        // Generate n / 10 / 10 random ProfessionalPersonalCars.
        var professionalPersonalCars = new List<ProfessionalPersonalCar>();
        for (var i = 0; i < totalVehicles / 10 / 10; i++)
        {
            var hasSafetyBar = random.Next() % 2 == 0;
            var loadCapacity = random.Next(0, 1_000);
            var personalCar = personalCars[random.Next(personalCars.Count)];

            var professionalPersonalCar = DatabaseManager.CreateProfessionalPersonalCar(new ProfessionalPersonalCar(0, hasSafetyBar, loadCapacity, personalCar));

            professionalPersonalCars.Add(professionalPersonalCar);
            personalCars.Remove(personalCar);

            Console.WriteLine(professionalPersonalCar + "\n");
        }

        // Generate n / 10 / 10 random PrivatePersonalCars.
        var privatePersonalCars = new List<PrivatePersonalCar>();
        for (var i = 0; i < totalVehicles / 10 / 10; i++)
        {
            var hasIsofixFittings = random.Next() % 2 == 0;
            var personalCar = personalCars[random.Next(personalCars.Count)];

            var privatePersonalCar = DatabaseManager.CreatePrivatePersonalCar(new PrivatePersonalCar(0, hasIsofixFittings, personalCar));

            privatePersonalCars.Add(privatePersonalCar);
            personalCars.Remove(personalCar);

            Console.WriteLine(privatePersonalCar + "\n");
        }

        // Generate n / 10 random Dimensions.
        dimensions = new List<Dimensions>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var length = random.Next(0, 10);
            var width = random.Next(0, 10);
            var height = random.Next(0, 10);

            var dimension = DatabaseManager.CreateDimensions(new Dimensions(0, length, width, height));

            dimensions.Add(dimension);

            Console.WriteLine(dimension + "\n");
        }

        // Generate n / 10 random HeavyVehicles.
        var heavyVehicles = new List<HeavyVehicle>();
        for (var i = 0; i < totalVehicles / 10; i++)
        {
            var vehicle = vehicles[random.Next(vehicles.Count)];
            var vehicleDimensions = dimensions[random.Next(dimensions.Count)];
            var heavyVehicle = DatabaseManager.CreateHeavyVehicle(new HeavyVehicle(0, vehicleDimensions, vehicle));

            heavyVehicles.Add(heavyVehicle);
            dimensions.Remove(vehicleDimensions);
            vehicles.Remove(vehicle);

            Console.WriteLine(heavyVehicle + "\n");
        }

        // Generate n / 10 / 10 random Trucks.
        var trucks = new List<Truck>();
        for (var i = 0; i < totalVehicles / 10 / 10; i++)
        {
            var loadCapacity = random.Next(0, 1_000);
            var heavyVehicle = heavyVehicles[random.Next(heavyVehicles.Count)];

            var truck = DatabaseManager.CreateTruck(new Truck(0, loadCapacity, heavyVehicle));

            trucks.Add(truck);
            heavyVehicles.Remove(heavyVehicle);

            Console.WriteLine(truck + "\n");
        }

        // Generate n / 10 / 10 random Buses.
        var buses = new List<Bus>();
        for (var i = 0; i < totalVehicles / 10 / 10; i++)
        {
            var numberOfSeats = (byte)random.Next(1, 100);
            var numberOfSleepingSpaces = (byte)random.Next(1, 100);
            var hasToilet = random.Next() % 2 == 0;
            var heavyVehicle = heavyVehicles[random.Next(heavyVehicles.Count)];

            var bus = DatabaseManager.CreateBus(new Bus(0, numberOfSeats, numberOfSleepingSpaces, hasToilet, heavyVehicle));

            buses.Add(bus);
            heavyVehicles.Remove(heavyVehicle);

            Console.WriteLine(bus + "\n");
        }

        const int totalAuctions = 1_000;

        var auctions = new List<Auction>();
        for (var i = 0; i < totalAuctions; i++)
        {
            var name = Faker.Name.First();
            var startDate = DateTime.Now.AddDays(random.Next(1, 100));
            var endDate = startDate.AddDays(random.Next(1, 100));
            var vehicle = DatabaseManager.GetVehicleById(random.Next(1, 1_000));
            var seller = DatabaseManager.GetUserById(random.Next(1, 1_000));
            var startingBid = random.Next(0, 1_000_000);
            var auction = DatabaseManager.CreateAuction(new Auction(0, startingBid, startingBid, startDate, endDate,
                vehicle, seller, null));

            auctions.Add(auction);

            Console.WriteLine(auction + "\n");
        }

        var bids = new List<Bid>();
        for (var i = 0; i < totalAuctions / 10; i++)
        {
            var auction = auctions[random.Next(auctions.Count)];
            var time = auction.StartDate.AddSeconds(random.Next(1, 100));
            var bidder = DatabaseManager.GetUserById(random.Next(1, 1_000));
            var amount = auction.CurrentPrice + random.Next(1, 1_000);
            var bid = DatabaseManager.CreateBid(new Bid(0, time, amount, bidder, auction));

            bids.Add(bid);

            Console.WriteLine(bid + "\n");
        }
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
