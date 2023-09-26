using Data.Classes;

namespace Utility.DatabaseManager;

/// <summary>
///     The part of the database manager that handles users.
/// </summary>
public partial class DatabaseManager
{
    #region User
    /// <summary>
    ///     Gets all users from the database.
    /// </summary>
    /// <returns>A list of users.</returns>
    /// <exception cref="ArgumentException">Thrown when no users exist.</exception>
    public static List<User> GetAllUsers()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Users";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No users exist!");
        }

        var users = new List<User>();

        while (reader.Read())
        {
            var userId = (uint)reader.GetInt32(0);
            var username = reader.GetString(1);
            var password = reader.GetString(2);
            var zipcode = (uint)reader.GetInt32(3);
            var balance = reader.GetDecimal(4);

            users.Add(new User(userId, username, password, zipcode, balance));
        }

        reader.Close();

        return users;
    }

    /// <summary>
    ///     Creates a user in the database.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <exception cref="ArgumentException">Thrown when the user already exists.</exception>
    public static void CreateUser(User user)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Users (Username, Password, Zipcode, Balance) VALUES (@Username, @Password, @Zipcode, @Balance)";
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Password", user.PasswordHash);
        command.Parameters.AddWithValue("@Zipcode", user.Zipcode);
        command.Parameters.AddWithValue("@Balance", user.Balance);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("User already exists!");
        }

        connection.Close();
    }

    /// <summary>
    ///     Gets a user from the database by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to get.</param>
    /// <returns>The user with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown when the user does not exist.</exception>
    public static User GetUserById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Users WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        reader.Read();

        var userId = (uint)reader.GetInt32(0);
        var username = reader.GetString(1);
        var password = reader.GetString(2);
        var zipcode = (uint)reader.GetInt32(3);
        var balance = reader.GetDecimal(4);

        reader.Close();
        connection.Close();

        return new User(userId, username, password, zipcode, balance);
    }

    /// <summary>
    ///     Gets a user from the database by their username.
    /// </summary>
    /// <param name="name">The username of the user to get.</param>
    /// <returns>The user with the given username.</returns>
    /// <exception cref="ArgumentException">Thrown when the user does not exist.</exception>
    public static User GetUserByName(string name)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Users WHERE Username = @Username";
        command.Parameters.AddWithValue("@Username", name);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        reader.Read();

        var userId = (uint)reader.GetInt32(0);
        var username = reader.GetString(1);
        var password = reader.GetString(2);
        var zipcode = (uint)reader.GetInt32(3);
        var balance = reader.GetDecimal(4);

        reader.Close();
        connection.Close();

        return new User(userId, username, password, zipcode, balance);
    }

    /// <summary>
    ///     Updates a user in the database.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <returns>The updated user.</returns>
    /// <exception cref="ArgumentException">Thrown when the user does not exist.</exception>
    public static User UpdateUser(User user)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Users SET Username = @Username, Password = @Password, Zipcode = @Zipcode, Balance = @Balance WHERE Id = @Id";
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Password", user.PasswordHash);
        command.Parameters.AddWithValue("@Zipcode", user.Zipcode);
        command.Parameters.AddWithValue("@Balance", user.Balance);
        command.Parameters.AddWithValue("@Id", user.UserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        connection.Close();

        return user;
    }

    /// <summary>
    ///     Deletes a user from the database.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <exception cref="ArgumentException">Thrown when the user does not exist.</exception>
    public static void DeleteUser(User user)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Users WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", user.UserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        connection.Close();
    }
    #endregion

    #region PrivateUser
    /// <summary>
    ///     Gets all private users from the database.
    /// </summary>
    /// <returns>A list of private users.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static List<PrivateUser> GetAllPrivateUsers()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM PrivateUsers";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No private users exist!");
        }

        var privateUsers = new List<PrivateUser>();

        while (reader.Read())
        {
            var privateUserId = (uint)reader.GetInt32(0);
            var cpr = reader.GetString(1);
            var userId = (uint)reader.GetInt32(2);

            var user = GetUserById(userId);

            privateUsers.Add(new PrivateUser(privateUserId, cpr, user));
        }

        reader.Close();

        return privateUsers;
    }

    /// <summary>
    ///     Creates a private user in the database.
    /// </summary>
    /// <param name="privateUser">The private user to create.</param>
    /// <exception cref="ArgumentException">Thrown when the private user already exists.</exception>
    public static void CreatePrivateUser(PrivateUser privateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO PrivateUsers (Cpr, UserId) VALUES (@Cpr, @UserId)";
        command.Parameters.AddWithValue("@Cpr", privateUser.Cpr);
        command.Parameters.AddWithValue("@UserId", privateUser.UserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Private user already exists!");
        }

        connection.Close();
    }

    /// <summary>
    ///     Gets a private user from the database by their ID.
    /// </summary>
    /// <param name="id">The ID of the private user to get.</param>
    /// <returns>The private user with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown when the private user does not exist.</exception>
    public static PrivateUser GetPrivateUserById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM PrivateUsers WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Private user does not exist!");
        }

        reader.Read();

        var privateUserId = (uint)reader.GetInt32(0);
        var cpr = reader.GetString(1);
        var userId = (uint)reader.GetInt32(2);

        reader.Close();
        connection.Close();

        var user = GetUserById(userId);

        return new PrivateUser(privateUserId, cpr, user);
    }

    /// <summary>
    ///     Gets a private user from the database by their CPR number.
    /// </summary>
    /// <param name="cpr">The CPR number of the private user to get.</param>
    /// <returns>The private user with the given CPR number.</returns>
    /// <exception cref="ArgumentException">Thrown when the private user does not exist.</exception>
    public static PrivateUser GetPrivateUserByCpr(string cpr)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM PrivateUsers WHERE Cpr = @Cpr";
        command.Parameters.AddWithValue("@Cpr", cpr);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Private user does not exist!");
        }

        reader.Read();

        var privateUserId = (uint)reader.GetInt32(0);
        var cprNumber = reader.GetString(1);
        var userId = (uint)reader.GetInt32(2);

        reader.Close();
        connection.Close();

        var user = GetUserById(userId);

        return new PrivateUser(privateUserId, cprNumber, user);
    }

    /// <summary>
    ///     Updates a private user in the database.
    /// </summary>
    /// <param name="privateUser">The private user to update.</param>
    /// <returns>The updated private user.</returns>
    /// <exception cref="ArgumentException">Thrown when the private user does not exist.</exception>
    public static PrivateUser UpdatePrivateUser(PrivateUser privateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE PrivateUsers SET Cpr = @Cpr, UserId = @UserId WHERE Id = @Id";
        command.Parameters.AddWithValue("@Cpr", privateUser.Cpr);
        command.Parameters.AddWithValue("@UserId", privateUser.UserId);
        command.Parameters.AddWithValue("@Id", privateUser.PrivateUserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Private user does not exist!");
        }

        connection.Close();

        return privateUser;
    }

    /// <summary>
    ///     Deletes a private user from the database.
    /// </summary>
    /// <param name="privateUser">The private user to delete.</param>
    /// <exception cref="ArgumentException">Thrown when the private user does not exist.</exception>
    public static void DeletePrivateUser(PrivateUser privateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM PrivateUsers WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", privateUser.PrivateUserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Private user does not exist!");
        }

        connection.Close();
    }
    #endregion

    #region CorporateUser
    /// <summary>
    ///     Gets all corporate users from the database.
    /// </summary>
    /// <returns>A list of corporate users.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static List<CorporateUser> GetAllCorporateUsers()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM CorporateUsers";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No corporate users exist!");
        }

        var corporateUsers = new List<CorporateUser>();

        while (reader.Read())
        {
            var corporateUserId = (uint)reader.GetInt32(0);
            var cvrNumber = reader.GetString(1);
            var credit = reader.GetDecimal(2);
            var userId = (uint)reader.GetInt32(3);

            var user = GetUserById(userId);

            corporateUsers.Add(new CorporateUser(corporateUserId, cvrNumber, credit, user));
        }

        reader.Close();

        return corporateUsers;
    }

    /// <summary>
    ///     Creates a corporate user in the database.
    /// </summary>
    /// <param name="corporateUser">The corporate user to create.</param>
    /// <exception cref="ArgumentException">Thrown when the corporate user already exists.</exception>
    public static void CreateCorporateUser(CorporateUser corporateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO CorporateUsers (Cvr, Credit, UserId) VALUES (@Cvr, @Credit, @UserId)";
        command.Parameters.AddWithValue("@Cvr", corporateUser.CvrNumber);
        command.Parameters.AddWithValue("@Credit", corporateUser.Credit);
        command.Parameters.AddWithValue("@UserId", corporateUser.UserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Corporate user already exists!");
        }

        connection.Close();
    }

    /// <summary>
    ///     Gets a corporate user from the database by their ID.
    /// </summary>
    /// <param name="id">The ID of the corporate user to get.</param>
    /// <returns>The corporate user with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown when the corporate user does not exist.</exception>
    public static CorporateUser GetCorporateUserById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM CorporateUsers WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Corporate user does not exist!");
        }

        reader.Read();

        var corporateUserId = (uint)reader.GetInt32(0);
        var cvrNumber = reader.GetString(1);
        var credit = reader.GetDecimal(2);
        var userId = (uint)reader.GetInt32(3);

        reader.Close();
        connection.Close();

        var user = GetUserById(userId);

        return new CorporateUser(corporateUserId, cvrNumber, credit, user);
    }

    /// <summary>
    ///     Gets a list of corporate users from the database by their CVR number.
    /// </summary>
    /// <param name="cvr">The CVR number of the corporate users to get.</param>
    /// <returns>A list of corporate users with the given CVR number.</returns>
    /// <exception cref="ArgumentException">Thrown when the corporate users do not exist.</exception>
    public static List<CorporateUser> GetCorporateUsersByCvr(string cvr)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM CorporateUsers WHERE Cvr = @Cvr";
        command.Parameters.AddWithValue("@Cvr", cvr);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Corporate users do not exist!");
        }

        var corporateUsers = new List<CorporateUser>();

        while (reader.Read())
        {
            var corporateUserId = (uint)reader.GetInt32(0);
            var cvrNumber = reader.GetString(1);
            var credit = reader.GetDecimal(2);
            var userId = (uint)reader.GetInt32(3);

            var user = GetUserById(userId);

            corporateUsers.Add(new CorporateUser(corporateUserId, cvrNumber, credit, user));
        }

        reader.Close();
        connection.Close();

        return corporateUsers;
    }

    /// <summary>
    ///     Updates a corporate user in the database.
    /// </summary>
    /// <param name="corporateUser">The corporate user to update.</param>
    /// <returns>The updated corporate user.</returns>
    /// <exception cref="ArgumentException">Thrown when the corporate user does not exist.</exception>
    public static CorporateUser UpdateCorporateUser(CorporateUser corporateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE CorporateUsers SET Cvr = @Cvr, Credit = @Credit, UserId = @UserId WHERE Id = @Id";
        command.Parameters.AddWithValue("@Cvr", corporateUser.CvrNumber);
        command.Parameters.AddWithValue("@Credit", corporateUser.Credit);
        command.Parameters.AddWithValue("@UserId", corporateUser.UserId);
        command.Parameters.AddWithValue("@Id", corporateUser.CorporateUserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Corporate user does not exist!");
        }

        connection.Close();

        return corporateUser;
    }

    /// <summary>
    ///     Deletes a corporate user from the database.
    /// </summary>
    /// <param name="corporateUser">The corporate user to delete.</param>
    /// <exception cref="ArgumentException">Thrown when the corporate user does not exist.</exception>
    public static void DeleteCorporateUser(CorporateUser corporateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM CorporateUsers WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", corporateUser.CorporateUserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Corporate user does not exist!");
        }

        connection.Close();
    }
    #endregion
}
