using System.Data.SqlClient;
using System.Security.Authentication;
using DotNetEnv;

namespace Utility;

public class DatabaseManager
{
    private static DatabaseManager? _instance = null;
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
        var credentials = DotNetEnv.Env.Load(".env");
        var keyValuePairs = credentials.ToList();

        // Check if credentials are valid.
        var host = keyValuePairs.ToDictionary().GetValueOrDefault("SQL_HOST");
        if (string.IsNullOrEmpty(host))
        {
            throw new InvalidCredentialException("SQL Host is invalid!");
        }

        var port = keyValuePairs.ToDictionary().GetValueOrDefault("SQL_PORT");
        if (port == null || string.IsNullOrEmpty(port) || !uint.TryParse(port, out _))
        {
            throw new InvalidCredentialException("SQL Port is invalid!");
        }

        var database = keyValuePairs.ToDictionary().GetValueOrDefault("SQL_DATABASE");
        if (string.IsNullOrEmpty(database))
        {
            throw new InvalidCredentialException("SQL Database is invalid!");
        }

        var user = keyValuePairs.ToDictionary().GetValueOrDefault("SQL_USER");
        if (string.IsNullOrEmpty(user))
        {
            throw new InvalidCredentialException("SQL User is invalid!");
        }

        var password = keyValuePairs.ToDictionary().GetValueOrDefault("SQL_PASSWORD");
        if (string.IsNullOrEmpty(password))
        {
            throw new InvalidCredentialException("SQL Password is invalid!");
        }

        return (host, uint.Parse(port), database, user, password);
    }
}
