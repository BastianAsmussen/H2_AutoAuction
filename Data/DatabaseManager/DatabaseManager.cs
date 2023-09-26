using System.Data.SqlClient;
using System.Security.Authentication;
using dotenv.net;

namespace Data.DatabaseManager;

/// <summary>
///     Singleton class for handling database connections.
/// </summary>
public partial class DatabaseManager
{
    private static DatabaseManager? _instance;
    public static DatabaseManager Instance => _instance ??= new DatabaseManager();

    private SqlConnection? _connection;

    private DatabaseManager()
    {
        _instance ??= this;
    }

    /// <summary>
    ///     Returns a connection to the database.
    /// </summary>
    /// <returns>The connection.</returns>
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

    /// <summary>
    ///    Returns the credentials for the database.
    /// </summary>
    /// <returns>The credentials.</returns>
    /// <exception cref="InvalidCredentialException">If the credentials are invalid.</exception>
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
}
