using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using GUI.Views.UserControls;
using ReactiveUI;
using static GUI.Utilities.ContentArea;

namespace GUI.ViewModels;

public class CreateUserViewModel : ViewModelBase
{
    #region Properties

    private string _userName;
    private string _passWord;
    private string _rPassWord;
    private bool _isCorporate = false;
    private bool _isPrivate = false;
    private bool _btnCreateEnabled = false;


    [Required]
    public string UserName
    {
        get => _userName;
        set => this.RaiseAndSetIfChanged(ref _userName, value);
    }

    [Required]
    public string PassWord
    {
        get => _passWord;
        set => this.RaiseAndSetIfChanged(ref _passWord, value);
    }

    [Required]
    public string RPassWord
    {
        get => _rPassWord;
        set => this.RaiseAndSetIfChanged(ref _rPassWord, value);
    }

    public bool IsCorporate
    {
        get => _isCorporate;
        set => this.RaiseAndSetIfChanged(ref _isCorporate, value);
    }

    public bool IsPrivate
    {
        get => _isPrivate;
        set => this.RaiseAndSetIfChanged(ref _isPrivate, value);
    }

    public bool BtnCreateEnabled
    {
        get => _btnCreateEnabled;
        set => this.RaiseAndSetIfChanged(ref _btnCreateEnabled, value);
    }

    #endregion

    public ICommand CancelCommand { get; }
    public ICommand CreateCommand { get; }


    public CreateUserViewModel()
    {
        CancelCommand = ReactiveCommand.Create(Cancel);
        CreateCommand = ReactiveCommand.Create(Create);


        UserName = string.Empty;
        PassWord = string.Empty;
        RPassWord = string.Empty;

        InitializeCreateButtonState();
    }


    #region Methods

    private void Create()
    {
        if (IsPrivate)
            if (CreateUser(UserName, PassWord, RPassWord, IsPrivate))
            {
                Navigate(new LoginView());
                return;
            }

        if (CreateUser(UserName, PassWord, RPassWord, IsCorporate))
        {
            Navigate(new LoginView());
        }
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
    /// <exception cref="ArgumentNullException">Thrown when either username or password is null or empty.</exception>
    /// <remarks>
    /// This method sends a request to the server to create a new user with the given credentials.
    /// If the user is created successfully, it will print a success message.
    /// If any exception occurs during the process, it will be caught and re-thrown.
    /// </remarks>
    private bool CreateUser(string username, string password, string rPassword, bool isWhat)
    {
        if (string.IsNullOrEmpty(password) && password.Equals(password))
        {
            Console.WriteLine("No Can Do");
            return false;
        }

        //#TODO: Send request to the server
        Console.WriteLine($"New '{username}' has been created successfully");
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
    void InitializeCreateButtonState()
    {
        this.WhenAnyValue(
                x => x.UserName,
                x => x.PassWord,
                x => x.RPassWord,
                (userName, passWord, rpassWord) => !string.IsNullOrWhiteSpace(userName) &&
                                                   !string.IsNullOrWhiteSpace(passWord) &&
                                                   !string.IsNullOrWhiteSpace(rpassWord))
            .Subscribe(x => BtnCreateEnabled = x);
    }

    #endregion
}