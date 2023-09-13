using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace GUI.ViewModels;

public class CreateUserViewModel : ViewModelBase
{
    
    #region Properties
    private string _userName;
    private string _passWord;
    private string _rPassWord;
    private bool _btnCreateEnabled;
    private bool _btnCancelEnabled;
   
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

    public bool BtnCreateEnabled
    {
        get => _btnCreateEnabled;
        set => this.RaiseAndSetIfChanged(ref _btnCreateEnabled, value);
    }

    public bool BtnCancelEnabled
    {
        get => _btnCancelEnabled;
        set => this.RaiseAndSetIfChanged(ref _btnCancelEnabled, value);
    }
    #endregion
    
    public CreateUserViewModel()
    {
        UserName = string.Empty;
        PassWord = string.Empty;
        RPassWord = string.Empty;
        BtnCreateEnabled = false;
        BtnCancelEnabled = true;
    }

}