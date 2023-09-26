using Data.Classes.Vehicles;
using Data.Classes.Vehicles.HeavyVehicles;
using Data.Classes.Vehicles.PersonalCars;

namespace Data.DatabaseManager;

/// <summary>
///     The part of the database manager that handles vehicles.
/// </summary>
public partial class DatabaseManager
{
    #region LicenseType
    /// <summary>
    ///     Get a list of all license types.
    /// </summary>
    /// <returns>A list of all license types.</returns>
    /// <exception cref="ArgumentException">Thrown if no license types exist.</exception>
    public static List<LicenseType> GetAllLicenseTypes()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM LicenseTypes";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No license types exist!");
        }

        var licenseTypes = new List<LicenseType>();

        while (reader.Read())
        {
            // var licenseTypeId = (uint)reader.GetInt32(0);
            var name = reader.GetString(1);

            licenseTypes.Add((LicenseType) Enum.Parse(typeof(LicenseType), name));
        }

        reader.Close();
        connection.Close();

        return licenseTypes;
    }

    /// <summary>
    ///     Create a license type in the database.
    /// </summary>
    /// <param name="licenseType">The license type to create.</param>
    /// <returns>The ID of the created license type.</returns>
    /// <exception cref="ArgumentException">Thrown if the license type could not be created.</exception>
    public static uint CreateLicenseType(LicenseType licenseType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO LicenseTypes (Type)" +
                              "    OUTPUT inserted.Id" +
                              "    VALUES (@Type)";
        command.Parameters.AddWithValue("@Type", licenseType.ToString());

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("License type could not be created!");
        }

        reader.Read();

        var licenseTypeId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return licenseTypeId;
    }

    /// <summary>
    ///     Get a license type by its ID.
    /// </summary>
    /// <param name="id">The ID of the license type to get.</param>
    /// <returns>The license type with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no license type with the given ID exists.</exception>
    public static LicenseType GetLicenseTypeById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM LicenseTypes" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No license type with that ID exists!");
        }

        reader.Read();

        // var licenseTypeId = (uint)reader.GetInt32(0);
        var name = reader.GetString(1);

        reader.Close();
        connection.Close();

        return (LicenseType) Enum.Parse(typeof(LicenseType), name);
    }

    /// <summary>
    ///     Get a license type by its name.
    /// </summary>
    /// <param name="name">The name of the license type to get.</param>
    /// <returns>The license type with the given name.</returns>
    /// <exception cref="ArgumentException">Thrown if no license type with the given name exists.</exception>
    public static LicenseType GetLicenseTypeByName(string name)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM LicenseTypes" +
                              "    WHERE Type = @Type";
        command.Parameters.AddWithValue("@Type", name);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No license type with that name exists!");
        }

        reader.Read();

        // var licenseTypeId = (uint)reader.GetInt32(0);
        var licenseTypeName = reader.GetString(1);

        reader.Close();
        connection.Close();

        return (LicenseType) Enum.Parse(typeof(LicenseType), licenseTypeName);
    }

    /// <summary>
    ///     Update a license type in the database.
    /// </summary>
    /// <param name="licenseType">The license type to update.</param>
    /// <returns>The updated license type.</returns>
    /// <exception cref="ArgumentException">Thrown if no license type with the given ID exists.</exception>
    public static LicenseType UpdateLicenseType(LicenseType licenseType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE LicenseTypes" +
                              "    SET Type = @Type" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", licenseType);
        command.Parameters.AddWithValue("@Type", licenseType.ToString());

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No license type with that ID exists!");
        }

        connection.Close();

        return licenseType;
    }

    /// <summary>
    ///    Delete a license type from the database.
    /// </summary>
    /// <param name="licenseType">The license type to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no license type with the given ID exists.</exception>
    public static void DeleteLicenseType(LicenseType licenseType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM LicenseTypes" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", licenseType);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No license type with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region FuelType
    /// <summary>
    ///     Get a list of all fuel types.
    /// </summary>
    /// <returns>A list of all fuel types.</returns>
    /// <exception cref="ArgumentException">Thrown if no fuel types exist.</exception>
    public static List<FuelType> GetAllFuelTypes()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM FuelTypes";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No fuel types exist!");
        }

        var fuelTypes = new List<FuelType>();

        while (reader.Read())
        {
            // var fuelTypeId = (uint)reader.GetInt32(0);
            var name = reader.GetString(1);

            fuelTypes.Add((FuelType) Enum.Parse(typeof(FuelType), name));
        }

        reader.Close();
        connection.Close();

        return fuelTypes;
    }

    /// <summary>
    ///     Create a fuel type in the database.
    /// </summary>
    /// <param name="fuelType">The fuel type to create.</param>
    /// <returns>The ID of the created fuel type.</returns>
    /// <exception cref="ArgumentException">Thrown if the fuel type could not be created.</exception>
    public static uint CreateFuelType(FuelType fuelType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO FuelTypes (Type)" +
                              "    OUTPUT inserted.Id" +
                              "    VALUES (@Type)";
        command.Parameters.AddWithValue("@Type", fuelType.ToString());

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Fuel type could not be created!");
        }

        reader.Read();

        var fuelTypeId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return fuelTypeId;
    }

    /// <summary>
    ///     Get a fuel type by its ID.
    /// </summary>
    /// <param name="id">The ID of the fuel type to get.</param>
    /// <returns>The fuel type with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no fuel type with the given ID exists.</exception>
    public static FuelType GetFuelTypeById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM FuelTypes" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No fuel type with that ID exists!");
        }

        reader.Read();

        // var fuelTypeId = (uint)reader.GetInt32(0);
        var name = reader.GetString(1);

        reader.Close();
        connection.Close();

        return (FuelType) Enum.Parse(typeof(FuelType), name);
    }

    /// <summary>
    ///     Get a fuel type by its name.
    /// </summary>
    /// <param name="name">The name of the fuel type to get.</param>
    /// <returns>The fuel type with the given name.</returns>
    /// <exception cref="ArgumentException">Thrown if no fuel type with the given name exists.</exception>
    public static FuelType GetFuelTypeByName(string name)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM FuelTypes" +
                              "    WHERE Type = @Type";
        command.Parameters.AddWithValue("@Type", name);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No fuel type with that name exists!");
        }

        reader.Read();

        // var fuelTypeId = (uint)reader.GetInt32(0);
        var fuelTypeName = reader.GetString(1);

        reader.Close();
        connection.Close();

        return (FuelType) Enum.Parse(typeof(FuelType), fuelTypeName);
    }

    /// <summary>
    ///     Update a fuel type in the database.
    /// </summary>
    /// <param name="fuelType">The fuel type to update.</param>
    /// <returns>The updated fuel type.</returns>
    /// <exception cref="ArgumentException">Thrown if no fuel type with the given ID exists.</exception>
    public static FuelType UpdateFuelType(FuelType fuelType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE FuelTypes" +
                              "    SET Type = @Type" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Type", fuelType.ToString());
        command.Parameters.AddWithValue("@Id", fuelType);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No fuel type with that ID exists!");
        }

        connection.Close();

        return fuelType;
    }

    /// <summary>
    ///     Delete a fuel type from the database.
    /// </summary>
    /// <param name="fuelType">The fuel type to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no fuel type with the given ID exists.</exception>
    public static void DeleteFuelType(FuelType fuelType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM FuelTypes" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", fuelType);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No fuel type with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region EnergyType
    /// <summary>
    ///     Get a list of all energy types.
    /// </summary>
    /// <returns>A list of all energy types.</returns>
    /// <exception cref="ArgumentException">Thrown if no energy types exist.</exception>
    public static List<EnergyType> GetAllEnergyTypes()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM EnergyTypes";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No energy types exist!");
        }

        var energyTypes = new List<EnergyType>();

        while (reader.Read())
        {
            // var energyTypeId = (uint)reader.GetInt32(0);
            var name = reader.GetString(1);

            energyTypes.Add((EnergyType) Enum.Parse(typeof(EnergyType), name));
        }

        reader.Close();
        connection.Close();

        return energyTypes;
    }

    /// <summary>
    ///     Create an energy type in the database.
    /// </summary>
    /// <param name="energyType">The energy type to create.</param>
    /// <returns>The ID of the created energy type.</returns>
    /// <exception cref="ArgumentException">Thrown if the energy type could not be created.</exception>
    public static uint CreateEnergyType(EnergyType energyType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO EnergyTypes (Type)" +
                              "    OUTPUT inserted.Id" +
                              "    VALUES (@Type)";
        command.Parameters.AddWithValue("@Type", energyType.ToString());

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Energy type could not be created!");
        }

        reader.Read();

        var energyTypeId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return energyTypeId;
    }

    /// <summary>
    ///     Get an energy type by its ID.
    /// </summary>
    /// <param name="id">The ID of the energy type to get.</param>
    /// <returns>The energy type with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no energy type with the given ID exists.</exception>
    public static EnergyType GetEnergyTypeById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM EnergyTypes" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No energy type with that ID exists!");
        }

        reader.Read();

        // var energyTypeId = (uint)reader.GetInt32(0);
        var name = reader.GetString(1);

        reader.Close();
        connection.Close();

        return (EnergyType) Enum.Parse(typeof(EnergyType), name);
    }

    /// <summary>
    ///     Get an energy type by its name.
    /// </summary>
    /// <param name="name">The name of the energy type to get.</param>
    /// <returns>The energy type with the given name.</returns>
    /// <exception cref="ArgumentException">Thrown if no energy type with the given name exists.</exception>
    public static EnergyType GetEnergyTypeByName(string name)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM EnergyTypes" +
                              "    WHERE Type = @Type";
        command.Parameters.AddWithValue("@Type", name);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No energy type with that name exists!");
        }

        reader.Read();

        // var energyTypeId = (uint)reader.GetInt32(0);
        var energyTypeName = reader.GetString(1);

        reader.Close();
        connection.Close();

        return (EnergyType) Enum.Parse(typeof(EnergyType), energyTypeName);
    }

    /// <summary>
    ///     Update an energy type in the database.
    /// </summary>
    /// <param name="energyType">The energy type to update.</param>
    /// <returns>The updated energy type.</returns>
    /// <exception cref="ArgumentException">Thrown if no energy type with the given ID exists.</exception>
    public static EnergyType UpdateEnergyType(EnergyType energyType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE EnergyTypes" +
                              "    SET Type = @Type" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", energyType);
        command.Parameters.AddWithValue("@Type", energyType.ToString());

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No energy type with that ID exists!");
        }

        connection.Close();

        return energyType;
    }

    /// <summary>
    ///     Delete an energy type from the database.
    /// </summary>
    /// <param name="energyType">The energy type to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no energy type with the given ID exists.</exception>
    public static void DeleteEnergyType(EnergyType energyType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM EnergyTypes" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", energyType);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No energy type with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region Vehicle
    /// <summary>
    ///     Gets all vehicles from the database.
    /// </summary>
    /// <returns>A list of all vehicles in the database.</returns>
    /// <exception cref="ArgumentException">Thrown if no vehicles exist.</exception>
    public static List<Vehicle> GetAllVehicles()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vehicles";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No vehicles exist!");
        }

        var vehicles = new List<Vehicle>();

        while (reader.Read())
        {
            var vehicleId = (uint)reader.GetInt32(0);
            var name = reader.GetString(1);
            var km = reader.GetFloat(2);
            var registrationNumber = reader.GetString(3);
            var year = (ushort)reader.GetInt32(4);
            var hasTowbar = reader.GetBoolean(5);
            var licenseType = (LicenseType)reader.GetInt32(6);
            var engineSize = reader.GetFloat(7);
            var kmPerLiter = reader.GetFloat(8);
            var fuelType = (FuelType)reader.GetInt32(9);
            var energyClass = (EnergyType)reader.GetInt32(10);

            vehicles.Add(new Vehicle(vehicleId, name, km, registrationNumber, year, hasTowbar, licenseType, engineSize,
                kmPerLiter, fuelType, energyClass));
        }

        reader.Close();
        connection.Close();

        return vehicles;
    }

    /// <summary>
    ///     Creates a vehicle in the database.
    /// </summary>
    /// <param name="vehicle">The vehicle to create.</param>
    /// <returns>The created vehicle.</returns>
    /// <exception cref="ArgumentException">Thrown if the vehicle could not be created.</exception>
    public static Vehicle CreateVehicle(Vehicle vehicle)
    {
        // Create the vehicle in the database.
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText =
            "INSERT INTO Vehicles (" +
            "    Name," +
            "    Km," +
            "    RegistrationNumber," +
            "    Year," +
            "    HasTowbar," +
            "    LicenseTypeId," +
            "    EngineSize," +
            "    KmPerLiter," +
            "    FuelTypeId," +
            "    EnergyTypeId" +
            ")" +
            " OUTPUT inserted.Id" +
            " VALUES (" +
            "    @Name," +
            "    @Km," +
            "    @RegistrationNumber," +
            "    @Year," +
            "    @HasTowbar," +
            "    @LicenseTypeId," +
            "    @EngineSize," +
            "    @KmPerLiter," +
            "    @FuelTypeId," +
            "    @EnergyTypeId" +
            ")";
        command.Parameters.AddWithValue("@Name", vehicle.Name);
        command.Parameters.AddWithValue("@Km", vehicle.Km);
        command.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
        command.Parameters.AddWithValue("@Year", vehicle.Year);
        command.Parameters.AddWithValue("@HasTowbar", vehicle.HasTowbar);
        command.Parameters.AddWithValue("@LicenseTypeId", vehicle.LicenseType);
        command.Parameters.AddWithValue("@EngineSize", vehicle.EngineSize);
        command.Parameters.AddWithValue("@KmPerLiter", vehicle.KmPerLiter);
        command.Parameters.AddWithValue("@FuelTypeId", vehicle.FuelType);
        command.Parameters.AddWithValue("@EnergyTypeId", vehicle.EnergyClass);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Vehicle could not be created!");
        }

        reader.Read();

        var vehicleId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetVehicleById(vehicleId);
    }

    /// <summary>
    ///     Gets a vehicle from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the vehicle to get.</param>
    /// <returns>The vehicle with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no vehicle with the given ID exists.</exception>
    public static Vehicle GetVehicleById(uint id)
    {
        // Retrieve the vehicle from the database.
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vehicles" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No vehicle with that ID exists!");
        }

        reader.Read();

        var vehicleId = (uint)reader.GetInt32(0);
        var name = reader.GetString(1);
        var km = reader.GetFloat(2);
        var registrationNumber = reader.GetString(3);
        var year = (ushort)reader.GetInt32(4);
        var hasTowbar = reader.GetBoolean(5);
        var licenseType = (LicenseType)reader.GetInt32(6);
        var engineSize = reader.GetFloat(7);
        var kmPerLiter = reader.GetFloat(8);
        var fuelType = (FuelType)reader.GetInt32(9);
        var energyClass = (EnergyType)reader.GetInt32(10);

        reader.Close();
        connection.Close();

        return new Vehicle(vehicleId, name, km, registrationNumber, year, hasTowbar, licenseType, engineSize,
            kmPerLiter, fuelType, energyClass);
    }

    /// <summary>
    ///     Gets a list of vehicles from the database by their name.
    /// </summary>
    /// <param name="name">The name of the vehicles to get.</param>
    /// <returns>The vehicles with the given name.</returns>
    /// <exception cref="ArgumentException">Thrown if no vehicle with the given name exists.</exception>
    public static List<Vehicle> GetVehiclesByName(string name)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vehicles" +
                              "    WHERE Name = @Name";
        command.Parameters.AddWithValue("@Name", name);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No vehicle with that name exists!");
        }

        var vehicles = new List<Vehicle>();

        while (reader.Read())
        {
            var vehicleId = (uint)reader.GetInt32(0);
            var km = reader.GetFloat(2);
            var registrationNumber = reader.GetString(3);
            var year = (ushort)reader.GetInt32(4);
            var hasTowbar = reader.GetBoolean(5);
            var licenseType = (LicenseType)reader.GetInt32(6);
            var engineSize = reader.GetFloat(7);
            var kmPerLiter = reader.GetFloat(8);
            var fuelType = (FuelType)reader.GetInt32(9);
            var energyClass = (EnergyType)reader.GetInt32(10);

            vehicles.Add(new Vehicle(vehicleId, name, km, registrationNumber, year, hasTowbar, licenseType,
                engineSize, kmPerLiter, fuelType, energyClass));
        }

        reader.Close();
        connection.Close();

        return vehicles;
    }

    /// <summary>
    ///     Gets a list of vehicles from the database by their number of seats.
    /// </summary>
    /// <param name="seats">The number of seats of the vehicles to get.</param>
    /// <returns>A list of vehicles with the given number of seats.</returns>
    /// <exception cref="ArgumentException">Thrown if no vehicle with the given number of seats exists.</exception>
    public static List<Vehicle> GetVehiclesByNumberOfSeats(ushort seats)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT V.Id" +
                              "    FROM Vehicles AS V" +
                              "    WHERE EXISTS (" +
                              "        SELECT 1" +
                              "        FROM Buses AS B" +
                              "        INNER JOIN HeavyVehicles AS H ON B.HeavyVehicleId = H.Id" +
                              "        WHERE H.VehicleId = V.Id" +
                              "            AND B.NumberOfSeats = @NumberOfSeats" +
                              ")" +
                              "OR EXISTS (" +
                              "    SELECT 1" +
                              "    FROM PersonalCars AS P" +
                              "    WHERE P.VehicleId = V.Id" +
                              "        AND P.NumberOfSeats = @NumberOfSeats" +
                              ")";
        command.Parameters.AddWithValue("@NumberOfSeats", seats);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No vehicle with that number of seats exists!");
        }

        var vehicles = new List<Vehicle>();

        while (reader.Read())
        {
            var vehicleId = (uint)reader.GetInt32(0);
            var vehicle = GetVehicleById(vehicleId);

            vehicles.Add(vehicle);
        }

        reader.Close();
        connection.Close();

        return vehicles;
    }

    /// <summary>
    ///     Gets a list of vehicles from the database by their license type.
    /// </summary>
    /// <param name="licenseType">The license type of the vehicles to get.</param>
    /// <returns>A list of vehicles with the given license type.</returns>
    /// <exception cref="ArgumentException">Thrown if no vehicle with the given license type exists.</exception>
    public static List<Vehicle> GetVehiclesByLicenseType(LicenseType licenseType)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id FROM Vehicles WHERE LicenseTypeId = @LicenseType";
        command.Parameters.AddWithValue("@LicenseType", (uint) licenseType);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No vehicle with that driver's license type exists!");
        }

        var vehicles = new List<Vehicle>();

        while (reader.Read())
        {
            var vehicleId = (uint)reader.GetInt32(0);
            var vehicle = GetVehicleById(vehicleId);

            vehicles.Add(vehicle);
        }

        reader.Close();
        connection.Close();

        return vehicles;
    }

    /// <summary>
    ///     Gets a list of vehicles from the database by the number of kilometers they have driven and their price when new.
    /// </summary>
    /// <param name="km">The number of kilometers the vehicles have driven.</param>
    /// <param name="newPrice">The price of the vehicles when new.</param>
    /// <returns>A list of vehicles with the given number of kilometers driven and price when new.</returns>
    /// <exception cref="ArgumentException">Thrown if no vehicle with the given number of kilometers driven and price when new exists.</exception>
    public static List<Vehicle> GetVehiclesByKmAndPrice(float km, decimal newPrice)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id FROM Vehicles WHERE Km = @Km AND NewPrice = @NewPrice";
        command.Parameters.AddWithValue("@Km", km);
        command.Parameters.AddWithValue("@NewPrice", newPrice);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No vehicle with that number of kilometers driven and price when new exists!");
        }

        var vehicles = new List<Vehicle>();

        while (reader.Read())
        {
            var vehicleId = (uint)reader.GetInt32(0);
            var vehicle = GetVehicleById(vehicleId);

            vehicles.Add(vehicle);
        }

        reader.Close();
        connection.Close();

        return vehicles;
    }

    /// <summary>
    ///     Updates a vehicle in the database.
    /// </summary>
    /// <param name="vehicle">The vehicle to update.</param>
    /// <returns>The updated vehicle.</returns>
    /// <exception cref="ArgumentException">Thrown if no vehicle with the given ID exists.</exception>
    public static Vehicle UpdateVehicle(Vehicle vehicle)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText =
            "UPDATE Vehicles" +
            " SET Name = @Name," +
            "     Km = @Km," +
            "     RegistrationNumber = @RegistrationNumber," +
            "     Year = @Year," +
            "     HasTowbar = @HasTowbar," +
            "     LicenseTypeId = @LicenseTypeId," +
            "     EngineSize = @EngineSize," +
            "     KmPerLiter = @KmPerLiter," +
            "     FuelTypeId = @FuelTypeId," +
            "     EnergyTypeId = @EnergyTypeId" +
            " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", vehicle.VehicleId);
        command.Parameters.AddWithValue("@Name", vehicle.Name);
        command.Parameters.AddWithValue("@Km", vehicle.Km);
        command.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
        command.Parameters.AddWithValue("@Year", vehicle.Year);
        command.Parameters.AddWithValue("@HasTowbar", vehicle.HasTowbar);
        command.Parameters.AddWithValue("@LicenseTypeId", vehicle.LicenseType);
        command.Parameters.AddWithValue("@EngineSize", vehicle.EngineSize);
        command.Parameters.AddWithValue("@KmPerLiter", vehicle.KmPerLiter);
        command.Parameters.AddWithValue("@FuelTypeId", vehicle.FuelType);
        command.Parameters.AddWithValue("@EnergyTypeId", vehicle.EnergyClass);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No vehicle with that ID exists!");
        }

        connection.Close();

        return vehicle;
    }

    /// <summary>
    ///     Deletes a vehicle from the database.
    /// </summary>
    /// <param name="vehicle">The vehicle to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no vehicle with the given ID exists.</exception>
    public static void DeleteVehicle(Vehicle vehicle)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Vehicles WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", vehicle.VehicleId);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No vehicle with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region Dimensions
    /// <summary>
    ///     Get a list of all dimensions.
    /// </summary>
    /// <returns>A list of all dimensions.</returns>
    /// <exception cref="ArgumentException">Thrown if no dimensions exist.</exception>
    public static List<Dimensions> GetAllDimensions()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Dimensions";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No dimensions exist!");
        }

        var dimensions = new List<Dimensions>();

        while (reader.Read())
        {
            var dimensionId = (uint) reader.GetInt32(0);
            var length = reader.GetFloat(1);
            var width = reader.GetFloat(2);
            var height = reader.GetFloat(3);

            dimensions.Add(new Dimensions(dimensionId, length, width, height));
        }

        reader.Close();
        connection.Close();

        return dimensions;
    }

    /// <summary>
    ///     Create a dimension in the database.
    /// </summary>
    /// <param name="dimensions">The dimensions to create.</param>
    /// <returns>The created dimensions.</returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions could not be created.</exception>
    public static Dimensions CreateDimensions(Dimensions dimensions)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Dimensions (Length, Width, Height)" +
                              "    OUTPUT inserted.Id" +
                              "    VALUES (@Length, @Width, @Height)";
        command.Parameters.AddWithValue("@Length", dimensions.Length);
        command.Parameters.AddWithValue("@Width", dimensions.Width);
        command.Parameters.AddWithValue("@Height", dimensions.Height);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Dimensions could not be created!");
        }

        reader.Read();

        var dimensionId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetDimensionsById(dimensionId);
    }

    /// <summary>
    ///     Get a dimension by its ID.
    /// </summary>
    /// <param name="id">The ID of the dimension to get.</param>
    /// <returns>The dimension with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no dimension with the given ID exists.</exception>
    public static Dimensions GetDimensionsById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Dimensions WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No dimension with that ID exists!");
        }

        reader.Read();

        var dimensionId = (uint) reader.GetInt32(0);
        var length = reader.GetFloat(1);
        var width = reader.GetFloat(2);
        var height = reader.GetFloat(3);

        reader.Close();
        connection.Close();

        return new Dimensions(dimensionId, length, width, height);
    }

    /// <summary>
    ///     Get a list of dimensions by their length.
    /// </summary>
    /// <param name="length">The length of the dimension to get.</param>
    /// <returns>A list of dimensions with the given length.</returns>
    /// <exception cref="ArgumentException">Thrown if no dimension with the given length exists.</exception>
    public static List<Dimensions> GetDimensionsByLength(float length)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Dimensions WHERE Length = @Length";
        command.Parameters.AddWithValue("@Length", length);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No dimension with that length exists!");
        }

        var dimensions = new List<Dimensions>();

        while (reader.Read())
        {
            var dimensionId = (uint) reader.GetInt32(0);
            var width = reader.GetFloat(2);
            var height = reader.GetFloat(3);

            dimensions.Add(new Dimensions(dimensionId, length, width, height));
        }

        reader.Close();
        connection.Close();

        return dimensions;
    }

    /// <summary>
    ///     Get a list of dimensions by their width.
    /// </summary>
    /// <param name="width">The width of the dimension to get.</param>
    /// <returns>A list of dimensions with the given width.</returns>
    /// <exception cref="ArgumentException">Thrown if no dimension with the given width exists.</exception>
    public static List<Dimensions> GetDimensionsByWidth(float width)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Dimensions WHERE Width = @Width";
        command.Parameters.AddWithValue("@Width", width);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No dimension with that width exists!");
        }

        var dimensions = new List<Dimensions>();

        while (reader.Read())
        {
            var dimensionId = (uint) reader.GetInt32(0);
            var length = reader.GetFloat(1);
            var height = reader.GetFloat(3);

            dimensions.Add(new Dimensions(dimensionId, length, width, height));
        }

        reader.Close();
        connection.Close();

        return dimensions;
    }

    /// <summary>
    ///     Get a list of dimensions by their height.
    /// </summary>
    /// <param name="height">The height of the dimension to get.</param>
    /// <returns>A list of dimensions with the given height.</returns>
    /// <exception cref="ArgumentException">Thrown if no dimension with the given height exists.</exception>
    public static List<Dimensions> GetDimensionsByHeight(float height)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Dimensions WHERE Height = @Height";
        command.Parameters.AddWithValue("@Height", height);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No dimension with that height exists!");
        }

        var dimensions = new List<Dimensions>();

        while (reader.Read())
        {
            var dimensionId = (uint) reader.GetInt32(0);
            var length = reader.GetFloat(1);
            var width = reader.GetFloat(2);

            dimensions.Add(new Dimensions(dimensionId, length, width, height));
        }

        reader.Close();
        connection.Close();

        return dimensions;
    }

    /// <summary>
    ///     Update a dimension in the database.
    /// </summary>
    /// <param name="dimensions">The dimension to update.</param>
    /// <returns>The updated dimension.</returns>
    /// <exception cref="ArgumentException">Thrown if no dimension with the given ID exists.</exception>
    public static Dimensions UpdateDimensions(Dimensions dimensions)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Dimensions SET Length = @Length, Width = @Width, Height = @Height WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", dimensions);
        command.Parameters.AddWithValue("@Length", dimensions.Length);
        command.Parameters.AddWithValue("@Width", dimensions.Width);
        command.Parameters.AddWithValue("@Height", dimensions.Height);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No dimension with that ID exists!");
        }

        connection.Close();

        return dimensions;
    }

    /// <summary>
    ///     Delete a dimension from the database.
    /// </summary>
    /// <param name="dimensions">The dimension to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no dimension with the given ID exists.</exception>
    public static void DeleteDimensions(Dimensions dimensions)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Dimensions WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", dimensions);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No dimension with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region HeavyVehicle
    /// <summary>
    ///     Get a list of all heavy vehicles.
    /// </summary>
    /// <returns>A list of all heavy vehicles.</returns>
    /// <exception cref="ArgumentException">Thrown if no heavy vehicles exist.</exception>
    public static List<HeavyVehicle> GetAllHeavyVehicles()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT VehicleDimensionsId, VehicleId FROM HeavyVehicles";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No heavy vehicles exist!");
        }

        var heavyVehicles = new List<HeavyVehicle>();

        while (reader.Read())
        {
            var heavyVehicleId = (uint) reader.GetInt32(0);

            var dimensions = GetDimensionsById((uint) reader.GetInt32(1));
            var vehicle = GetVehicleById((uint) reader.GetInt32(2));

            heavyVehicles.Add(new HeavyVehicle(heavyVehicleId, dimensions, vehicle));
        }

        reader.Close();
        connection.Close();

        return heavyVehicles;
    }

    /// <summary>
    ///     Create a heavy vehicle in the database.
    /// </summary>
    /// <param name="heavyVehicle">The heavy vehicle to create.</param>
    /// <exception cref="ArgumentException">Thrown if the heavy vehicle could not be created.</exception>
    public static HeavyVehicle CreateHeavyVehicle(HeavyVehicle heavyVehicle)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO HeavyVehicles (VehicleDimensionsId, VehicleId)" +
                              "    OUTPUT inserted.Id" +
                              "    VALUES (@VehicleDimensionsId, @VehicleId)";
        command.Parameters.AddWithValue("@VehicleDimensionsId", heavyVehicle.Dimensions);
        command.Parameters.AddWithValue("@VehicleId", heavyVehicle.VehicleId);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Heavy vehicle could not be created!");
        }

        reader.Read();

        var heavyVehicleId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetHeavyVehicleById(heavyVehicleId);
    }

    /// <summary>
    ///     Get a heavy vehicle by its ID.
    /// </summary>
    /// <param name="id">The ID of the heavy vehicle to get.</param>
    /// <returns>The heavy vehicle with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no heavy vehicle with the given ID exists.</exception>
    public static HeavyVehicle GetHeavyVehicleById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM HeavyVehicles" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No heavy vehicle with that ID exists!");
        }

        reader.Read();

        var heavyVehicleId = (uint) reader.GetInt32(0);

        var dimensions = GetDimensionsById((uint) reader.GetInt32(1));
        var vehicle = GetVehicleById((uint) reader.GetInt32(2));

        reader.Close();
        connection.Close();

        return new HeavyVehicle(heavyVehicleId, dimensions, vehicle);
    }

    /// <summary>
    ///     Update a heavy vehicle in the database.
    /// </summary>
    /// <param name="heavyVehicle">The heavy vehicle to update.</param>
    /// <returns>The updated heavy vehicle.</returns>
    /// <exception cref="ArgumentException">Thrown if no heavy vehicle with the given ID exists.</exception>
    public static HeavyVehicle UpdateHeavyVehicle(HeavyVehicle heavyVehicle)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE HeavyVehicles" +
                              "     SET VehicleDimensionsId = @VehicleDimensionsId," +
                              "         VehicleId = @VehicleId" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@VehicleDimensionsId", heavyVehicle.Dimensions);
        command.Parameters.AddWithValue("@VehicleId", heavyVehicle.VehicleId);
        command.Parameters.AddWithValue("@Id", heavyVehicle.HeavyVehicleId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No heavy vehicle with that ID exists!");
        }

        connection.Close();

        return heavyVehicle;
    }

    /// <summary>
    ///     Delete a heavy vehicle from the database.
    /// </summary>
    /// <param name="heavyVehicle">The heavy vehicle to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no heavy vehicle with the given ID exists.</exception>
    public static void DeleteHeavyVehicle(HeavyVehicle heavyVehicle)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM HeavyVehicles" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", heavyVehicle);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No heavy vehicle with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region Truck
    /// <summary>
    ///     Get a list of all trucks.
    /// </summary>
    /// <returns>A list of all trucks.</returns>
    /// <exception cref="ArgumentException">Thrown if no trucks exist.</exception>
    public static List<Truck> GetAllTrucks()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT LoadCapacity, HeavyVehicleId FROM Trucks";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No trucks exist!");
        }

        var trucks = new List<Truck>();

        while (reader.Read())
        {
            var truckId = (uint) reader.GetInt32(0);

            var loadCapacity = reader.GetFloat(1);
            var heavyVehicle = GetHeavyVehicleById((uint) reader.GetInt32(2));

            trucks.Add(new Truck(truckId, loadCapacity, heavyVehicle));
        }

        reader.Close();
        connection.Close();

        return trucks;
    }

    /// <summary>
    ///     Create a truck in the database.
    /// </summary>
    /// <param name="truck">The truck to create.</param>
    /// <returns>The created truck.</returns>
    /// <exception cref="ArgumentException">Thrown if the truck could not be created.</exception>
    public static Truck CreateTruck(Truck truck)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Trucks (" +
                              "    LoadCapacity," +
                              "    HeavyVehicleId" +
                              ")" +
                              "    OUTPUT inserted.Id" +
                              "    VALUES (" +
                              "        @LoadCapacity," +
                              "        @HeavyVehicleId" +
                              ")";
        command.Parameters.AddWithValue("@LoadCapacity", truck.LoadCapacity);
        command.Parameters.AddWithValue("@HeavyVehicleId", truck.HeavyVehicleId);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Truck could not be created!");
        }

        reader.Read();

        var truckId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetTruckById(truckId);
    }

    /// <summary>
    ///     Get a truck by its ID.
    /// </summary>
    /// <param name="id">The ID of the truck to get.</param>
    /// <returns>The truck with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no truck with the given ID exists.</exception>
    public static Truck GetTruckById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Trucks" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No truck with that ID exists!");
        }

        reader.Read();

        var truckId = (uint) reader.GetInt32(0);

        var loadCapacity = reader.GetFloat(1);
        var heavyVehicle = GetHeavyVehicleById((uint) reader.GetInt32(2));

        reader.Close();
        connection.Close();

        return new Truck(truckId, loadCapacity, heavyVehicle);
    }

    /// <summary>
    ///     Update a truck in the database.
    /// </summary>
    /// <param name="truck">The truck to update.</param>
    /// <returns>The updated truck.</returns>
    /// <exception cref="ArgumentException">Thrown if no truck with the given ID exists.</exception>
    public static Truck UpdateTruck(Truck truck)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Trucks" +
                              " SET LoadCapacity = @LoadCapacity," +
                              "     HeavyVehicleId = @HeavyVehicleId" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@LoadCapacity", truck.LoadCapacity);
        command.Parameters.AddWithValue("@HeavyVehicleId", truck.HeavyVehicleId);
        command.Parameters.AddWithValue("@Id", truck.TruckId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No truck with that ID exists!");
        }

        connection.Close();

        return truck;
    }

    /// <summary>
    ///     Delete a truck from the database.
    /// </summary>
    /// <param name="truck">The truck to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no truck with the given ID exists.</exception>
    public static void DeleteTruck(Truck truck)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Trucks" +
                              "    WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", truck.TruckId);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No truck with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region Bus
    /// <summary>
    ///     Get a list of all buses.
    /// </summary>
    /// <returns>A list of all buses.</returns>
    /// <exception cref="ArgumentException">Thrown if no buses exist.</exception>
    public static List<Bus> GetAllBuses()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT NumberOfSeats," +
                              "       NumberOfSleepingSpaces," +
                              "       HasToilet," +
                              "       HeavyVehicleId" +
                              " FROM Buses";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No buses exist!");
        }

        var buses = new List<Bus>();

        while (reader.Read())
        {
            var busId = (uint) reader.GetInt32(0);

            var numberOfSeats = reader.GetByte(1);
            var numberOfSleepingSpaces = reader.GetByte(2);
            var hasToilet = reader.GetBoolean(3);
            var heavyVehicle = GetHeavyVehicleById((uint) reader.GetInt32(4));

            buses.Add(new Bus(busId, numberOfSeats, numberOfSleepingSpaces, hasToilet, heavyVehicle));
        }

        reader.Close();
        connection.Close();

        return buses;
    }

    /// <summary>
    ///     Create a bus in the database.
    /// </summary>
    /// <param name="bus">The bus to create.</param>
    /// <returns>The created bus.</returns>
    /// <exception cref="ArgumentException">Thrown if the bus could not be created.</exception>
    public static Bus CreateBus(Bus bus)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Buses (" +
                              "    NumberOfSeats," +
                              "    NumberOfSleepingSpaces," +
                              "    HasToilet," +
                              "    HeavyVehicleId" +
                              ")" +
                              "    OUTPUT inserted.Id" +
                              "    VALUES (" +
                              "        @NumberOfSeats," +
                              "        @NumberOfSleepingSpaces," +
                              "        @HasToilet," +
                              "        @HeavyVehicleId" +
                              ")";
        command.Parameters.AddWithValue("@NumberOfSeats", bus.NumberOfSeats);
        command.Parameters.AddWithValue("@NumberOfSleepingSpaces", bus.NumberOfSleepingSpaces);
        command.Parameters.AddWithValue("@HasToilet", bus.HasToilet);
        command.Parameters.AddWithValue("@HeavyVehicleId", bus.HeavyVehicleId);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Bus could not be created!");
        }

        reader.Read();

        var busId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetBusById(busId);
    }

    /// <summary>
    ///     Get a bus by its ID.
    /// </summary>
    /// <param name="id">The ID of the bus to get.</param>
    /// <returns>The bus with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no bus with the given ID exists.</exception>
    public static Bus GetBusById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Buses" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No bus with that ID exists!");
        }

        reader.Read();

        var busId = (uint) reader.GetInt32(0);

        var numberOfSeats = reader.GetByte(1);
        var numberOfSleepingSpaces = reader.GetByte(2);
        var hasToilet = reader.GetBoolean(3);
        var heavyVehicle = GetHeavyVehicleById((uint) reader.GetInt32(4));

        reader.Close();
        connection.Close();

        return new Bus(busId, numberOfSeats, numberOfSleepingSpaces, hasToilet, heavyVehicle);
    }

    /// <summary>
    ///     Update a bus in the database.
    /// </summary>
    /// <param name="bus">The bus to update.</param>
    /// <returns>The updated bus.</returns>
    /// <exception cref="ArgumentException">Thrown if no bus with the given ID exists.</exception>
    public static Bus UpdateBus(Bus bus)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Buses" +
                              " SET NumberOfSeats = @NumberOfSeats," +
                              "     NumberOfSleepingSpaces = @NumberOfSleepingSpaces," +
                              "     HasToilet = @HasToilet," +
                              "     HeavyVehicleId = @HeavyVehicleId" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@NumberOfSeats", bus.NumberOfSeats);
        command.Parameters.AddWithValue("@NumberOfSleepingSpaces", bus.NumberOfSleepingSpaces);
        command.Parameters.AddWithValue("@HasToilet", bus.HasToilet);
        command.Parameters.AddWithValue("@HeavyVehicleId", bus.HeavyVehicleId);
        command.Parameters.AddWithValue("@Id", bus.BusId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No bus with that ID exists!");
        }

        connection.Close();

        return bus;
    }

    /// <summary>
    ///     Delete a bus from the database.
    /// </summary>
    /// <param name="bus">The bus to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no bus with the given ID exists.</exception>
    public static void DeleteBus(Bus bus)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Buses" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", bus.BusId);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No bus with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region PersonalCar
    /// <summary>
    ///     Get a list of all personal cars.
    /// </summary>
    /// <returns>A list of all personal cars.</returns>
    /// <exception cref="ArgumentException">Thrown if no personal cars exist.</exception>
    public static List<PersonalCar> GetAllPersonalCars()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT TrunkDimensionsId, VehicleId FROM PersonalCars";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No personal cars exist!");
        }

        var personalCars = new List<PersonalCar>();

        while (reader.Read())
        {
            var personalCarId = (uint) reader.GetInt32(0);
            var numberOfSeats = reader.GetByte(1);
            var dimensions = GetDimensionsById((uint) reader.GetInt32(2));
            var vehicle = GetVehicleById((uint) reader.GetInt32(3));

            personalCars.Add(new PersonalCar(personalCarId, numberOfSeats, dimensions, vehicle));
        }

        reader.Close();
        connection.Close();

        return personalCars;
    }

    /// <summary>
    ///     Create a personal car in the database.
    /// </summary>
    /// <param name="personalCar">The personal car to create.</param>
    /// <returns>The created personal car.</returns>
    /// <exception cref="ArgumentException">Thrown if the personal car could not be created.</exception>
    public static PersonalCar CreatePersonalCar(PersonalCar personalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO PersonalCars (" +
                              "    NumberOfSeats," +
                              "    TrunkDimensionsId," +
                              "    VehicleId" +
                              ")" +
                              " OUTPUT inserted.Id" +
                              " VALUES (" +
                              "    @NumberOfSeats," +
                              "    @VehicleDimensionsId," +
                              "    @VehicleId" +
                              ")";
        command.Parameters.AddWithValue("@NumberOfSeats", personalCar.NumberOfSeats);
        command.Parameters.AddWithValue("@VehicleDimensionsId", personalCar.TrunkDimensions.DimensionsId);
        command.Parameters.AddWithValue("@VehicleId", personalCar.VehicleId);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Personal car could not be created!");
        }

        reader.Read();

        var personalCarId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetPersonalCarById(personalCarId);
    }

    /// <summary>
    ///     Get a personal car by its ID.
    /// </summary>
    /// <param name="id">The ID of the personal car to get.</param>
    /// <returns>The personal car with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no personal car with the given ID exists.</exception>
    public static PersonalCar GetPersonalCarById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM PersonalCars" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No personal car with that ID exists!");
        }

        reader.Read();

        var personalCarId = (uint) reader.GetInt32(0);

        var numberOfSeats = reader.GetByte(1);
        var trunkDimensions = GetDimensionsById((uint) reader.GetInt32(2));
        var vehicle = GetVehicleById((uint) reader.GetInt32(3));

        reader.Close();
        connection.Close();

        return new PersonalCar(personalCarId, numberOfSeats, trunkDimensions, vehicle);
    }

    /// <summary>
    ///     Update a personal car in the database.
    /// </summary>
    /// <param name="personalCar">The personal car to update.</param>
    /// <returns>The updated personal car.</returns>
    /// <exception cref="ArgumentException">Thrown if no personal car with the given ID exists.</exception>
    public static PersonalCar UpdatePersonalCar(PersonalCar personalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE PersonalCars" +
                              " SET NumberOfSeats = @NumberOfSeats," +
                              "     TrunkDimensionsId = @TrunkDimensionsId," +
                              "     VehicleId = @VehicleId" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@NumberOfSeats", personalCar.NumberOfSeats);
        command.Parameters.AddWithValue("@TrunkDimensionsId", personalCar.TrunkDimensions);
        command.Parameters.AddWithValue("@VehicleId", personalCar.VehicleId);
        command.Parameters.AddWithValue("@Id", personalCar.PersonalCarId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No personal car with that ID exists!");
        }

        connection.Close();

        return personalCar;
    }

    /// <summary>
    ///     Delete a personal car from the database.
    /// </summary>
    /// <param name="personalCar">The personal car to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no personal car with the given ID exists.</exception>
    public static void DeletePersonalCar(PersonalCar personalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM PersonalCars" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", personalCar);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No personal car with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region PrivatePersonalCar
    /// <summary>
    ///     Get a list of all private personal cars.
    /// </summary>
    /// <returns>A list of all private personal cars.</returns>
    /// <exception cref="ArgumentException">Thrown if no private personal cars exist.</exception>
    public static List<PrivatePersonalCar> GetAllPrivatePersonalCars()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT PersonalCarId FROM PrivatePersonalCars";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No private personal cars exist!");
        }

        var privatePersonalCars = new List<PrivatePersonalCar>();

        while (reader.Read())
        {
            var privatePersonalCarId = (uint) reader.GetInt32(0);
            var hasIsoFittings = reader.GetBoolean(1);
            var personalCar = GetPersonalCarById((uint) reader.GetInt32(2));

            privatePersonalCars.Add(new PrivatePersonalCar(privatePersonalCarId, hasIsoFittings, personalCar));
        }

        reader.Close();
        connection.Close();

        return privatePersonalCars;
    }

    /// <summary>
    ///     Create a private personal car in the database.
    /// </summary>
    /// <param name="privatePersonalCar">The private personal car to create.</param>
    /// <returns>The created private personal car.</returns>
    /// <exception cref="ArgumentException">Thrown if the private personal car could not be created.</exception>
    public static PrivatePersonalCar CreatePrivatePersonalCar(PrivatePersonalCar privatePersonalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO PrivatePersonalCars (" +
                              "    HasIsofixFittings," +
                              "    PersonalCarId" +
                              ")" +
                              " OUTPUT inserted.Id" +
                              " VALUES (" +
                              "    @HasIsofixFittings," +
                              "    @PersonalCarId" +
                              ")";
        command.Parameters.AddWithValue("@HasIsofixFittings", privatePersonalCar.HasIsofixFittings);
        command.Parameters.AddWithValue("@PersonalCarId", privatePersonalCar.PersonalCarId);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Private personal car could not be created!");
        }

        reader.Read();

        var privatePersonalCarId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetPrivatePersonalCarById(privatePersonalCarId);
    }

    /// <summary>
    ///     Get a private personal car by its ID.
    /// </summary>
    /// <param name="id">The ID of the private personal car to get.</param>
    /// <returns>The private personal car with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no private personal car with the given ID exists.</exception>
    public static PrivatePersonalCar GetPrivatePersonalCarById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM PrivatePersonalCars" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No private personal car with that ID exists!");
        }

        reader.Read();

        var privatePersonalCarId = (uint) reader.GetInt32(0);
        var hasIsoFittings = reader.GetBoolean(1);
        var personalCar = GetPersonalCarById((uint) reader.GetInt32(2));

        reader.Close();
        connection.Close();

        return new PrivatePersonalCar(privatePersonalCarId, hasIsoFittings, personalCar);
    }

    /// <summary>
    ///     Update a private personal car in the database.
    /// </summary>
    /// <param name="privatePersonalCar">The private personal car to update.</param>
    /// <returns>The updated private personal car.</returns>
    /// <exception cref="ArgumentException">Thrown if no private personal car with the given ID exists.</exception>
    public static PrivatePersonalCar UpdatePrivatePersonalCar(PrivatePersonalCar privatePersonalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE PrivatePersonalCars" +
                              " SET HasIsofixFittings = @HasIsofixFittings," +
                              "     PersonalCarId = @PersonalCarId" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", privatePersonalCar);
        command.Parameters.AddWithValue("@HasIsofixFittings", privatePersonalCar.HasIsofixFittings);
        command.Parameters.AddWithValue("@PersonalCarId", privatePersonalCar.PersonalCarId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No private personal car with that ID exists!");
        }

        connection.Close();

        return privatePersonalCar;
    }

    /// <summary>
    ///     Delete a private personal car from the database.
    /// </summary>
    /// <param name="privatePersonalCar">The private personal car to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no private personal car with the given ID exists.</exception>
    public static void DeletePrivatePersonalCar(PrivatePersonalCar privatePersonalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM PrivatePersonalCars" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", privatePersonalCar);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No private personal car with that ID exists!");
        }

        connection.Close();
    }
    #endregion

    #region ProfessionalPersonalCar
    /// <summary>
    ///     Get a list of all professional personal cars.
    /// </summary>
    /// <returns>A list of all professional personal cars.</returns>
    /// <exception cref="ArgumentException">Thrown if no professional personal cars exist.</exception>
    public static List<ProfessionalPersonalCar> GetAllProfessionalPersonalCars()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT PersonalCarId FROM ProfessionalPersonalCars";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No professional personal cars exist!");
        }

        var professionalPersonalCars = new List<ProfessionalPersonalCar>();

        while (reader.Read())
        {
            var professionalPersonalCarId = (uint) reader.GetInt32(0);
            var hasSafetyBar = reader.GetBoolean(1);
            var loadCapacity = reader.GetFloat(2);
            var personalCar = GetPersonalCarById((uint) reader.GetInt32(3));

            professionalPersonalCars.Add(new ProfessionalPersonalCar(professionalPersonalCarId, hasSafetyBar, loadCapacity, personalCar));
        }

        reader.Close();
        connection.Close();

        return professionalPersonalCars;
    }

    /// <summary>
    ///     Create a professional personal car in the database.
    /// </summary>
    /// <param name="professionalPersonalCar">The professional personal car to create.</param>
    /// <returns>The created professional personal car.</returns>
    /// <exception cref="ArgumentException">Thrown if the professional personal car could not be created.</exception>
    public static ProfessionalPersonalCar CreateProfessionalPersonalCar(ProfessionalPersonalCar professionalPersonalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO ProfessionalPersonalCars (" +
                              "    HasSafetyBar," +
                              "    LoadCapacity," +
                              "    PersonalCarId" +
                              ")" +
                              " OUTPUT inserted.Id" +
                              " VALUES (" +
                              "    @HasSafetyBar," +
                              "    @LoadCapacity," +
                              "    @PersonalCarId" +
                              ")";
        command.Parameters.AddWithValue("@HasSafetyBar", professionalPersonalCar.HasSafetyBar);
        command.Parameters.AddWithValue("@LoadCapacity", professionalPersonalCar.LoadCapacity);
        command.Parameters.AddWithValue("@PersonalCarId", professionalPersonalCar.PersonalCarId);

        var reader = command.ExecuteReader();

        // If it fails, throw an exception and close the connection.
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Professional personal car could not be created!");
        }

        reader.Read();

        var professionalPersonalCarId = (uint)reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetProfessionalPersonalCarById(professionalPersonalCarId);
    }

    /// <summary>
    ///     Get a professional personal car by its ID.
    /// </summary>
    /// <param name="id">The ID of the professional personal car to get.</param>
    /// <returns>The professional personal car with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown if no professional personal car with the given ID exists.</exception>
    public static ProfessionalPersonalCar GetProfessionalPersonalCarById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ProfessionalPersonalCars" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No professional personal car with that ID exists!");
        }

        reader.Read();

        var professionalPersonalCarId = (uint) reader.GetInt32(0);
        var hasSafetyBar = reader.GetBoolean(1);
        var loadCapacity = reader.GetFloat(2);
        var personalCar = GetPersonalCarById((uint) reader.GetInt32(3));

        reader.Close();
        connection.Close();

        return new ProfessionalPersonalCar(professionalPersonalCarId, hasSafetyBar, loadCapacity, personalCar);
    }

    /// <summary>
    ///     Update a professional personal car in the database.
    /// </summary>
    /// <param name="professionalPersonalCar">The professional personal car to update.</param>
    /// <returns>The updated professional personal car.</returns>
    /// <exception cref="ArgumentException">Thrown if no professional personal car with the given ID exists.</exception>
    public static ProfessionalPersonalCar UpdateProfessionalPersonalCar(ProfessionalPersonalCar professionalPersonalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE ProfessionalPersonalCars" +
                              " SET HasSafetyBar = @HasSafetyBar," +
                              "     LoadCapacity = @LoadCapacity," +
                              "     PersonalCarId = @PersonalCarId" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@HasSafetyBar", professionalPersonalCar.HasSafetyBar);
        command.Parameters.AddWithValue("@LoadCapacity", professionalPersonalCar.LoadCapacity);
        command.Parameters.AddWithValue("@PersonalCarId", professionalPersonalCar.PersonalCarId);
        command.Parameters.AddWithValue("@Id", professionalPersonalCar.ProfessionalPersonalCarId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("No professional personal car with that ID exists!");
        }

        connection.Close();

        return professionalPersonalCar;
    }

    /// <summary>
    ///     Delete a professional personal car from the database.
    /// </summary>
    /// <param name="professionalPersonalCar">The professional personal car to delete.</param>
    /// <exception cref="ArgumentException">Thrown if no professional personal car with the given ID exists.</exception>
    public static void DeleteProfessionalPersonalCar(ProfessionalPersonalCar professionalPersonalCar)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM ProfessionalPersonalCars" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", professionalPersonalCar.ProfessionalPersonalCarId);

        // If it fails, throw an exception.
        if (command.ExecuteNonQuery() == 0)
        {
            throw new ArgumentException("No professional personal car with that ID exists!");
        }

        connection.Close();
    }
    #endregion
}
