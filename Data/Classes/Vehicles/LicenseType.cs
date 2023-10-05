namespace Data.Classes.Vehicles;

/// <summary>
///     Enum for the different types of drivers licenses.
///     Learn more: https://en.wikipedia.org/wiki/Driving_licence_in_Denmark
/// </summary>
public enum LicenseType
{
    /// <summary>
    ///     The license type A, which is for motorcycles.
    /// </summary>
    A = 1,

    /// <summary>
    ///     The license type B, which is for cars.
    /// </summary>
    B = 2,

    /// <summary>
    ///     The license type C, which is for trucks.
    /// </summary>
    C = 3,

    /// <summary>
    ///     The license type D, which is for buses.
    /// </summary>
    D = 4,

    /// <summary>
    ///     The license type BE, which is for cars with trailers.
    /// </summary>
    BE = 5,

    /// <summary>
    ///     The license type CE, which is for trucks with trailers.
    /// </summary>
    CE = 6,

    /// <summary>
    ///     The license type DE, which is for buses with trailers.
    /// </summary>
    DE = 7
}