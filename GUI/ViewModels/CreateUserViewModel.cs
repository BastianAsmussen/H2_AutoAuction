using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Avalonia.Data;
using Data.Classes;
using GUI.Views.UserControls;
using ReactiveUI;
using static GUI.Utilities.ContentArea;

namespace GUI.ViewModels;

public class CreateUserViewModel : ViewModelBase
{
    private string _userName;
    private string _passWord;
    private string _rPassWord;
    private bool _isCorporate;
    private string _cvrNumber;
    private bool _isPrivate;
    private string _zipCode;
    private string _cprNumber;
    private bool _btnCreateEnabled;

    #region Properties

    [MinLength(1, ErrorMessage = "Username cannot be empty")]
    public string UserName
    {
        get => _userName;
        set => this.RaiseAndSetIfChanged(ref _userName, value);
    }

    [MinLength(1, ErrorMessage = "Password cannot be empty")]
    public string PassWord
    {
        get => _passWord;
        set => this.RaiseAndSetIfChanged(ref _passWord, value);
    }

    [MinLength(1, ErrorMessage = "Rewrite the password")]
    public string RPassWord
    {
        get => _rPassWord;
        set => this.RaiseAndSetIfChanged(ref _rPassWord, value);
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

    [MinLength(1, ErrorMessage = "")]
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

    [MinLength(1, ErrorMessage = "")]
    public string CprNumber
    {
        get => _cprNumber;
        set
        {
            if (!string.IsNullOrEmpty(value))
                if (!value.All(char.IsDigit))
                    if (!value.Contains("-"))
                    {
                        throw new DataValidationException("Numbers only");
                    }

            if (value.Length == 10)
            {
                value = value.Insert(6, "-");
            }

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


    public CreateUserViewModel()
    {
        CancelCommand = ReactiveCommand.Create(() => { Navigate(new LoginView()); });
        CreateCommand = ReactiveCommand.Create(Create);
        IsCorporateCommand = ReactiveCommand.Create(Corporate);
        IsPrivateCommand = ReactiveCommand.Create(Private);

        InitializeCreateButtonState();
    }

    #region Methods

    private void Create()
    {
        if (_isPrivate)
            try
            {
                CreatePrivateUser(UserName, PassWord, RPassWord, Convert.ToUInt32(ZipCode),
                    Convert.ToUInt32(CprNumber));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        if (_isCorporate)
            try
            {
                CreateCorporateUser(UserName, PassWord, RPassWord, Convert.ToUInt32(ZipCode),
                    Convert.ToUInt32(CvrNumber), Convert.ToDecimal(Credit));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        Navigate(new LoginView());
    }

    /// <summary>
    /// Creates a new user with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user to create.</param>
    /// <param name="password">The password of the user to create.</param>
    /// <param name="rPassword">The ReWritten password</param>
    /// <param name="zipCode">Users ZipCOde</param>
    /// <param name="cprNumber">Users CprNumb</param>
    /// <exception cref="ArgumentNullException">Thrown when either username or password is null or empty.</exception>
    /// <remarks>
    /// This method sends a request to the server to create a new user with the given credentials.
    /// If the user is created successfully, it will print a success message.
    /// If any exception occurs during the process, it will be caught and re-thrown.
    /// </remarks>
    private void CreatePrivateUser(string username, string password, string rPassword, uint zipCode, uint cprNumber)
    {
        if (!password.Equals(rPassword))
            throw new Exception("Password does not match");

        if (string.IsNullOrEmpty(ZipCode) || string.IsNullOrEmpty(CprNumber))
            throw new Exception("ZipCode or CprNumber is empty");

        PrivateUser unused = new(username, rPassword, zipCode, cprNumber);

        //#TODO: Send request to the server
        Console.WriteLine($"New '{unused.UserName}' has been created successfully");
    }

    /// <summary>
    /// Creates a new user with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user to create.</param>
    /// <param name="password">The password of the user to create.</param>
    /// <param name="rPassword"></param>
    /// <param name="zipCode"></param>
    /// <param name="cvrNumber"></param>
    /// <param name="credit"></param>
    /// <exception cref="ArgumentNullException">Thrown when either username or password is null or empty.</exception>
    /// <remarks>
    /// This method sends a request to the server to create a new user with the given credentials.
    /// If the user is created successfully, it will print a success message.
    /// If any exception occurs during the process, it will be caught and re-thrown.
    /// </remarks>
    private void CreateCorporateUser(string username, string password, string rPassword, uint zipCode, uint cvrNumber,
        decimal credit)
    {
        if (!password.Equals(rPassword))
            throw new Exception("Password does not match");

        if (string.IsNullOrEmpty(CvrNumber) || string.IsNullOrEmpty(Credit))
            throw new Exception("ZipCode, CvrNumber or Credit is empty");


        CorporateUser unused = new(username, rPassword, zipCode, cvrNumber, credit);

        //#TODO: Send request to the server
        Console.WriteLine($"New '{unused.UserName}' has been created successfully");
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
                x => x.RPassWord,
                x => x.ZipCode,
                (userName, passWord, rpassWord, zipCode) =>
                    !string.IsNullOrWhiteSpace(userName) &&
                    !string.IsNullOrWhiteSpace(passWord) &&
                    !string.IsNullOrWhiteSpace(zipCode) &&
                    !string.IsNullOrWhiteSpace(rpassWord))
            .Subscribe(x => BtnCreateEnabled = x);
    }

    #endregion
}