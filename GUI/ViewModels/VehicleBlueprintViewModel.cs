using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Collections;
using Data.Classes.Vehicles;
using ReactiveUI;

namespace GUI.ViewModels;

public class VehicleBlueprintViewModel : ViewModelBase
{
    private VehicleTypes _vehicleType;
    private object? _selectedVehicleType = null;
    private string? _height;
    private string? _width;
    private string? _weight;
    private string? _engineSize;
    private bool _hasTowBar = false;

    public List<VehicleTypes> EnumValues { get; } =
        new List<VehicleTypes>(Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>());

    #region Properties

    public VehicleTypes VehicleType
    {
        get => _vehicleType;
        set => this.RaiseAndSetIfChanged(ref _vehicleType, value);
    }

    public object? SelectedVehicleType
    {
        get => _selectedVehicleType;
        set => this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
    }

    public string? Height
    {
        get => _height;
        set => this.RaiseAndSetIfChanged(ref _height, value);
    }

    public string? Width
    {
        get => _width;
        set => this.RaiseAndSetIfChanged(ref _width, value);
    }

    public string? Weight
    {
        get => _weight;
        set => this.RaiseAndSetIfChanged(ref _weight, value);
    }

    public string? EngineSize
    {
        get => _engineSize;
        set => this.RaiseAndSetIfChanged(ref _engineSize, value);
    }

    public bool HasTowBar
    {
        get => _hasTowBar;
        set => this.RaiseAndSetIfChanged(ref _hasTowBar, value);
    }

    #endregion

    public ICommand YesCommand { get; }
    public ICommand NoCommand { get; }

    public VehicleBlueprintViewModel()
    {
        YesCommand = ReactiveCommand.Create(DoesHaveTowBar);
        NoCommand = ReactiveCommand.Create(DoesNotHaveTowBar);
    }

    #region Methods

    private void DoesHaveTowBar() => HasTowBar = true;
    private void DoesNotHaveTowBar() => HasTowBar = false;

    #endregion
}