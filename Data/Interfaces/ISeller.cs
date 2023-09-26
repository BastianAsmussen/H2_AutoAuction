namespace Data.Interfaces;

public interface ISeller
{
    public uint UserId { get; set; }

    /// <summary>
    ///     Receives a message for the user.
    /// </summary>
    /// <param name="message"></param>
    /// <returns>The message </returns>
    string ReceiveBidNotification(string message);
}
