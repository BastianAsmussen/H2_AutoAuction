using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Windows.Input;
using Avalonia.Data;
using Data.Classes;
using Data.DatabaseManager;
using GUI.Views.UserControls;
using ReactiveUI;
using static GUI.Utilities.ContentArea;

namespace GUI.ViewModels;

public class CreateUserViewModel : ViewModelBase
{
    private string _userName;
    private string _passWord;
    private string _repeatPassword;
    private bool _isCorporate;
    private string _cvrNumber;
    private bool _isPrivate;
    private string _zipCode;
    private string _cprNumber;
    private bool _btnCreateEnabled;

    #region Properties

    [MinLength(1, ErrorMessage = "Username is required")]
    public string UserName
    {
        get => _userName;
        set => this.RaiseAndSetIfChanged(ref _userName, value.Trim());
    }

    [MinLength(1, ErrorMessage = "Password is required")]
    public string PassWord
    {
        get => _passWord;
        set => this.RaiseAndSetIfChanged(ref _passWord, value.Trim());
    }

    public string RepeatPassword
    {
        get => _repeatPassword;
        set
        {
            if (!PassWord.Equals(value))
            {
                //Rider 
                throw new DataValidationException("Password does not match");
            }

            this.RaiseAndSetIfChanged(ref _repeatPassword, value);
        }
    }

    public string CvrNumber
    {
        get => _cvrNumber;
        set
        {
            if (!string.IsNullOrEmpty(value))
                if (!value.All(char.IsDigit))
                    throw new DataValidationException("Numbers only");

            this.RaiseAndSetIfChanged(ref _cvrNumber, value);
        }
    }

    public string ZipCode
    {
        get => _zipCode;
        set
        {
            if (!string.IsNullOrEmpty(value))
                if (!value.All(char.IsDigit))
                    throw new DataValidationException("Numbers only");

            this.RaiseAndSetIfChanged(ref _zipCode, value);
        }
    }

    public string CprNumber
    {
        get => _cprNumber;
        set
        {
            // if (!string.IsNullOrEmpty(value))
            //     if (!value.All(char.IsDigit))
            //         if (!value.Contains("-"))
            //             throw new DataValidationException("Numbers only");


            this.RaiseAndSetIfChanged(ref _cprNumber, value);
        }
    }

    public bool BtnCreateEnabled
    {
        get => _btnCreateEnabled;
        set => this.RaiseAndSetIfChanged(ref _btnCreateEnabled, value);
    }

    #endregion

    public ICommand CancelCommand { get; }
    public ICommand CreateCommand { get; }
    public ICommand IsCorporateCommand { get; }
    public ICommand IsPrivateCommand { get; }

#pragma warning disable
    public CreateUserViewModel()
    {
        CancelCommand = ReactiveCommand.Create(() => { Navigate(new LoginView()); });
        CreateCommand = ReactiveCommand.Create(Create);
        IsCorporateCommand = ReactiveCommand.Create(Corporate);
        IsPrivateCommand = ReactiveCommand.Create(Private);

        InitializeCreateButtonState();
    }
#pragma warning restore

    #region Methods

    private void Create()
    {
        if (_isPrivate)
            try
            {
                CreatePrivateUser(UserName, PassWord, ZipCode, CprNumber);
            }
            catch (Exception e) when (e is ArgumentException or DataException)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        if (_isCorporate)
            try
            {
                CreateCorporateUser(UserName, PassWord, ZipCode, CvrNumber);
            }
            catch (Exception e) when (e is ArgumentException or DataException)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        Navigate(new LoginView());
    }

    /// <summary>
    ///     Creates a new user with the provided username and password.
    ///     <remarks>
    ///         This method sends a request to the server to create a new user with the given credentials.
    ///         If the user is created successfully, it will print a success message.
    ///         If any exception occurs during the process, it will be caught and re-thrown.
    ///     </remarks>
    /// </summary>
    private void CreatePrivateUser(string username, string password, string zipCode, string cprNumber)
    {
        // Remove any whitespace from the CPR number.
        cprNumber = cprNumber.Replace(" ", "");

        var privateUser = new PrivateUser(0, cprNumber, new User(0, username, password.Trim(), zipCode));

        try
        {
            // Throw an exception if the CPR number is invalid.
            DatabaseManager.ValidateCprNumber(cprNumber);

            // Attempt to create the private user.
            DatabaseManager.SignUp(privateUser);
        }
        catch (Exception e) when (e is ArgumentException or DataException)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Console.WriteLine($"Success: Private user created with username {username}");
    }

    /// <summary>
    ///     Creates a new user with the provided username and password.
    /// </summary>
    private void CreateCorporateUser(string username, string password, string zipCode, string cvrNumber)
    {
        var corporateUser = new CorporateUser(0, cvrNumber, 0, new User(0, username, password, zipCode));

        try
        {
            DatabaseManager.SignUp(corporateUser);
        }
        catch (Exception e) when (e is ArgumentException or DataException)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Console.WriteLine($"Success: Corporate user created with username {username}");
    }

    /// <summary>
    /// Sets the user type to Corporate 
    /// </summary>
    private void Corporate()
    {
        _isPrivate = false;
        _isCorporate = true;
    }

    /// <summary>
    /// Sets the user type to Private 
    /// </summary>
    private void Private()
    {
        _isCorporate = false;
        _isPrivate = true;
    }


    /// <summary>
    /// Initializes the state of the Create button based on the values of user input fields.
    /// </summary>
    /// <remarks>
    /// This method subscribes to changes in the UserName, PassWord, and RPassWord properties
    /// and evaluates whether the Create button should be enabled or disabled based on the presence
    /// of non-empty and non-whitespace values in these input fields. If all input fields have
    /// valid values, the BtnCreateEnabled property is set to true, enabling the Create button;
    /// otherwise, it is set to false, disabling the button.
    /// </remarks>
    private void InitializeCreateButtonState()
    {
        this.WhenAnyValue(
                x => x.UserName,
                x => x.PassWord,
                x => x.RepeatPassword,
                x => x.ZipCode,
                (userName, passWord, repeatPass, zipCode) =>
                    !string.IsNullOrWhiteSpace(userName) &&
                    !string.IsNullOrWhiteSpace(passWord) &&
                    !string.IsNullOrWhiteSpace(zipCode) &&
                    !string.IsNullOrWhiteSpace(repeatPass))
            .Subscribe(x => BtnCreateEnabled = x);
    }

    #endregion
}