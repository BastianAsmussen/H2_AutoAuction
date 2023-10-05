namespace Data.Classes.Vehicles;

/// <summary>
///     Enum for the different types of energy classes.
///     Learn more: https://en.wikipedia.org/wiki/European_Union_energy_label
/// </summary>
public enum EnergyType
{
    /// <summary>
    ///     The energy class A, which is the most efficient.
    /// </summary>
    A = 1,

    /// <summary>
    ///     The energy class B, which is less efficient than A.
    /// </summary>
    B = 2,

    /// <summary>
    ///     The energy class C, which is less efficient than B.
    /// </summary>
    C = 3,

    /// <summary>
    ///     The energy class D, which is the least efficient.
    /// </summary>
    D = 4
}