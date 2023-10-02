using System;
using Data.Classes;
using Data.Interfaces;
using GUI.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace GUI.Utilities;

#pragma warning disable
/// <summary>
/// This class is used to store the current user instance
/// </summary>
public abstract class UserInstance : ViewModelBase
{
    private static bool _canEdit = true;
    private static User _user;


    /// <summary>
    /// Sets the current user. The current user can only be set once,
    /// subsequent attempts will throw an AccessViolationException.
    /// </summary>
    /// <param name="user">The user to set as the current user.</param>
    /// <exception cref="AccessViolationException">
    /// Thrown when an attempt is made to change the current user after it has already been set.
    /// </exception>
    public static void SetCurrentUser(User user)
    {
        if (!_canEdit)
            throw new AccessViolationException("UserInstance is already set");

        _user = user;
        _canEdit = false;
    }

    /// <summary>
    /// Gets the currently set user.
    /// </summary>
    /// <returns>The currently set user.</returns>
    public static User GetCurrentUser()
    {
        if (_user == null)
            Console.WriteLine("UserInstance is not set");

        return _user;
    }

    public static void LogOut()
    {
        _user = null;
        _canEdit = true;
    }
}