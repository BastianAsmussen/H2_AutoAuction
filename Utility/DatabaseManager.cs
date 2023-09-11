using System.Data;
using System.Data.SqlClient;
using System.Security.Authentication;
using Data.Classes.Vehicles;
using dotenv.net;

namespace Utility;

public class DatabaseManager
{
    private static DatabaseManager? _instance;
    public static DatabaseManager Instance => _instance ??= new DatabaseManager();

    private SqlConnection? _connection;

    private DatabaseManager()
    {
        _instance ??= this;
    }

    public SqlConnection GetConnection()
    {
        var credentials = GetCredentials();

        SqlConnectionStringBuilder sb = new()
        {
            DataSource = $"{credentials.Item1},{credentials.Item2.ToString()}",
            InitialCatalog = credentials.Item3,
            UserID = credentials.Item4,
            Password = credentials.Item5,
        };

        var connectionString = sb.ToString();

        _connection = new SqlConnection(connectionString);
        _connection.Open();

        return _connection;
    }

    private static (string, uint, string, string, string) GetCredentials()
    {
        // Fetch credentials from file.
        DotEnv.Load();

        var env = DotEnv.Read();

        // Check if credentials are valid.
        var host = env["SQL_HOST"];
        if (string.IsNullOrWhiteSpace(host))
        {
            throw new InvalidCredentialException("SQL Host is invalid!");
        }

        var port = env["SQL_PORT"];
        if (!uint.TryParse(port, out var _))
        {
            throw new InvalidCredentialException("SQL Port is invalid!");
        }

        var database = env["SQL_DATABASE"];
        if (string.IsNullOrWhiteSpace(database))
        {
            throw new InvalidCredentialException("SQL Database is invalid!");
        }

        var username = env["SQL_USERNAME"];
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidCredentialException("SQL User is invalid!");
        }

        var password = env["SQL_PASSWORD"];
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidCredentialException("SQL Password is invalid!");
        }

        return (host, uint.Parse(port), database, username, password);
    }

    /// <summary>
    ///     Get the license type from the database by id.
    /// </summary>
    /// <param name="id">The id of the license type.</param>
    /// <returns>The license type.</returns>
    /// <exception cref="DataException">If the license type is not found.</exception>
    public LicenseType GetLicenseType(uint id)
    {
        var connection = GetConnection();

        var query = "SELECT * FROM LicenseTypes WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("License type not found!");
        }

        reader.Read();

        var licenseType = (LicenseType) reader.GetInt32(0);

        reader.Close();

        return licenseType;
    }

    /// <summary>
    ///     Get the fuel type from the database by id.
    /// </summary>
    /// <param name="id">The id of the fuel type.</param>
    /// <returns>The fuel type.</returns>
    /// <exception cref="DataException">If the fuel type is not found.</exception>
    public FuelType GetFuelType(uint id)
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

        var fuelType = (FuelType) reader.GetInt32(0);

        reader.Close();

        return fuelType;
    }

    /// <summary>
    ///     Get the energy type from the database by id.
    /// </summary>
    /// <param name="id">The id of the energy type.</param>
    /// <returns>The energy type.</returns>
    /// <exception cref="DataException">If the energy type is not found.</exception>
    public EnergyType GetEnergyType(uint id)
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

        var energyType = (EnergyType) reader.GetInt32(0);

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
    public ProfessionalPersonalCar GetProfessionalPersonalCar(uint id)
    {
        var connection = GetConnection();

        // A professional personal car is a personal car with a load capacity and a safety bar, join the tables to get the data.
        // Tables: Vehicles -> PersonalCars -> ProfessionalPersonalCars
        var query = "SELECT * FROM Vehicles " +
                    "INNER JOIN PersonalCars ON Vehicles.Id = PersonalCars.Id " +
                    "INNER JOIN ProfessionalPersonalCars ON PersonalCars.Id = ProfessionalPersonalCars.Id " +
                    "WHERE Vehicles.Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Professional personal car not found!");
        }

        reader.Read();

        var name = reader.GetString(1);
        var km = reader.GetDouble(2);
        var registrationNumber = reader.GetString(3);
        var year = (ushort) reader.GetInt16(4);
        var newPrice = reader.GetDecimal(5);
        var engineSize = reader.GetDouble(6);
        var kmPerLiter = reader.GetDouble(7);
        var fuelType = GetFuelType((uint)reader.GetInt32(8));
        var numberOfSeat = (ushort) reader.GetInt16(9);
        var trunkDimensions = GetDimensionsById((uint)reader.GetInt32(10));
        var loadCapacity = reader.GetDouble(11);
        var hasSafetyBar = reader.GetBoolean(12);
        var driversLicense = GetLicenseType((uint)reader.GetInt32(13));

        reader.Close();

        return new ProfessionalPersonalCar(name, km, registrationNumber, year, newPrice, engineSize, kmPerLiter, fuelType, numberOfSeat, trunkDimensions, hasSafetyBar, loadCapacity, driversLicense);
    }

    /// <summary>
    ///     Get the private personal car from the database by id.
    /// </summary>
    /// <param name="id">The id of the private personal car.</param>
    /// <returns>The private personal car.</returns>
    /// <exception cref="DataException">If the private personal car is not found.</exception>
    public PrivatePersonalCar GetPrivatePersonalCar(uint id)
    {
        var connection = GetConnection();

        // A private personal car is a personal car with a hasIsofixFittings, join the tables to get the data.
        // Tables: Vehicles -> PersonalCars -> PrivatePersonalCars
        var query = "SELECT * FROM Vehicles " +
                    "INNER JOIN PersonalCars ON Vehicles.Id = PersonalCars.Id " +
                    "INNER JOIN PrivatePersonalCars ON PersonalCars.Id = PrivatePersonalCars.Id " +
                    "WHERE Vehicles.Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Private personal car not found!");
        }

        reader.Read();

        var name = reader.GetString(1);
        var km = reader.GetDouble(2);
        var registrationNumber = reader.GetString(3);
        var year = (ushort) reader.GetInt16(4);
        var hasTowbar = reader.GetBoolean(5);
        var newPrice = reader.GetDecimal(6);
        var engineSize = reader.GetDouble(7);
        var kmPerLiter = reader.GetDouble(8);
        var fuelType = GetFuelType((uint) reader.GetInt32(9));
        var numberOfSeats = (ushort) reader.GetInt16(10);
        var trunkDimensions = GetDimensionsById((uint) reader.GetInt32(11));
        var hasIsofixFittings = reader.GetBoolean(12);

        reader.Close();

        return new PrivatePersonalCar(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, numberOfSeats, trunkDimensions, hasIsofixFittings);
    }

    public Truck GetTruckById(uint id)
    {
        var connection = GetConnection();

        // A truck is a vehicle with a load capacity, join the tables to get the data.
        // Tables: Vehicles -> HeavyVehicles -> Trucks
        var query = "SELECT * FROM Vehicles " +
                    "INNER JOIN HeavyVehicles ON Vehicles.Id = HeavyVehicles.Id " +
                    "INNER JOIN Trucks ON HeavyVehicles.Id = Trucks.Id " +
                    "WHERE Vehicles.Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Truck not found!");
        }

        reader.Read();

        var name = reader.GetString(1);
        var km = reader.GetDouble(2);
        var registrationNumber = reader.GetString(3);
        var year = (ushort) reader.GetInt16(4);
        var newPrice = reader.GetDecimal(5);
        var hasTowbar = reader.GetBoolean(6);
        var engineSize = reader.GetDouble(7);
        var kmPerLiter = reader.GetDouble(8);
        var fuelType = GetFuelType((uint) reader.GetInt32(9));
        var numberOfSeats = (ushort) reader.GetInt16(10);
        var vehicleDimensions = GetDimensionsById((uint) reader.GetInt32(11));
        var loadCapacity = reader.GetDouble(12);

        reader.Close();

        return new Truck(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, vehicleDimensions, loadCapacity);
    }
}
