using Data.Classes;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;

namespace Utility.DatabaseManager;

/// <summary>
///     The part of the database manager that handles auctions.
/// </summary>
public partial class DatabaseManager
{
    #region Auctions
    /// <summary>
    ///     Gets all auctions from the database.
    /// </summary>
    /// <returns>A list of auctions.</returns>
    /// <exception cref="ArgumentException">Thrown when no auctions exist.</exception>
    public static List<Auction> GetAllAuctions()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Auctions";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No users exist!");
        }

        var auctions = new List<Auction>();

        while (reader.Read())
        {
            var auctionId = (uint)reader.GetInt32(0);
            var minimumPrice = reader.GetDecimal(1);
            var standingBid = reader.GetDecimal(2);

            var vehicleId = (uint)reader.GetInt32(3);
            var vehicle = GetVehicleById(vehicleId);

            var sellerId = (uint)reader.GetInt32(4);
            var seller = GetUserById(sellerId);

            if (reader.IsDBNull(5))
            {
                auctions.Add(new Auction(auctionId, vehicle, seller, null, minimumPrice, standingBid));

                continue;
            }

            var buyerId = (uint)reader.GetInt32(5);
            var buyer = GetUserById(buyerId);

            auctions.Add(new Auction(auctionId, vehicle, seller, buyer, minimumPrice, standingBid));
        }

        reader.Close();

        return auctions;
    }

    /// <summary>
    ///     Gets all auctions by a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A list of auctions.</returns>
    /// <exception cref="ArgumentException">Thrown when the user does not exist.</exception>
    public static List<Auction> GetAuctionsByUser(User user)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Auctions WHERE SellerId = @Id OR BuyerId = @Id";
        command.Parameters.AddWithValue("@Id", user.UserId);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        var auctions = new List<Auction>();

        while (reader.Read())
        {
            var auctionId = (uint)reader.GetInt32(0);
            var minimumPrice = reader.GetDecimal(1);
            var standingBid = reader.GetDecimal(2);

            var vehicleId = (uint)reader.GetInt32(3);
            var vehicle = GetVehicleById(vehicleId);

            var sellerId = (uint)reader.GetInt32(4);
            var seller = GetUserById(sellerId);

            if (reader.IsDBNull(5))
            {
                auctions.Add(new Auction(auctionId, vehicle, seller, null, minimumPrice, standingBid));

                continue;
            }

            var buyerId = (uint)reader.GetInt32(5);
            var buyer = GetUserById(buyerId);

            auctions.Add(new Auction(auctionId, vehicle, seller, buyer, minimumPrice, standingBid));
        }

        reader.Close();

        return auctions;
    }

    /// <summary>
    ///     Creates an auction.
    /// </summary>
    /// <param name="auction">The auction.</param>
    /// <exception cref="ArgumentException">Thrown when the auction could not be created.</exception>
    public static void CreateAuction(Auction auction)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Auctions (MinimumPrice, StandingBid, VehicleId, SellerId, BuyerId) VALUES (@MinimumPrice, @StandingBid, @VehicleId, @SellerId, @BuyerId)";
        command.Parameters.AddWithValue("@MinimumPrice", auction.MinimumPrice);
        command.Parameters.AddWithValue("@StandingBid", auction.StandingBid);
        command.Parameters.AddWithValue("@VehicleId", auction.Vehicle.VehicleId);
        command.Parameters.AddWithValue("@SellerId", auction.Seller.UserId);
        command.Parameters.AddWithValue("@BuyerId", auction.Buyer?.UserId ?? 0);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Failed to create auction!");
        }

        connection.Close();
    }

    /// <summary>
    ///     Gets an auction by its ID.
    /// </summary>
    /// <param name="id">The ID of the auction.</param>
    /// <returns>The auction.</returns>
    /// <exception cref="ArgumentException">Thrown when the auction does not exist.</exception>
    public static Auction GetAuctionById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Auctions WHERE AuctionId = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Auction does not exist!");
        }

        reader.Read();

        var auctionId = (uint)reader.GetInt32(0);
        var minimumPrice = reader.GetDecimal(1);
        var standingBid = reader.GetDecimal(2);

        var vehicleId = (uint)reader.GetInt32(3);
        var vehicle = GetVehicleById(vehicleId);

        var sellerId = (uint)reader.GetInt32(4);
        var seller = GetUserById(sellerId);

        if (reader.IsDBNull(5))
        {
            reader.Close();
            connection.Close();

            return new Auction(auctionId, vehicle, seller, null, minimumPrice, standingBid);
        }

        var buyerId = (uint)reader.GetInt32(5);
        var buyer = GetUserById(buyerId);

        reader.Close();

        return new Auction(auctionId, vehicle, seller, buyer, minimumPrice, standingBid);
    }

    /// <summary>
    ///     Gets the auction by a vehicle.
    /// </summary>
    /// <param name="vehicle">The vehicle.</param>
    /// <returns>The auction.</returns>
    /// <exception cref="ArgumentException">Thrown when the vehicle does not exist.</exception>
    public static Auction GetAuctionByVehicle(Vehicle vehicle)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Auctions WHERE VehicleId = @Id";
        command.Parameters.AddWithValue("@Id", vehicle.VehicleId);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Vehicle does not exist!");
        }

        reader.Read();

        var auctionId = (uint)reader.GetInt32(0);
        var minimumPrice = reader.GetDecimal(1);
        var standingBid = reader.GetDecimal(2);

        var sellerId = (uint)reader.GetInt32(4);
        var seller = GetUserById(sellerId);

        if (reader.IsDBNull(5))
        {
            reader.Close();
            connection.Close();

            return new Auction(auctionId, vehicle, seller, null, minimumPrice, standingBid);
        }

        var buyerId = (uint)reader.GetInt32(5);
        var buyer = GetUserById(buyerId);

        reader.Close();

        return new Auction(auctionId, vehicle, seller, buyer, minimumPrice, standingBid);
    }

    /// <summary>
    ///     Updates an auction.
    /// </summary>
    /// <param name="auction">The auction.</param>
    /// <returns>The updated auction.</returns>
    /// <exception cref="ArgumentException">Thrown when the auction does not exist.</exception>
    public static Auction UpdateAuction(Auction auction)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Auctions SET MinimumPrice = @MinimumPrice, StandingBid = @StandingBid, VehicleId = @VehicleId, SellerId = @SellerId, BuyerId = @BuyerId WHERE AuctionId = @Id";
        command.Parameters.AddWithValue("@MinimumPrice", auction.MinimumPrice);
        command.Parameters.AddWithValue("@StandingBid", auction.StandingBid);
        command.Parameters.AddWithValue("@VehicleId", auction.Vehicle.VehicleId);
        command.Parameters.AddWithValue("@SellerId", auction.Seller.UserId);
        command.Parameters.AddWithValue("@BuyerId", auction.Buyer?.UserId ?? 0);
        command.Parameters.AddWithValue("@Id", auction.AuctionId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();
            throw new ArgumentException("Failed to update auction!");
        }

        connection.Close();

        return auction;
    }

    /// <summary>
    ///     Deletes an auction.
    /// </summary>
    /// <param name="auction">The auction.</param>
    /// <exception cref="ArgumentException">Thrown when the auction does not exist.</exception>
    public static void DeleteAuction(Auction auction)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Auctions WHERE AuctionId = @Id";
        command.Parameters.AddWithValue("@Id", auction.AuctionId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();
            throw new ArgumentException("Failed to delete auction!");
        }

        connection.Close();
    }
    #endregion

    #region Bids
    /// <summary>
    ///     Gets all bids from the database.
    /// </summary>
    /// <returns>A list of bids.</returns>
    /// <exception cref="ArgumentException">Thrown when no bids exist.</exception>
    public static List<Bid> GetAllBids()
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Bids";

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No bids exist!");
        }

        var bids = new List<Bid>();

        while (reader.Read())
        {
            var bidId = (uint)reader.GetInt32(0);
            var time = reader.GetDateTime(1);
            var amount = reader.GetDecimal(2);

            var bidderId = (uint)reader.GetInt32(3);
            var bidder = GetUserById(bidderId);

            var auctionId = (uint)reader.GetInt32(4);
            var auction = GetAuctionById(auctionId);

            bids.Add(new Bid(bidId, time, amount, bidder, auction));
        }

        reader.Close();

        return bids;
    }

    /// <summary>
    ///     Gets all bids by a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A list of bids.</returns>
    /// <exception cref="ArgumentException">Thrown when the user does not exist.</exception>
    public static List<Bid> GetBidsByUser(User user)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Bids WHERE BidderId = @Id";
        command.Parameters.AddWithValue("@Id", user.UserId);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("User does not exist!");
        }

        var bids = new List<Bid>();

        while (reader.Read())
        {
            var bidId = (uint)reader.GetInt32(0);
            var time = reader.GetDateTime(1);
            var amount = reader.GetDecimal(2);

            var bidderId = (uint)reader.GetInt32(3);
            var bidder = GetUserById(bidderId);

            var auctionId = (uint)reader.GetInt32(4);
            var auction = GetAuctionById(auctionId);

            bids.Add(new Bid(bidId, time, amount, bidder, auction));
        }

        reader.Close();

        return bids;
    }

    /// <summary>
    ///     Gets all bids on an auction.
    /// </summary>
    /// <param name="auction">The auction.</param>
    /// <returns>A list of bids.</returns>
    /// <exception cref="ArgumentException">Thrown when the auction does not exist.</exception>
    public static List<Bid> GetBidsByAuction(Auction auction)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Bids WHERE AuctionId = @Id";
        command.Parameters.AddWithValue("@Id", auction.AuctionId);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Auction does not exist!");
        }

        var bids = new List<Bid>();

        while (reader.Read())
        {
            var bidId = (uint)reader.GetInt32(0);
            var time = reader.GetDateTime(1);
            var amount = reader.GetDecimal(2);

            var bidderId = (uint)reader.GetInt32(3);
            var bidder = GetUserById(bidderId);

            var auctionId = (uint)reader.GetInt32(4);
            auction = GetAuctionById(auctionId);

            bids.Add(new Bid(bidId, time, amount, bidder, auction));
        }

        reader.Close();

        return bids;
    }

    /// <summary>
    ///     Creates a bid.
    /// </summary>
    /// <param name="bid">The bid.</param>
    /// <exception cref="ArgumentException">Thrown when the bid could not be created.</exception>
    public static void CreateBid(Bid bid)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Bids (Time, Amount, BidderId, AuctionId) VALUES (@Time, @Amount, @BidderId, @AuctionId)";
        command.Parameters.AddWithValue("@Time", bid.Time);
        command.Parameters.AddWithValue("@Amount", bid.Amount);
        command.Parameters.AddWithValue("@BidderId", bid.Bidder.UserId);
        command.Parameters.AddWithValue("@AuctionId", bid.Auction.AuctionId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();

            throw new ArgumentException("Failed to create bid!");
        }

        connection.Close();
    }

    /// <summary>
    ///     Gets a bid by its ID.
    /// </summary>
    /// <param name="id">The ID of the bid.</param>
    /// <returns>The bid.</returns>
    /// <exception cref="ArgumentException">Thrown when the bid does not exist.</exception>
    public static Bid GetBidById(uint id)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Bids WHERE BidId = @Id";
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("Bid does not exist!");
        }

        reader.Read();

        var bidId = (uint)reader.GetInt32(0);
        var time = reader.GetDateTime(1);
        var amount = reader.GetDecimal(2);

        var bidderId = (uint)reader.GetInt32(3);
        var bidder = GetUserById(bidderId);

        var auctionId = (uint)reader.GetInt32(4);
        var auction = GetAuctionById(auctionId);

        reader.Close();

        return new Bid(bidId, time, amount, bidder, auction);
    }

    /// <summary>
    ///     Updates a bid.
    /// </summary>
    /// <param name="bid">The bid.</param>
    /// <returns>The updated bid.</returns>
    /// <exception cref="ArgumentException">Thrown when the bid does not exist.</exception>
    public static Bid UpdateBid(Bid bid)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Bids SET Time = @Time, Amount = @Amount, BidderId = @BidderId, AuctionId = @AuctionId WHERE BidId = @Id";
        command.Parameters.AddWithValue("@Time", bid.Time);
        command.Parameters.AddWithValue("@Amount", bid.Amount);
        command.Parameters.AddWithValue("@BidderId", bid.Bidder.UserId);
        command.Parameters.AddWithValue("@AuctionId", bid.Auction.AuctionId);
        command.Parameters.AddWithValue("@Id", bid.BidId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();
            throw new ArgumentException("Failed to update bid!");
        }

        connection.Close();

        return bid;
    }

    /// <summary>
    ///     Deletes a bid.
    /// </summary>
    /// <param name="bid">The bid.</param>
    /// <exception cref="ArgumentException">Thrown when the bid does not exist.</exception>
    public static void DeleteBid(Bid bid)
    {
        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Bids WHERE BidId = @Id";
        command.Parameters.AddWithValue("@Id", bid.BidId);

        if (command.ExecuteNonQuery() == 0)
        {
            connection.Close();
            throw new ArgumentException("Failed to delete bid!");
        }

        connection.Close();
    }
    #endregion

    #region Seller
    /// <summary>
    ///     Gets all sellers within a given zipcode range from the database.
    /// </summary>
    /// <param name="from">The lower bound of the zipcode range.</param>
    /// <param name="to">The upper bound of the zipcode range.</param>
    /// <returns>A list of sellers within the given zipcode range.</returns>
    /// <exception cref="ArgumentException">Thrown when no sellers exist or when the lower bound is greater than the upper bound.</exception>
    public static List<User> GetSellersByZipcodeRange(uint from, uint to)
    {
        if (from > to)
        {
            throw new ArgumentException("'from' value cannot be greater than 'to' value!");
        }

        var connection = Instance.GetConnection();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT DISTINCT Id FROM Users U" +
                              "    INNER JOIN Auctions A ON U.Id = A.SellerId" +
                              "    WHERE Zipcode BETWEEN @From AND @To";
        command.Parameters.AddWithValue("@From", from);
        command.Parameters.AddWithValue("@To", to);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();

            throw new ArgumentException("No sellers within the given zipcode range exist!");
        }

        var sellers = new List<User>();

        while (reader.Read())
        {
            var userId = (uint)reader.GetInt32(0);
            var user = GetUserById(userId);

            sellers.Add(user);
        }

        reader.Close();
        connection.Close();

        return sellers;
    }
    #endregion
}
