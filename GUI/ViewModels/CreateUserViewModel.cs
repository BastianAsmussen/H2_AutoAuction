using System;
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
    private string _credit;
    private bool _isPrivate;
    private string _zipCode;
    private string _cprNumber;
    private bool _btnCreateEnabled;

    #region Properties

    public string UserName
    {
        get => _userName;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(UserName), "Not a valid UserName");

            this.RaiseAndSetIfChanged(ref _userName, value);
        }
    }

    public string PassWord
    {
        get => _passWord;
        set => this.RaiseAndSetIfChanged(ref _passWord, value);
    }

    public string RPassWord
    {
        get => _rPassWord;
        set => this.RaiseAndSetIfChanged(ref _rPassWord, value);
    }

    public string CvrNumber
    {
        get => _cvrNumber;
        set => this.RaiseAndSetIfChanged(ref _cvrNumber, value);
    }

    public string Credit
    {
        get => _credit;
        set => this.RaiseAndSetIfChanged(ref _credit, value);
    }

    public string ZipCode
    {
        get => _zipCode;
        set => this.RaiseAndSetIfChanged(ref _zipCode, value);
    }

    public string CprNumber
    {
        get => _cprNumber;
        set => this.RaiseAndSetIfChanged(ref _cprNumber, value);
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
        CancelCommand = ReactiveCommand.Create(Cancel);
        CreateCommand = ReactiveCommand.Create(Create);
        IsCorporateCommand = ReactiveCommand.Create(() =>
        {
            _isCorporate = true;
            _isPrivate = false;
        });
        IsPrivateCommand = ReactiveCommand.Create(() =>
        {
            _isPrivate = true;
            _isCorporate = false;
        });

        UserName = string.Empty;
        PassWord = string.Empty;
        RPassWord = string.Empty;

        InitializeCreateButtonState();
    }


    #region Methods

    private void Create()
    {
        if (_isPrivate)
            if (CreatePrivateUser(UserName, PassWord, RPassWord, Convert.ToUInt32(ZipCode),
                    Convert.ToUInt32(CprNumber)))
                Navigate(new LoginView());

            else
                Console.WriteLine("No Can Do");

        if (_isCorporate)
            if (CreateCorporateUser(UserName, PassWord, RPassWord, Convert.ToUInt32(ZipCode),
                    Convert.ToUInt32(CvrNumber), Convert.ToDecimal(Credit)))
                Navigate(new LoginView());

            else
                Console.WriteLine("No Can Do");
    }

    private void Cancel()
    {
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
    private bool CreatePrivateUser(string username, string password, string rPassword, uint zipCode, uint cprNumber)
    {
        if (string.IsNullOrEmpty(password) && password.Equals(password))
            return false;

        PrivateUser unused = new(username, rPassword, zipCode, cprNumber);

        //#TODO: Send request to the server
        Console.WriteLine($"New '{unused.UserName}' has been created successfully");
        return true;
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
    private bool CreateCorporateUser(string username, string password, string rPassword, uint zipCode, uint cvrNumber,
        decimal credit)
    {
        if (string.IsNullOrEmpty(password) && password.Equals(password))
            return false;

        CorporateUser unused = new(username, rPassword, zipCode, cvrNumber, credit);

        //#TODO: Send request to the server
        Console.WriteLine($"New '{unused.UserName}' has been created successfully");
        return true;
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
                x => x._isCorporate,
                x => x._isPrivate,
                (userName, passWord, rpassWord, isCorporate, isPrivate) =>
                    !string.IsNullOrWhiteSpace(userName) &&
                    !string.IsNullOrWhiteSpace(passWord) &&
                    !string.IsNullOrWhiteSpace(rpassWord) && isCorporate || isPrivate)
            .Subscribe(x => BtnCreateEnabled = x);
    }

    #endregion
}