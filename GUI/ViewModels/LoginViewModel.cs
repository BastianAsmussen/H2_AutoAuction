using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class LoginViewModel : ViewModelBase
{
    #region Properties

    private string _userName = null!;
    private string _passWord = null!;

    private bool _btnLoginEnabled;

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

    public bool BtnLoginEnabled
    {
        get => _btnLoginEnabled;
        set => this.RaiseAndSetIfChanged(ref _btnLoginEnabled, value);
    }

    public ICommand LoginCommand { get; }
    public ICommand SignUpCommand { get; }

    #endregion

    public LoginViewModel()
    {
        LoginCommand = ReactiveCommand.Create(GoToHomeScreen);
        SignUpCommand = ReactiveCommand.Create(GoToCreateScreen);

        ValidateInput();
    }

    private void GoToCreateScreen()
    {
        ContentArea.Navigate(new CreateUserView());
    }

    void GoToHomeScreen()
    {
        ContentArea.Navigate(new HomeScreenView());
    }

    /// <summary>
    /// If the user has entered a username and password, the login button will be enabled.
    /// </summary>
    private void ValidateInput()
    {
        this.WhenAnyValue(
                x => x.UserName,
                x => x.PassWord,
                (userName, passWord) => !string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(passWord))
            .Subscribe(x => BtnLoginEnabled = x);
    }
}