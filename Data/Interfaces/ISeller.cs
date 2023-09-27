﻿using Data.Classes;
using Data.Classes.Vehicles;

namespace Data.Interfaces;

public interface ISeller
{
    public int UserId { get; set; }

    /// <summary>
    ///     Receives a message for the user.
    /// </summary>
    /// <param name="message"></param>
    /// <returns>The message </returns>
    string ReceiveBidNotification(string message);

    int SetForSale(Vehicle vehicle, DateTime startDate, DateTime endDate, User seller, decimal minBid);
}
