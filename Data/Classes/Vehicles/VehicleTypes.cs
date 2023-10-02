using System.ComponentModel;
using Data.Classes.Vehicles.PersonalCars;

namespace Data.Classes.Vehicles;

public enum VehicleTypes
{
    [Description("Private Personal Car")] 
    PrivatePersonalCar,

    [Description("Professional Personal Car")]
    ProfessionalPersonalCar,

    [Description("Long Vehicle with plenty of places to sit")]
    Bus,

    [Description("Fat vehicle")] 
    Truck
}