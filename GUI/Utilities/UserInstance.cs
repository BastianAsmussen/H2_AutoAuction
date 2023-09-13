using Data.Classes;
using Data.Interfaces;
using GUI.ViewModels;
using ReactiveUI;

namespace GUI.Utilities;

public abstract class UserInstance : ViewModelBase
{
    private static ISeller User;

    public static void SetUser(ISeller user)
    {
        User = user;
    }

    public static ISeller GetUser()
    {
        return User;
    }
}