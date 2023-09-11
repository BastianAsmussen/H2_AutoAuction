using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ReactiveUI;

namespace GUI.ViewModels;

public class LoginViewModel : ReactiveObject, INotifyDataErrorInfo
{
    private string _userName;
    private string _passWord;
    private bool _btnLoginEnabled;

    public string UserName
    {
        get => _userName;
        set => this.RaiseAndSetIfChanged(ref _userName, value);
    }

    public string PassWord
    {
        get => _passWord;
        set => this.RaiseAndSetIfChanged(ref _passWord, value);
    }


    public LoginViewModel()
    {
        UserName = string.Empty;
        PassWord = string.Empty;
    }

    private void Validate_EMail()
    {
        // first of all clear all previous errors
        ClearErrors(nameof(UserName));

        // No empty string allowed
        if (string.IsNullOrEmpty(UserName))
        {
            AddError(nameof(UserName), "This field is required");
        }

        // No empty string allowed
        if (string.IsNullOrEmpty(PassWord))
        {
            AddError(nameof(PassWord), "This field is required");
        }
    }


    #region DataError Info

    // Implement members of INotifyDataErrorInfo

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    // we have errors present if errors.Count is greater than 0
    public bool HasErrors => errors.Count > 0;

    /// <inheritdoc />
    public IEnumerable GetErrors(string? propertyName)
    {
        // Get entity-level errors when the target property is null or empty
        if (string.IsNullOrEmpty(propertyName))
        {
            return errors.Values.SelectMany(static errors => errors);
        }

        // Property-level errors, if any
        if (this.errors.TryGetValue(propertyName!, out List<ValidationResult>? result))
        {
            return result;
        }

        // In case there are no errors we return an empty array.
        return Array.Empty<ValidationResult>();
    }

    // Store Errors in a Dictionary
    private Dictionary<string, List<ValidationResult>> errors = new Dictionary<string, List<ValidationResult>>();

    /// <summary>
    /// Clears the errors for a given property name.
    /// </summary>
    /// <param name="propertyName">The name of the property to clear or all properties if <see langword="null"/></param>
    protected void ClearErrors(string? propertyName = null)
    {
        // Clear entity-level errors when the target property is null or empty
        if (string.IsNullOrEmpty(propertyName))
        {
            errors.Clear();
        }
        else
        {
            errors.Remove(propertyName);
        }

        // Notify that errors have changed
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
    }

    /// <summary>
    /// Adds a given error message for a given property name.
    /// </summary>
    /// <param name="propertyName">the name of the property</param>
    /// <param name="errorMessage">The error message to show</param>
    protected void AddError(string propertyName, string errorMessage)
    {
        // Add the cached errors list for later use.
        if (!errors.TryGetValue(propertyName, out List<ValidationResult>? propertyErrors))
        {
            propertyErrors = new List<ValidationResult>();
            errors.Add(propertyName, propertyErrors);
        }

        propertyErrors.Add(new ValidationResult(errorMessage));

        // Notify that errors have changed
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
    }

    #endregion
}