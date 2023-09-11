using System.Data;
using System.Data.SqlClient;
using Data.Classes.Vehicles;

namespace Utility.DatabaseManager;

public partial class DatabaseManager
{
    /// <summary>
    ///     Get the license type from the database by id.
    /// </summary>
    /// <param name="id">The id of the license type.</param>
    /// <returns>The license type.</returns>
    /// <exception cref="DataException">If the license type is not found.</exception>
    public LicenseType GetLicenseTypeById(uint id)
    {
        var connection = Instance.GetConnection();

        var query = "SELECT * FROM LicenseTypes WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("License type not found!");
        }

        reader.Read();

        var licenseType = (LicenseType) reader.GetByte(0);

        reader.Close();

        return licenseType;
    }

    /// <summary>
    ///     Get the fuel type from the database by id.
    /// </summary>
    /// <param name="id">The id of the fuel type.</param>
    /// <returns>The fuel type.</returns>
    /// <exception cref="DataException">If the fuel type is not found.</exception>
    public FuelType GetFuelTypeById(uint id)
    {
        var connection = GetConnection();

        var query = "SELECT * FROM FuelTypes WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Fuel type not found!");
        }

        reader.Read();

        var fuelType = (FuelType) reader.GetByte(0);

        reader.Close();

        return fuelType;
    }

    /// <summary>
    ///     Get the energy type from the database by id.
    /// </summary>
    /// <param name="id">The id of the energy type.</param>
    /// <returns>The energy type.</returns>
    /// <exception cref="DataException">If the energy type is not found.</exception>
    public EnergyType GetEnergyTypeById(uint id)
    {
        var connection = GetConnection();

        var query = "SELECT * FROM EnergyTypes WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Energy type not found!");
        }

        reader.Read();

        var energyType = (EnergyType) reader.GetByte(0);

        reader.Close();

        return energyType;
    }

    /// <summary>
    ///     Get the dimensions from the database by id.
    /// </summary>
    /// <param name="id">The id of the dimensions.</param>
    /// <returns>The dimensions.</returns>
    /// <exception cref="DataException">If the dimensions is not found.</exception>
    public Dimensions GetDimensionsById(uint id)
    {
        var connection = GetConnection();

        var query = "SELECT * FROM Dimensions WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Dimensions not found!");
        }

        reader.Read();

        var dimensions = new Dimensions(reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(3));

        reader.Close();

        return dimensions;
    }

    /// <summary>
    ///     Get the vehicle from the database by id.
    /// </summary>
    /// <param name="id">The id of the vehicle.</param>
    /// <returns>The vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not found.</exception>
    public ProfessionalPersonalCar GetProfessionalPersonalCarById(uint id)
    {
        var connection = GetConnection();

        var query =
            "SELECT PPC.HasSafetyBar, PPC.LoadCapacity," +
            "       PC.NumberOfSeats," +
            "       D.Length, D.Width, D.Height," +
            "       V.Name, V.Km, V.RegistrationNumber, V.Year, V.NewPrice, V.HasTowbar, V.EngineSize, V.KmPerLiter, V.LicenseTypeId, V.FuelTypeId, V.EnergyTypeId, " +
            "       LT.Id, FT.Id" +
            " FROM ProfessionalPersonalCars AS PPC" +
            "      JOIN PersonalCars PC on PPC.PersonalCarId = PC.Id" +
            "      JOIN Dimensions D on D.Id = PC.TrunkDimensionsId" +
            "      JOIN Vehicles V on PC.VehicleId = V.Id" +
            "      JOIN LicenseTypes LT on V.LicenseTypeId = LT.Id" +
            "      JOIN FuelTypes FT on V.FuelTypeId = FT.Id" +
            " WHERE PPC.Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Professional Personal Car not found!");
        }

        reader.Read();

        var hasSafetyBar = reader.GetBoolean(1);
        var loadCapacity = reader.GetFloat(2);

        var numberOfSeats = reader.GetByte(3);

        var dimensions = new Dimensions(reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6));

        var name = reader.GetString(3);
        var km = reader.GetDouble(4);
        var registrationNumber = reader.GetString(5);
        var year = (ushort) reader.GetSqlInt16(6).Value;
        var newPrice = reader.GetDecimal(7);
        // var hasTowbar = reader.GetBoolean(8);
        var engineSize = reader.GetDouble(9);
        var kmPerLiter = reader.GetDouble(10);

        var licenseType = (LicenseType)reader.GetByte(15);
        var fuelType = (FuelType)reader.GetByte(16);

        return new ProfessionalPersonalCar(name, km, registrationNumber, year, newPrice, engineSize, kmPerLiter,
            fuelType, numberOfSeats, dimensions, hasSafetyBar, loadCapacity, licenseType);
    }

    /// <summary>
    ///     Get the vehicle from the database by id.
    /// </summary>
    /// <param name="id">The id of the vehicle.</param>
    /// <returns>The vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not found.</exception>
    public PrivatePersonalCar GetPrivatePersonalCarById(uint id)
    {
        var connection = GetConnection();

        var query =
            "SELECT PPC.HasIsofixFittings," +
            "       PC.NumberOfSeats," +
            "       D.Length, D.Width, D.Height," +
            "       V.Name, V.Km, V.RegistrationNumber, V.Year, V.NewPrice, V.HasTowbar, V.EngineSize, V.KmPerLiter" +
            "       FT.Id" +
            " FROM PrivatePersonalCars PPC" +
            "     JOIN PersonalCars PC on PPC.PersonalCarId = PC.Id" +
            "     JOIN Dimensions D on D.Id = PC.TrunkDimensionsId" +
            "     JOIN Vehicles V on PC.VehicleId = V.Id" +
            "     JOIN FuelTypes FT on V.FuelTypeId = FT.Id" +
            " WHERE PPC.Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Private Personal Car not found!");
        }

        reader.Read();

        var hasIsofixFittings = reader.GetBoolean(1);

        var numberOfSeats = reader.GetByte(2);

        var dimensions = new Dimensions(reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5));

        var name = reader.GetString(3);
        var km = reader.GetDouble(4);
        var registrationNumber = reader.GetString(5);
        var year = (ushort) reader.GetSqlInt16(6).Value;
        var newPrice = reader.GetDecimal(7);
        var hasTowbar = reader.GetBoolean(8);
        var engineSize = reader.GetDouble(9);
        var kmPerLiter = reader.GetDouble(10);

        var fuelType = (FuelType)reader.GetByte(14);

        return new PrivatePersonalCar(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter,
            fuelType, numberOfSeats, dimensions, hasIsofixFittings);
    }

    /// <summary>
    ///     Get the vehicle from the database by id.
    /// </summary>
    /// <param name="id">The id of the vehicle.</param>
    /// <returns>The vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not found.</exception>
    public Truck GetTruckById(uint id)
    {
        var connection = GetConnection();

        var query =
            "SELECT T.LoadCapacity," +
            "       V.Name, V.Km, V.RegistrationNumber, V.Year, V.NewPrice, V.HasTowbar, V.EngineSize, V.KmPerLiter," +
            "       D.Length, D.Width, D.Height," +
            "       FT.Id" +
            " FROM Trucks T" +
            "     JOIN HeavyVehicles HV ON T.HeavyVehicleId = HV.Id" +
            "     JOIN Dimensions D on D.Id = HV.VehicleDimensionsId" +
            "     JOIN Vehicles V on HV.VehicleId = V.Id" +
            "     JOIN FuelTypes FT on V.FuelTypeId = FT.Id" +
            " WHERE T.Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Truck not found!");
        }

        reader.Read();

        var loadCapacity = reader.GetFloat(0);

        var name = reader.GetString(3);
        var km = reader.GetDouble(4);
        var registrationNumber = reader.GetString(5);
        var year = (ushort) reader.GetSqlInt16(6).Value;
        var newPrice = reader.GetDecimal(7);
        var hasTowbar = reader.GetBoolean(8);
        var engineSize = reader.GetDouble(9);
        var kmPerLiter = reader.GetDouble(10);

        var dimensions = new Dimensions(reader.GetDouble(9), reader.GetDouble(10), reader.GetDouble(11));

        var fuelType = (FuelType)reader.GetByte(12);

        return new Truck(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, dimensions, loadCapacity);
    }

    /// <summary>
    ///     Get the vehicle from the database by id.
    /// </summary>
    /// <param name="id">The id of the vehicle.</param>
    /// <returns>The vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not found.</exception>
    public Bus GetBusById(uint id)
    {
        var connection = GetConnection();

        var query =
            "SELECT B.NumberOfSeats, B.NumberOfSleepingSpaces, B.HasToilet," +
            "       V.Name, V.Km, V.RegistrationNumber, V.Year, V.NewPrice, V.HasTowbar, V.EngineSize, V.KmPerLiter," +
            "       D.Length, D.Width, D.Height," +
            "       FT.Id" +
            " FROM Buses B" +
            "     JOIN HeavyVehicles HV ON B.HeavyVehicleId = HV.Id" +
            "     JOIN Dimensions D on D.Id = HV.VehicleDimensionsId" +
            "     JOIN Vehicles V on HV.VehicleId = V.Id" +
            "     JOIN FuelTypes FT on V.FuelTypeId = FT.Id" +
            " WHERE B.Id = @Id";

        var command = new SqlCommand(query, connection);

        var idParameter = new SqlParameter("@Id", SqlDbType.Int, 0) { Value = id };
        command.Parameters.Add(idParameter);

        command.Prepare();

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Bus not found!");
        }

        reader.Read();

        var numberOfSeats = reader.GetByte(0);
        var numberOfSleepingSpaces = reader.GetByte(1);
        var hasToilet = reader.GetBoolean(2);

        var name = reader.GetString(3);
        var km = reader.GetDouble(4);
        var registrationNumber = reader.GetString(5);
        var year = (ushort) reader.GetSqlInt16(6).Value;
        var newPrice = reader.GetDecimal(7);
        var hasTowbar = reader.GetBoolean(8);
        var engineSize = reader.GetDouble(9);
        var kmPerLiter = reader.GetDouble(10);

        var dimensions = new Dimensions(reader.GetDouble(11), reader.GetDouble(12), reader.GetDouble(13));

        var fuelType = (FuelType)reader.GetByte(14);

        return new Bus(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, dimensions, numberOfSeats, numberOfSleepingSpaces, hasToilet);
    }
}