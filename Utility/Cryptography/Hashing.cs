using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace Utility.Cryptography;

public class Hashing
{
    /// <summary>
    ///     Hashes a password using BCrypt.
    /// </summary>
    /// <param name="password">The plaintext password.</param>
    /// <returns>The hashed password.</returns>
    /// <exception cref="ArgumentException">Thrown when the password is null or empty.</exception>
    /// <exception cref="SaltParseException">Thrown when the salt is invalid.</exception>
    public static string? HashPassword(string password)
    {
        var hash = BC.HashPassword(password, BC.GenerateSalt());

        return hash;
    }

    /// <summary>
    ///     Checks if a given password matches a given hash.
    /// </summary>
    /// <param name="password">The plaintext password.</param>
    /// <param name="hash">The hashed password.</param>
    /// <returns>True if the password matches the hash, false otherwise.</returns>
    public static bool IsValidPassword(string password, string hash)
    {
        return BC.Verify(password, hash);
    }
}
