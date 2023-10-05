using Data.Classes.Vehicles;
using Data.DatabaseManager;
using Data.Interfaces;
using GUI;

namespace H2_AutoAuction;

internal class Program
{
    private static void Main(string[] args)
    {
        var databaseThread = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine("Resolving auctions...");
                DatabaseManager.ResolveAuctions();

                // Sleep for 1 minute.
                Thread.Sleep(1_000 * 60);
            }
        });

        databaseThread.IsBackground = true;
        databaseThread.Start();

        // Start the GUI.
        Gui.Main(args);

        /*
        var random = new Random();

        const int totalUsers = 100;
        const string defaultPassword = "1234";

        var users = new List<User>();
        for (var i = 0; i < totalUsers; i++)
        {
            var username = $"{Faker.Name.First()}{Faker.Name.Last()}";
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

        const int totalVehicles = 100;

        var licenseTypes = Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().ToList();
        var fuelTypes = Enum.GetValues(typeof(FuelType)).Cast<FuelType>().ToList();
        var energyTypes = Enum.GetValues(typeof(EnergyType)).Cast<EnergyType>().ToList();

        // Generate n random vehicles.
        var vehicles = new List<Vehicle>();
        for (var i = 0; i < totalVehicles; i++)
        {
            var name = Faker.Name.First();
            var km = random.Next(0, 1_000_000);
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

        const int totalAuctions = 10;

        var auctions = new List<Auction>();
        for (var i = 0; i < totalAuctions; i++)
        {
            var startDate = DateTime.Now.AddDays(random.Next(1, 7));
            var endDate = startDate.AddDays(random.Next(1, 7));
            var vehicle = DatabaseManager.GetVehicleById(random.Next(1, totalVehicles));
            var seller = DatabaseManager.GetUserById(random.Next(1, totalUsers));
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
            var bidder = DatabaseManager.GetUserById(random.Next(1, totalUsers));
            var amount = auction.CurrentPrice + random.Next(1_000, 10_000);
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