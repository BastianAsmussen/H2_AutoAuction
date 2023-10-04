using System.Data;
using System.Security.Authentication;
using BCrypt.Net;
using Data.Classes;

namespace Data.DatabaseManager;

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
            var userId = reader.GetInt32(0);
            var username = reader.GetString(1);
            var password = reader.GetString(2);
            var zipcode = reader.GetString(3);
            var balance = reader.GetDecimal(4);

            users.Add(new User(userId, username, password, zipcode, balance));
        }

        reader.Close();
        connection.Close();

        return users;
    }

    /// <summary>
    ///     Creates a user in the database.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>The created user.</returns>
    /// <exception cref="ArgumentException">Thrown when the user already exists.</exception>
    public static User CreateUser(User user)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Users (" +
                              "    Username," +
                              "    Password," +
                              "    Zipcode," +
                              "    Balance" +
                              ")" +
                              " OUTPUT inserted.Id" +
                              " VALUES (" +
                              "    @Username," +
                              "    @Password," +
                              "    @Zipcode," +
                              "    @Balance" +
                              ")";
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Password", user.Password);
        command.Parameters.AddWithValue("@Zipcode", user.Zipcode);
        command.Parameters.AddWithValue("@Balance", user.Balance);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("User already exists!");
        }

        reader.Read();

        var userId = reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return new User(userId, user.Username, user.Password, user.Zipcode, user.Balance);
    }

    /// <summary>
    ///     Gets a user from the database by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to get.</param>
    /// <returns>The user with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown when the user does not exist.</exception>
    public static User GetUserById(int id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Users" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        reader.Read();

        var userId = reader.GetInt32(0);
        var username = reader.GetString(1);
        var password = reader.GetString(2);
        var zipcode = reader.GetString(3);
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
        command.CommandText = "SELECT * FROM Users" +
                              " WHERE Username = @Username";
        command.Parameters.AddWithValue("@Username", name);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        reader.Read();

        var userId = reader.GetInt32(0);
        var username = reader.GetString(1);
        var password = reader.GetString(2);
        var zipcode = reader.GetString(3);
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
        command.CommandText = "UPDATE Users" +
                              " SET Username = @Username," +
                              "     Password = @Password," +
                              "     Zipcode = @Zipcode," +
                              "     Balance = @Balance" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Password", user.Password);
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
        command.CommandText = "DELETE FROM Users" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", user.UserId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        connection.Close();
    }
    #endregion

    #region Login
    /// <summary>
    ///     Logs a user in.
    /// </summary>
    /// <param name="username">The username of the user to log in.</param>
    /// <param name="password">The password of the user to log in.</param>
    /// <returns>The logged in user, either a private user or a corporate user.</returns>
    /// <exception cref="ArgumentException">Thrown when the username or password is null or empty.</exception>
    /// <exception cref="DataException">Thrown when the user does not exist.</exception>
    /// <exception cref="InvalidCredentialException"></exception>
    public static User Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be empty!");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be empty!");
        }

        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();

        // Get the hashed password from the database.
        command.CommandText = "SELECT Id, Password FROM Users" +
                              " WHERE Username = @Username";
        command.Parameters.AddWithValue("@Username", username);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new DataException("User does not exist!");
        }

        reader.Read();

        var userId = reader.GetInt32(0);
        var hashedPassword = reader.GetString(1);

        reader.Close();
        connection.Close();

        // Check if the password is correct.
        if (!IsValidPassword(password, hashedPassword))
            throw new InvalidCredentialException("Password is incorrect!");

        // If the user is a corporate user, return the corporate user.
        if (IsCorporateUser(userId))
            return GetCorporateUserByUserId(userId)!;

        return GetPrivateUserByUserId(userId)!;
    }

    /// <summary>
    ///     Checks if a given password matches a given hash.
    /// </summary>
    /// <param name="password">The plaintext password.</param>
    /// <param name="hash">The hashed password.</param>
    /// <returns>True if the password matches the hash, false otherwise.</returns>
    /// <exception cref="ArgumentException">Thrown when the password is null or empty.</exception>
    /// <exception cref="SaltParseException">Thrown when the salt is invalid.</exception>
    private static bool IsValidPassword(string password, string hash)
    {
        return BC.Verify(password, hash);
    }

    /// <summary>
    ///     Checks if a username is already taken.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>True if the username is taken, false otherwise.</returns>
    private static bool IsUsernameTaken(string username)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id FROM Users" +
                              " WHERE Username = @Username";
        command.Parameters.AddWithValue("@Username", username);

        var reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Close();
            connection.Close();

            return true;
        }

        reader.Close();
        connection.Close();

        return false;
    }

    /// <summary>
    ///     Checks if a user is a corporate user or not.
    /// </summary>
    /// <param name="userId">The ID of the user to check.</param>
    /// <returns>True if the user is a corporate user, false otherwise.</returns>
    public static bool IsCorporateUser(int userId)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id FROM CorporateUsers" +
                              " WHERE UserId = @UserId";
        command.Parameters.AddWithValue("@UserId", userId);

        var reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Close();
            connection.Close();

            return true;
        }

        reader.Close();
        connection.Close();

        return false;
    }
    #endregion

    #region Signup
    /// <summary>
    ///     Signs up a user.
    /// </summary>
    /// <param name="user">The user to sign up.</param>
    /// <returns>The signed up user.</returns>
    /// <exception cref="DataException">Thrown when the username is already taken or the password could not be hashed.</exception>
    public static User SignUp(User user)
    {
        // Check if the username is already taken.
        if (IsUsernameTaken(user.Username))
            throw new DataException("Username is already taken!");

        // Hash the password.
        var hashedPassword = HashPassword(user.Password);

        // Make sure the password was hashed successfully.
        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new DataException("Password could not be hashed!");

        // Create the user.
        var userToCreate = new User(0, user.Username, hashedPassword, user.Zipcode, user.Balance);
        var createdUser = CreateUser(userToCreate);

        return createdUser;
    }

    /// <summary>
    ///     Signs up a private user.
    /// </summary>
    /// <param name="privateUser">The private user to sign up.</param>
    /// <returns>The signed up private user.</returns>
    /// <exception cref="ArgumentException">Thrown when the username, password or CPR number is null or empty.</exception>
    /// <exception cref="DataException">Thrown when the username is already taken.</exception>
    public static PrivateUser SignUp(PrivateUser privateUser)
    {
        ValidateUser(privateUser);

        var user = SignUp(new User(0, privateUser.Username, privateUser.Password, privateUser.Zipcode, privateUser.Balance));

        // Create the private user.
        var privateUserToCreate = new PrivateUser(0, privateUser.Cpr, user);
        var createdPrivateUser = CreatePrivateUser(privateUserToCreate);

        return createdPrivateUser;
    }

    /// <summary>
    ///     Signs up a corporate user.
    /// </summary>
    /// <param name="corporateUser">The corporate user to sign up.</param>
    /// <returns>The signed up corporate user.</returns>
    /// <exception cref="ArgumentException">Thrown when the username, password or CVR number is null or empty.</exception>
    /// <exception cref="DataException">Thrown when the username is already taken.</exception>
    public static CorporateUser SignUp(CorporateUser corporateUser)
    {
        ValidateUser(corporateUser);

        // Create the user.
        var user = SignUp(new User(0, corporateUser.Username, corporateUser.Password, corporateUser.Zipcode, corporateUser.Balance));

        // Create the corporate user.
        var corporateUserToCreate = new CorporateUser(0, corporateUser.Cvr, corporateUser.Credit, user);
        var createdCorporateUser = CreateCorporateUser(corporateUserToCreate);

        return createdCorporateUser;
    }

    /// <summary>
    ///     Validates a user.
    /// </summary>
    /// <param name="user">The user to validate.</param>
    /// <exception cref="ArgumentException">Thrown when a value is detected as invalid.</exception>
    private static void ValidateUser(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username))
        {
            throw new ArgumentException("Username cannot be empty!");
        }

        if (string.IsNullOrWhiteSpace(user.Password))
        {
            throw new ArgumentException("Password cannot be empty!");
        }

        if (string.IsNullOrWhiteSpace(user.Zipcode))
        {
            throw new ArgumentException("Zipcode cannot be empty!");
        }

        switch (user)
        {
            // If the user is a private user, make sure the CPR number is not empty.
            case PrivateUser privateUser when string.IsNullOrWhiteSpace(privateUser.Cpr):
                throw new ArgumentException("CPR number cannot be empty!");
            // If the user is a corporate user, make sure the CVR number is not empty.
            case CorporateUser corporateUser when string.IsNullOrWhiteSpace(corporateUser.Cvr):
                throw new ArgumentException("CVR number cannot be empty!");
        }
    }

    /// <summary>
    ///     Hashes a password using BCrypt.
    /// </summary>
    /// <param name="password">The plaintext password.</param>
    /// <returns>The hashed password.</returns>
    /// <exception cref="ArgumentException">Thrown when the password is null or empty.</exception>
    /// <exception cref="SaltParseException">Thrown when the salt is invalid.</exception>
    private static string? HashPassword(string password)
    {
        var hash = BC.HashPassword(password, BC.GenerateSalt());

        return hash;
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
            var privateUserId = reader.GetInt32(0);
            var cpr = reader.GetString(1);
            var userId = reader.GetInt32(2);

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
    /// <returns>The created private user.</returns>
    /// <exception cref="ArgumentException">Thrown when the private user already exists.</exception>
    public static PrivateUser CreatePrivateUser(PrivateUser privateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO PrivateUsers (Cpr, UserId)" +
                              " OUTPUT inserted.Id" +
                              " VALUES (@Cpr, @UserId)";
        command.Parameters.AddWithValue("@Cpr", privateUser.Cpr);
        command.Parameters.AddWithValue("@UserId", privateUser.UserId);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Private user already exists!");
        }

        reader.Read();

        var privateUserId = reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return GetPrivateUserById(privateUserId);
    }

    /// <summary>
    ///     Gets a private user from the database by their ID.
    /// </summary>
    /// <param name="id">The ID of the private user to get.</param>
    /// <returns>The private user with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown when the private user does not exist.</exception>
    public static PrivateUser GetPrivateUserById(int id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM PrivateUsers" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Private user does not exist!");
        }

        reader.Read();

        var privateUserId = reader.GetInt32(0);
        var cpr = reader.GetString(1);
        var userId = reader.GetInt32(2);

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
        command.CommandText = "SELECT * FROM PrivateUsers" +
                              " WHERE Cpr = @Cpr";
        command.Parameters.AddWithValue("@Cpr", cpr);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Private user does not exist!");
        }

        reader.Read();

        var privateUserId = reader.GetInt32(0);
        var cprNumber = reader.GetString(1);
        var userId = reader.GetInt32(2);

        reader.Close();
        connection.Close();

        var user = GetUserById(userId);

        return new PrivateUser(privateUserId, cprNumber, user);
    }

    /// <summary>
    ///     Gets a private user from the database by their user ID.
    /// </summary>
    /// <param name="id">The user ID of the private user to get.</param>
    /// <returns>The private user with the given user ID.</returns>
    /// <exception cref="ArgumentException">Thrown when the private user does not exist.</exception>
    public static PrivateUser GetPrivateUserByUserId(int id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM PrivateUsers" +
                              " WHERE UserId = @UserId";
        command.Parameters.AddWithValue("@UserId", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Private user does not exist!");
        }

        reader.Read();

        var privateUserId = reader.GetInt32(0);
        var cprNumber = reader.GetString(1);
        var userId = reader.GetInt32(2);

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
        command.CommandText = "UPDATE PrivateUsers" +
                              " SET Cpr = @Cpr," +
                              "     UserId = @UserId" +
                              " WHERE Id = @Id";
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
        command.CommandText = "DELETE FROM PrivateUsers" +
                              " WHERE Id = @Id";
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
            var corporateUserId = reader.GetInt32(0);
            var cvrNumber = reader.GetString(1);
            var credit = reader.GetDecimal(2);
            var userId = reader.GetInt32(3);

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
    /// <returns>The created corporate user.</returns>
    /// <exception cref="ArgumentException">Thrown when the corporate user already exists.</exception>
    public static CorporateUser CreateCorporateUser(CorporateUser corporateUser)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO CorporateUsers (Cvr, Credit, UserId)" +
                              " OUTPUT inserted.Id" +
                              " VALUES (@Cvr, @Credit, @UserId)";
        command.Parameters.AddWithValue("@Cvr", corporateUser.Cvr);
        command.Parameters.AddWithValue("@Credit", corporateUser.Credit);
        command.Parameters.AddWithValue("@UserId", corporateUser.UserId);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Corporate user already exists!");
        }

        reader.Read();

        var corporateUserId = reader.GetInt32(0);

        reader.Close();
        connection.Close();

        return new CorporateUser(corporateUserId, corporateUser.Cvr, corporateUser.Credit, GetUserById(corporateUser.UserId));
    }

    /// <summary>
    ///     Gets a corporate user from the database by their ID.
    /// </summary>
    /// <param name="id">The ID of the corporate user to get.</param>
    /// <returns>The corporate user with the given ID.</returns>
    /// <exception cref="ArgumentException">Thrown when the corporate user does not exist.</exception>
    public static CorporateUser GetCorporateUserById(int id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM CorporateUsers" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Corporate user does not exist!");
        }

        reader.Read();

        var corporateUserId = reader.GetInt32(0);
        var cvrNumber = reader.GetString(1);
        var credit = reader.GetDecimal(2);
        var userId = reader.GetInt32(3);

        reader.Close();
        connection.Close();

        var user = GetUserById(userId);

        return new CorporateUser(corporateUserId, cvrNumber, credit, user);
    }

    public static CorporateUser GetCorporateUserByUserId(int id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM CorporateUsers" +
                              " WHERE UserId = @UserId";
        command.Parameters.AddWithValue("@UserId", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Corporate user does not exist!");
        }

        reader.Read();

        var corporateUserId = reader.GetInt32(0);
        var cvrNumber = reader.GetString(1);
        var credit = reader.GetDecimal(2);
        var userId = reader.GetInt32(3);

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
        command.CommandText = "SELECT * FROM CorporateUsers" +
                              " WHERE Cvr = @Cvr";
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
            var corporateUserId = reader.GetInt32(0);
            var cvrNumber = reader.GetString(1);
            var credit = reader.GetDecimal(2);
            var userId = reader.GetInt32(3);

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
        command.CommandText = "UPDATE CorporateUsers" +
                              " SET Cvr = @Cvr," +
                              "     Credit = @Credit," +
                              "     UserId = @UserId" +
                              " WHERE Id = @Id";
        command.Parameters.AddWithValue("@Cvr", corporateUser.Cvr);
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
        command.CommandText = "DELETE FROM CorporateUsers" +
                              " WHERE Id = @Id";
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
