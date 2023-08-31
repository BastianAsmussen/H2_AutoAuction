namespace H2_AutoAuction.Interfaces;

public interface IBuyer
{
    /// <summary>
    ///     UserName proberty
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     Balance proberty
    /// </summary>
    decimal Balance { get; set; }
}