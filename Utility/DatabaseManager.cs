using System.Data.SqlClient;

namespace Utility;

public class DatabaseManager
{
    private static DatabaseManager? _instance;
    public static DatabaseManager Instance => _instance ??= new DatabaseManager();

    // TODO: Add the proper connection string.
    public SqlConnection Connection { get; } = new SqlConnection("Server=localhost;Database=H2_AutoAuction;Trusted_Connection=True;");

    private DatabaseManager()
    {
        _instance ??= this;
    }
}
