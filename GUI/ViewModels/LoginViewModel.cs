using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Data.Classes;
using Data.Interfaces;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class LoginViewModel : ViewModelBase
{
    #region Properties

    private string _userName;
    private string _passWord;

    private bool _btnLoginEnabled = false;

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
        if (PassWord != "password")
        {
            UserInstance.SetUser(new DemoUser($"{UserName}", 213, 9000));
            ContentArea.Navigate(new HomeScreenView());
        }
        else
        {
            Console.WriteLine("No Can Do");
        }
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

public class DemoUser 
{
    public string UserName { get; set; }
    public decimal Balance { get; set; }
    public uint Zipcode { get; set; }

    public DemoUser(string UserName, decimal Balance, uint Zipcode)
    {
        this.UserName = UserName;
        this.Balance = Balance;
        this.Zipcode = Zipcode;
    }

    public string ReceiveBidNotification(string message)
    {
        return "ReceiveBidNotification !!";
    }
}