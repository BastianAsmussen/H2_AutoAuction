using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Data.Classes;
using Data.DatabaseManager;

namespace GUI.Views;

public partial class ChangePasswordWindow : Window
{
    private User _user;

    public ChangePasswordWindow(User user)
    {
        _user = user;

        InitializeComponent();
    }

    private void OnChangePasswordButtonClick(object? sender, RoutedEventArgs e)
    {
        var newPassword = NewPassword.Text;
        var confirmPassword = ConfirmPassword.Text;

        if (!newPassword.Equals(confirmPassword))
        {
            Console.WriteLine("Passwords do not match!");

            return;
        }

        var oldPassword = OldPassword.Text;

        if (newPassword.Equals(oldPassword))
        {
            Console.WriteLine("New password cannot be the same as the old password!");

            return;
        }

        try
        {
            _user = DatabaseManager.UpdatePassword(_user, oldPassword, newPassword);

            Console.WriteLine("Password changed successfully!");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }

        Close();
    }
}