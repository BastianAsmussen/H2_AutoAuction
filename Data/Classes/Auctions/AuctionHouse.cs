﻿using Data.Classes.Vehicles;
using Data.Interfaces;

namespace Data.Classes.Auctions;

public static class AuctionHouse
{
    public static List<Auction> Auctions = new();

    /// <summary>
    ///     A method that ...
    /// </summary>
    /// <param name="vehicle"></param>
    /// <param name="seller"></param>
    /// <param name="minimumBid"></param>
    /// <returns> Auction ID </returns>
    public static uint SetForSale(Vehicle vehicle, ISeller seller, decimal minimumBid)
    {
        //TODO: A3 - SetForSale
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Receives a bid from a buyer.
    ///     Checks if the bid is eligible, by ...
    /// </summary>
    /// <param name="buyer">identification for the potential buyer placing a bid.</param>
    /// <param name="auctionId">Used to find the auction.</param>
    /// <param name="bid">The bid in decimal with the value ending in M for money.</param>
    /// <returns> A bool that indicates whether a bid was received or rejected. </returns>
    public static bool ReceiveBid(IBuyer buyer, uint auctionId, decimal bid)
    {
        //TODO: A5 - ReceiveBid
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Accepts a bid and ...
    /// </summary>
    /// <param name="seller"></param>
    /// <param name="auctionId"></param>
    /// <returns></returns>
    public static bool AcceptBid(ISeller seller, uint auctionId)
    {
        //TODO: A6 - AcceptBid
        throw new NotImplementedException();
    }

    #region Search Methods

    /// <summary>
    ///     Find an auction in the auction list from the id using a binary search.
    /// </summary>
    /// <param name="auctionId"></param>
    /// <returns> The Auction with the specific id or null if not found.</returns>
    public static async Task<Auction> FindAuctionById(uint auctionId)
    {
        //TODO: A7 - FindAuctionByID
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Finds vehicles by the name or part of the name.
    ///     Ekstra Opgave: 1. Find køretøjer hvis navn indeholder en angivet søgestreng
    /// </summary>
    /// <param name="searchWord"></param>
    /// <returns> A list of vehicles that contains the search word </returns>
    public static async Task<List<Vehicle>> FindVehiclesByName(string searchWord)
    {
        //TODO: AS1 - FindVehiclesByName
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Finds vehicles based on the minimum number of seats and whether it has toilet facilities.
    ///     Ekstra Opgave: 2. Find køretøjer der har et minimum angivet antal siddepladser samt toiletfaciliteter.
    /// </summary>
    /// <param name="seats"></param>
    /// <param name="hasToilet"></param>
    /// <returns> A list of vehicles that contains the vehicles </returns>
    public static async Task<List<Vehicle>> FindVehiclesByNumberOfSeats(int seats, bool hasToilet)
    {
        //TODO: AS2 - FindVehiclesByNumberOfSeats
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Ekstra Opgave: 3. Find køretøjer der kræver stort kørekort (kategori C, D, CE eller DE) og vejer under en angivet
    ///     maksimalvægt
    /// </summary>
    /// <param name="maxWeight"></param>
    /// <returns> A list of vehicles that contains the vehicles </returns>
    public static async Task<List<Vehicle>> FindVehiclesByDriversLicense(double maxWeight)
    {
        //TODO: AS3 - FindVehiclesByDriversLicense
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Ekstra Opgave: 4. Find alle personbiler til privatbrug som har kørt under et angivet antal km, og hvor minimum
    ///     salgsprisen
    ///     samtidig ligger under et angivet beløb. Køretøjerne skal returneres i sorteret rækkefølge efter antal kørte km.
    /// </summary>
    /// <param name="maxKm"></param>
    /// <param name="maxPrice"></param>
    /// <returns> A list of vehicles that contains the vehicles under maxKm and maxPrince </returns>
    public static async Task<List<Vehicle>> FindVehiclesByKmAndPrice(double maxKm, decimal maxPrice)
    {
        //TODO: AS4 - FindVehiclesByKmAndPrice
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Ekstra Opgave: 5. Find alle køretøjer hvor køretøjets sælger er bosiddende inden for en bestemt radius af et
    ///     angivet
    ///     postnummer. I denne forbindelse kan radius blot anskues som et tal der skal lægges til/trækkes fra
    ///     postnummeret.F.eks.vil en søgning efter køretøjer inden for en radius af 1500 fra postnummer 8000,
    ///     inkludere alle køretøjer hvor sælgers postnummer ligger mellem 6500 og 9500.
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="range"></param>
    /// <returns> A list of sellers that contains the sellers in range of the zipcode </returns>
    public static async Task<List<ISeller>> FindSellersByZipcodeRange(uint zipCode, uint range)
    {
        //TODO: AS5 - FindSellersByZipcodeRange
        throw new NotImplementedException();
    }

    #endregion
}