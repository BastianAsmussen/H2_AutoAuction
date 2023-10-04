using System.Windows.Input;
using Data.Classes;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class UserProfileViewModel : ViewModelBase
{
    private readonly User _user;

    public string Username => _user.Username;

    public string FormattedBalance => $"Konto: {_user.Balance:C0}";
    public string FormattedActiveAuctions => $"Aktive Auktioner: {DatabaseManager.GetAuctionsByUser(_user).FindAll(a => a.Buyer == null).Count:N0}";
    public string FormattedAuctionsSold => $"Auktioner Solgt: {DatabaseManager.GetAllAuctions().FindAll(a => a.Seller.UserId == _user.UserId && a.Buyer != null).Count:N0}";
    public string FormattedAuctionsWon => $"Auktioner Vundet: {DatabaseManager.GetAllAuctions().FindAll(a => a.Buyer != null && a.Buyer.UserId == _user.UserId).Count:N0}";

    public ICommand ChangePasswordCommand => ReactiveCommand.Create(() =>
    {
        var changePasswordWindow = new ChangePasswordWindow(_user);

        changePasswordWindow.Show();
    });
    public ICommand BackCommand => ReactiveCommand.Create(() => { ContentArea.Navigate(new HomeScreenView()); });

    public UserProfileViewModel(User user)
    {
        _user = user;
    }
}