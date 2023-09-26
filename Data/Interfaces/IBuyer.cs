namespace Data.Interfaces;

public interface IBuyer
{
    public uint UserId { get; set; }

    public void SubBalance(decimal amount);
}
