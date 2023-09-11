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
}
