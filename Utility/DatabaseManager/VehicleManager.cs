using System.Data;
using System.Data.SqlClient;
using Data.Classes.Vehicles;

namespace Utility.DatabaseManager;

public partial class DatabaseManager
{
    #region LicenseType
    /// <summary>
    ///     Get the license type from the database by id.
    /// </summary>
    /// <param name="id">The id of the license type.</param>
    /// <returns>The license type.</returns>
    /// <exception cref="DataException">If the license type is not found.</exception>
    public LicenseType GetLicenseTypeById(uint id)
    {
        var connection = Instance.GetConnection();

        var query = "SELECT * FROM LicenseTypes WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("License type not found!");
        }

        reader.Read();

        var licenseType = (LicenseType) reader.GetByte(0);

        reader.Close();

        return licenseType;
    }
    #endregion

    #region FuelType
    /// <summary>
    ///     Get the fuel type from the database by id.
    /// </summary>
    /// <param name="id">The id of the fuel type.</param>
    /// <returns>The fuel type.</returns>
    /// <exception cref="DataException">If the fuel type is not found.</exception>
    public FuelType GetFuelTypeById(uint id)
    {
        var connection = GetConnection();

        var query = "SELECT * FROM FuelTypes WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Fuel type not found!");
        }

        reader.Read();

        var fuelType = (FuelType) reader.GetByte(0);

        reader.Close();

        return fuelType;
    }
    #endregion

    #region EnergyType
    /// <summary>
    ///     Get the energy type from the database by id.
    /// </summary>
    /// <param name="id">The id of the energy type.</param>
    /// <returns>The energy type.</returns>
    /// <exception cref="DataException">If the energy type is not found.</exception>
    public EnergyType GetEnergyTypeById(uint id)
    {
        var connection = GetConnection();

        var query = "SELECT * FROM EnergyTypes WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Energy type not found!");
        }

        reader.Read();

        var energyType = (EnergyType) reader.GetByte(0);

        reader.Close();

        return energyType;
    }
    #endregion

    #region Dimensions
    /// <summary>
    ///     Get the dimensions from the database by id.
    /// </summary>
    /// <param name="id">The id of the dimensions.</param>
    /// <returns>The dimensions.</returns>
    /// <exception cref="DataException">If the dimensions is not found.</exception>
    public Dimensions GetDimensions(uint id)
    {
        var connection = GetConnection();

        var query = "SELECT * FROM Dimensions WHERE Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Dimensions not found!");
        }

        reader.Read();

        var dimensions = new Dimensions((uint) reader.GetInt32(1), reader.GetDouble(2), reader.GetDouble(3), reader.GetDouble(4));

        reader.Close();

        return dimensions;
    }
    #endregion

    #region Vehicle
    /// <summary>
    ///     Insert a new Vehicle into the database.
    /// </summary>
    /// <param name="vehicle">The Vehicle to insert.</param>
    /// <returns>The ID of the inserted vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not inserted.</exception>
    public uint CreateVehicle(Vehicle vehicle)
    {
        var connection = GetConnection();

        var query =
            "INSERT INTO Vehicles (Name, Km, RegistrationNumber, Year, NewPrice, HasTowbar, EngineSize, KmPerLiter, LicenseTypeId, FuelTypeId, EnergyTypeId)" +
            " OUTPUT INSERTED.Id AS VehicleId" +
            " VALUES (@Name, @Km, @RegistrationNumber, @Year, @NewPrice, @HasTowbar, @EngineSize, @KmPerLiter, @LicenseTypeId, @FuelTypeId, @EnergyTypeId)";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", vehicle.Name);
        command.Parameters.AddWithValue("@Km", vehicle.Km);
        command.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
        command.Parameters.AddWithValue("@Year", vehicle.Year);
        command.Parameters.AddWithValue("@NewPrice", vehicle.NewPrice);
        command.Parameters.AddWithValue("@HasTowbar", vehicle.HasTowbar);
        command.Parameters.AddWithValue("@EngineSize", vehicle.EngineSize);
        command.Parameters.AddWithValue("@KmPerLiter", vehicle.KmPerLiter);
        command.Parameters.AddWithValue("@LicenseTypeId", (byte) vehicle.DriversLicense);
        command.Parameters.AddWithValue("@FuelTypeId", (byte) vehicle.FuelType);
        command.Parameters.AddWithValue("@EnergyTypeId", (byte) vehicle.EnergyType);

        var reader = command.ExecuteReader();
        var vehicleId = reader.Read() ? (uint) reader.GetInt32(0) : throw new DataException("Vehicle not inserted!");

        reader.Close();

        return vehicleId;
    }

    /// <summary>
    ///     Get the vehicle from the database by ID.
    /// </summary>
    /// <param name="id">The ID of the vehicle.</param>
    /// <param name="vehicle">The vehicle.</param>
    /// <returns>The ID of the vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not found.</exception>
    public uint UpdateVehicle(uint id, Vehicle vehicle)
    {
        var connection = GetConnection();

        var query =
            "UPDATE Vehicles" +
            " SET Name = @Name, Km = @Km, RegistrationNumber = @RegistrationNumber, Year = @Year, NewPrice = @NewPrice, HasTowbar = @HasTowbar, EngineSize = @EngineSize, KmPerLiter = @KmPerLiter, LicenseTypeId = @LicenseTypeId, FuelTypeId = @FuelTypeId, EnergyTypeId = @EnergyTypeId" +
            " OUTPUT INSERTED.Id AS VehicleId" +
            " WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Name", vehicle.Name);
        command.Parameters.AddWithValue("@Km", vehicle.Km);
        command.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
        command.Parameters.AddWithValue("@Year", vehicle.Year);
        command.Parameters.AddWithValue("@NewPrice", vehicle.NewPrice);
        command.Parameters.AddWithValue("@HasTowbar", vehicle.HasTowbar);
        command.Parameters.AddWithValue("@EngineSize", vehicle.EngineSize);
        command.Parameters.AddWithValue("@KmPerLiter", vehicle.KmPerLiter);
        command.Parameters.AddWithValue("@LicenseTypeId", (byte) vehicle.DriversLicense);
        command.Parameters.AddWithValue("@FuelTypeId", (byte) vehicle.FuelType);
        command.Parameters.AddWithValue("@EnergyTypeId", (byte) vehicle.EnergyType);

        var reader = command.ExecuteReader();
        var vehicleId = reader.Read() ? (uint) reader.GetInt32(1) : throw new DataException("Vehicle not updated!");

        reader.Close();

        return vehicleId;
    }

    /// <summary>
    ///     Delete a vehicle from the database.
    /// </summary>
    /// <param name="id">The ID of the vehicle to delete.</param>
    /// <exception cref="DataException">If the vehicle is not deleted.</exception>
    public void DeleteVehicle(uint id)
    {
        var connection = GetConnection();

        var query =
            "DELETE FROM Vehicles" +
            " WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteNonQuery();

        if (reader == 0)
        {
            throw new DataException("Vehicle not deleted!");
        }
    }
    #endregion

    #region PersonalCar
    /// <summary>
    ///     Insert a new PersonalCar into the database.
    /// </summary>
    /// <param name="personalCar">The PersonalCar to insert.</param>
    /// <returns>The ID of the inserted personal car.</returns>
    /// <exception cref="DataException">If the personal car is not inserted.</exception>
    public (uint, uint) CreatePersonalCar(PersonalCar personalCar)
    {
        var connection = GetConnection();

        var vehicleId = CreateVehicle(personalCar);

        var query =
            "INSERT INTO PersonalCars (NumberOfSeats, TrunkDimensionsId, VehicleId)" +
            " OUTPUT INSERTED.Id AS PersonalCarId" +
            " VALUES (@NumberOfSeats, @TrunkDimensionsId, @VehicleId)";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@NumberOfSeats", personalCar.NumberOfSeats);
        command.Parameters.AddWithValue("@TrunkDimensionsId", personalCar.TrunkDimensionses.Id);
        command.Parameters.AddWithValue("@VehicleId", vehicleId);

        var reader = command.ExecuteReader();
        var personalCarId = reader.Read() ? (uint) reader.GetInt32(0) : throw new DataException("Personal car not inserted!");

        reader.Close();

        return personalCarId;
    }

    /// <summary>
    ///     Get the PersonalCar from the database by ID.
    /// </summary>
    /// <param name="id">The ID of the vehicle.</param>
    /// <param name="personalCar">The vehicle.</param>
    /// <returns>The ID of the vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not found.</exception>
    public uint UpdatePersonalCar(uint id, PersonalCar personalCar)
    {
        var connection = GetConnection();

        var vehicleId = UpdateVehicle(id, personalCar);

        var query =
            "UPDATE PersonalCars" +
            " SET NumberOfSeats = @NumberOfSeats, TrunkDimensionsId = @TrunkDimensionsId, VehicleId = @VehicleId" +
            " OUTPUT INSERTED.Id AS PersonalCarId" +
            " WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@NumberOfSeats", personalCar.NumberOfSeats);
        command.Parameters.AddWithValue("@TrunkDimensionsId", personalCar.TrunkDimensionses.DimensionsId);
        command.Parameters.AddWithValue("@VehicleId", vehicleId);

        var reader = command.ExecuteReader();
        var personalCarId = reader.Read() ? (uint) reader.GetInt32(1) : throw new DataException("Personal car not updated!");

        reader.Close();

        return personalCarId;
    }

    /// <summary>
    ///     Delete a PersonalCar from the database.
    /// </summary>
    /// <param name="id">The ID of the vehicle to delete.</param>
    /// <exception cref="DataException">If the vehicle is not deleted.</exception>
    public void DeletePersonalCar(uint id)
    {
        var connection = GetConnection();

        var query =
            "DELETE FROM PersonalCars" +
            " WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteNonQuery();
        if (reader == 0)
        {
            throw new DataException("Personal car not deleted!");
        }
    }
    #endregion

    #region ProfessionalPersonalCar
    /// <summary>
    ///     Insert a new ProfessionalPersonalCar into the database.
    /// </summary>
    /// <param name="professionalPersonalCar">The ProfessionalPersonalCar to insert.</param>
    /// <returns>The professional personal car ID of the inserted vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not inserted.</exception>
    public uint CreateProfessionalPersonalCar(ProfessionalPersonalCar professionalPersonalCar)
    {
        var connection = GetConnection();

        var personalCarId = CreatePersonalCar(professionalPersonalCar);
        var query =
            "INSERT INTO ProfessionalPersonalCars (HasSafetyBar, LoadCapacity, PersonalCarId)" +
            " OUTPUT INSERTED.Id AS ProfessionalPersonalCarId" +
            " VALUES (@HasSafetyBar, @LoadCapacity, @PersonalCarId)";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@HasSafetyBar", professionalPersonalCar.HasSafetyBar);
        command.Parameters.AddWithValue("@LoadCapacity", professionalPersonalCar.LoadCapacity);
        command.Parameters.AddWithValue("@PersonalCarId", personalCarId);

        var reader = command.ExecuteReader();
        var professionalPersonalCarId = reader.Read() ? (uint) reader.GetInt32(1) : throw new DataException("Professional personal car not inserted!");

        reader.Close();

        return professionalPersonalCarId;
    }

    /// <summary>
    ///     Get the ProfessionalPersonalCar from the database by ID.
    /// </summary>
    /// <param name="id">The ID of the vehicle.</param>
    /// <returns>The vehicle.</returns>
    /// <exception cref="DataException">If the vehicle is not found.</exception>
    public ProfessionalPersonalCar GetProfessionalPersonalCar(uint id)
    {
        var connection = GetConnection();

        var query =
            "SELECT PPC.Id, PPC.HasSafetyBar, PPC.LoadCapacity," +
            "       PC.Id, PC.NumberOfSeats," +
            "       D.Id, D.Length, D.Width, D.Height," +
            "       V.Id, V.Name, V.Km, V.RegistrationNumber, V.Year, V.NewPrice, V.EngineSize, V.KmPerLiter, V.LicenseTypeId, V.FuelTypeId, V.EnergyTypeId, " +
            "       LT.Id, FT.Id" +
            " FROM ProfessionalPersonalCars AS PPC" +
            "      JOIN PersonalCars PC on PPC.PersonalCarId = PC.Id" +
            "      JOIN Dimensions D on D.Id = PC.TrunkDimensionsId" +
            "      JOIN Vehicles V on PC.VehicleId = V.Id" +
            "      JOIN LicenseTypes LT on V.LicenseTypeId = LT.Id" +
            "      JOIN FuelTypes FT on V.FuelTypeId = FT.Id" +
            " WHERE PPC.Id = @Id";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            throw new DataException("Professional Personal Car not found!");
        }

        reader.Read();

        var professionalPersonalCarId = (uint) reader.GetInt32(0);
        var hasSafetyBar = reader.GetBoolean(1);
        var loadCapacity = reader.GetFloat(2);

        var personalCarId = (uint) reader.GetInt32(3);
        var numberOfSeats = reader.GetByte(4);

        var dimensions = new Dimensions((uint) reader.GetInt32(5), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8));

        var vehicleId = (uint) reader.GetInt32(9);
        var name = reader.GetString(10);
        var km = reader.GetDouble(11);
        var registrationNumber = reader.GetString(12);
        var year = (ushort) reader.GetSqlInt16(13).Value;
        var newPrice = reader.GetDecimal(14);
        var engineSize = reader.GetDouble(15);
        var kmPerLiter = reader.GetDouble(16);

        var licenseType = (LicenseType) reader.GetByte(17);
        var fuelType = (FuelType) reader.GetByte(18);

        return new ProfessionalPersonalCar(professionalPersonalCarId, personalCarId, vehicleId, name, km,
            registrationNumber, year, newPrice, engineSize, kmPerLiter, fuelType, numberOfSeats, dimensions,
            hasSafetyBar, loadCapacity, licenseType);
    }

    /// <summary>
    ///     Update a ProfessionalPersonalCar in the database.
    /// </summary>
    /// <param name="id">The ID of the ProfessionalPersonalCar to update.</param>
    /// <param name="professionalPersonalCar">The object containing the new data.</param>
    /// <returns>The ID of the updated vehicle.</returns>
    public uint UpdateProfessionalPersonalCar(uint id,
        ProfessionalPersonalCar professionalPersonalCar)
    {
        var connection = GetConnection();

        var personalCarId = UpdatePersonalCar(id, professionalPersonalCar);

        var query =
            "UPDATE ProfessionalPersonalCars" +
            " SET HasSafetyBar = @HasSafetyBar, LoadCapacity = @LoadCapacity, PersonalCarId = @PersonalCarId" +
            " OUTPUT INSERTED.Id AS ProfessionalPersonalCarId" +
            " WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@HasSafetyBar", professionalPersonalCar.HasSafetyBar);
        command.Parameters.AddWithValue("@LoadCapacity", professionalPersonalCar.LoadCapacity);
        command.Parameters.AddWithValue("@PersonalCarId", personalCarId);

        var reader = command.ExecuteReader();
        var professionalPersonalCarId = reader.Read() ? (uint) reader.GetInt32(1) : throw new DataException("Professional personal car not updated!");

        reader.Close();

        return professionalPersonalCarId;
    }

    // Delete
    /// <summary>
    ///     Delete a ProfessionalPersonalCar from the database.
    /// </summary>
    /// <param name="id">The ID of the vehicle to delete.</param>
    /// <exception cref="DataException">If the vehicle is not deleted.</exception>
    public void DeleteProfessionalPersonalCar(uint id)
    {
        var connection = GetConnection();

        var query =
            "DELETE FROM ProfessionalPersonalCars" +
            " WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        var reader = command.ExecuteNonQuery();
        if (reader == 0)
        {
            throw new DataException("Professional personal car not deleted!");
        }
    }
    #endregion

    #region PrivatePersonalCar
    // Create
    public PrivatePersonalCar CreatePrivatePersonalCar(PrivatePersonalCar privatePersonalCar)
    {
        var connection = GetConnection();

        var personalCarId = CreatePersonalCar(privatePersonalCar);

        var query =
            "INSERT INTO PrivatePersonalCars (HasIsofixFittings, PersonalCarId)" +
            " OUTPUT INSERTED.Id AS PrivatePersonalCarId" +
            " VALUES (@HasIsofixFittings, @PersonalCarId)";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@HasIsofixFittings", privatePersonalCar.HasIsofixFittings);
        command.Parameters.AddWithValue("@PersonalCarId", personalCarId);

        var reader = command.ExecuteReader();
        var privatePersonalCarId = reader.Read() ? (uint) reader.GetInt32(1) : throw new DataException("Private personal car not inserted!");

        reader.Close();

        return new PrivatePersonalCar(privatePersonalCarId, personalCarId, vehicleId);
    }
}