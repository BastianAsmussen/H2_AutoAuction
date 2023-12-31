﻿using GUI.Utilities;
using GUI.Views.UserControls;

namespace GUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        ContentArea.Navigate(new LoginView());
    }

    public string Greeting => "Welcome to Avalonia!";
}