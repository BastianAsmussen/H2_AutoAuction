using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GUI.Utilities;
using ReactiveUI;

namespace GUI.ViewModels;

public class LoginViewModel : ViewModelBase
{
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
}