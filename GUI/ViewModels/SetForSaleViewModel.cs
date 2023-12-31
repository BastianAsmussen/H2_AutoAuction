﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class SetForSaleViewModel : ViewModelBase
{
    private DateTime _endDate;
    private EnergyType _energyType;
    private FuelType _fuelType;
    private LicenseType _licenseTypes;
    private double _mileage;
    private string _name = null!;
    private string _regNumber;
    private LicenseType _selectedLicenseType;
    private DateTime _startDate;
    private decimal _startingBid;
    private UserControl _vehicleBlueprintControl = null!;
    private VehicleBlueprintViewModel _vehiclebpVm = new();

    public SetForSaleViewModel()
    {
        CreateSaleCommand = ReactiveCommand.Create(CreateSale);
        CancelCommand = ReactiveCommand.Create(Cancel);

        InitVehicleBlueprint();
        InitDateTimes();
    }

    public ICommand CreateSaleCommand { get; }
    public ICommand CancelCommand { get; }

    #region Properties

    public List<LicenseType> LicenseTypes { get; } = Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().ToList();

    public LicenseType SelectedLicenseType
    {
        get => _selectedLicenseType;
        set => this.RaiseAndSetIfChanged(ref _selectedLicenseType, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public double Mileage
    {
        get => _mileage;
        set => this.RaiseAndSetIfChanged(ref _mileage, value);
    }

    public string RegNumber
    {
        get => _regNumber;
        set => this.RaiseAndSetIfChanged(ref _regNumber, value.ToUpper());
    }

    public decimal StartingBid
    {
        get => _startingBid;
        set => this.RaiseAndSetIfChanged(ref _startingBid, value);
    }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value < Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")))
                this.RaiseAndSetIfChanged(ref _startDate, Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")));

            this.RaiseAndSetIfChanged(ref _startDate, value);
        }
    }

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (value < StartDate)
                this.RaiseAndSetIfChanged(ref _startDate,
                    Convert.ToDateTime(DateTime.Now.AddDays(+1).ToString("dd-MM-yyyy")));

            this.RaiseAndSetIfChanged(ref _endDate, value);
        }
    }

    public UserControl VehicleBlueprintControl
    {
        get => _vehicleBlueprintControl;
        set => this.RaiseAndSetIfChanged(ref _vehicleBlueprintControl, value);
    }

    #endregion

    #region Methods

    private void Cancel()
    {
        ContentArea.Navigate(new HomeScreenView());
    }

    private void InitVehicleBlueprint()
    {
        _vehiclebpVm = new VehicleBlueprintViewModel();
        var blueprintView = new VehicleBlueprintView
        {
            DataContext = _vehiclebpVm
        };

        VehicleBlueprintControl = blueprintView;
    }

    private void InitDateTimes()
    {
        StartDate = Convert.ToDateTime(DateTime.Now.ToString("hh:mm:ss"));
        EndDate = Convert.ToDateTime(DateTime.Now.AddDays(+1).ToString("hh:mm:ss"));
    }

    private void CreateSale()
    {
        try
        {
            _vehiclebpVm.SetRegistrationNumber(string.Concat(RegNumber.Where(c => !char.IsWhiteSpace(c))).Remove(2, 1));
            _vehiclebpVm.SetLicenseType(SelectedLicenseType);
            _vehiclebpVm.SetMileage(Mileage);
            _vehiclebpVm.SetVehicleName(Name);

            var auction = new Auction(0, StartingBid, StartingBid, StartDate, EndDate,
                _vehiclebpVm.GetVehicleBlueprint(),
                UserInstance.GetCurrentUser(),
                null);

            DatabaseManager.CreateAuction(auction);
            Console.WriteLine("Sale created");
            ContentArea.Navigate(new HomeScreenView());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error : {e.Message}");
        }
    }

    #endregion
}