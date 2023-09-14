using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class CreateUserViewModel : ViewModelBase
{
    #region Properties

    private string _userName;
    private string _passWord;
    private string _rPassWord;
    private bool _isCorporate;
    private bool _isPrivate;

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
    }

    void InputValidator()
    {
        // Validate input Email and Password, to enable Create Button
    }

    private void Create()
    {
        Utilities.ContentArea.Navigate(new LoginView());
    }

    private void Cancel()
    {
        Utilities.ContentArea.Navigate(new LoginView());
    }

    private void CreateUser()
    {
        if (PassWord.Equals(RPassWord))
        {
            // Send request to the server
            try
            {
                UserCreationRequest(UserName, PassWord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    private void UserCreationRequest(string username, string password)
    {
        try
        {
            Console.WriteLine("User created");
        }
        catch (Exception EX_NAME)
        {
            throw new Exception("User not created");
        }
    }
}