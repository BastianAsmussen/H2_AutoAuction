using System.Security.Cryptography;
using System.Text;

namespace H2_AutoAuction.Classes;
/*
 * Domæne model
interface polymorfi via interface
interface til at kunne køde og sælge til

køber og sælger som interfaces

privat og company som klasser
 */

public abstract class User //TODO: U4 - Implement interfaces
{
    protected User(string userName, string password, uint zipCode)
    {
        //TODO: U1 - Set constructor and field

        HashAlgorithm sha = SHA256.Create();
        var result = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
        PasswordHash = result;

        throw new NotImplementedException();
    }

    /// <summary>
    ///     ID proberty
    /// </summary>
    public uint ID { get; }

    /// <summary>
    ///     PasswordHash proberty
    /// </summary>
    private byte[] PasswordHash { get; }

    /// <summary>
    ///     A method that ...
    /// </summary>
    /// <returns>Whether login is valid</returns>
    private bool ValidateLogin(string loginUserName, string loginPassword)
    {
        //TODO: U5 - Implement the rest of validation for password and user name

        HashAlgorithm sha = SHA256.Create(); //Make a HashAlgorithm object for makeing hash computations.
        var result =
            sha.ComputeHash(Encoding.ASCII.GetBytes(loginPassword)); //Encodes the password into a hash in a Byte array.

        return PasswordHash == result;

        throw new NotImplementedException();
    }

    //TODO: U4 - Implement interface proberties and methods.

    /// <summary>
    ///     Returns the User in a string with relivant information.
    /// </summary>
    /// <returns>...</returns>
    public override string ToString()
    {
        //TODO: U3 - ToString for User
        throw new NotImplementedException();
    }
}