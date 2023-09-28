using System.Collections.ObjectModel;
using System.Windows.Input;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class HomeScreenViewModel : ViewModelBase
{
    #region Properties

    private ObservableCollection<string> _userAuctions = new() ;
    private ObservableCollection<string> _currentAuctions = new();

    public ObservableCollection<string> UserAuctions
    {
        get => _userAuctions;
        set => this.RaiseAndSetIfChanged(ref _userAuctions, value);
    }

    public ObservableCollection<string> CurrentAuctions
    {
        get => _currentAuctions;
        set => this.RaiseAndSetIfChanged(ref _currentAuctions, value);
    }

    #endregion

    public ICommand SetForSaleCommand { get; }
    public ICommand UserProfileCommand { get; }
    public ICommand BidHistoryCommand { get; }

    public HomeScreenViewModel()
    {
        UserProfileCommand = ReactiveCommand.Create(ShowUserProfile);
        SetForSaleCommand = ReactiveCommand.Create(ShowSetForSale);
        BidHistoryCommand = ReactiveCommand.Create(ShowBidHistory);

    }


    #region Methods

    private void ShowUserProfile()
    {
        Utilities.ContentArea.Navigate(new UserProfileView());
    }


    private void ShowBidHistory()
    {
        Utilities.ContentArea.Navigate(new UserProfileView());
    }

    private void ShowSetForSale()
    {
        Utilities.ContentArea.Navigate(new SetForSaleView());
    }

    #endregion
}