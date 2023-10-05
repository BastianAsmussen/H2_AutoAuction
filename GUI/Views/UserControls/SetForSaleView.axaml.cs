using System;
using Avalonia.Controls;

namespace GUI.Views.UserControls;

public partial class SetForSaleView : UserControl
{
    public static int Year;

    public SetForSaleView()
    {
        InitializeComponent();
    }

    private void DatePicker_OnSelectedDateChanged(object? sender, DatePickerSelectedValueChangedEventArgs e)
    {
        if (sender is DatePicker datePicker)
            if (datePicker.SelectedDate != null)
                if (datePicker.SelectedDate.Value.Year > DateTime.Now.Year)
                    datePicker.SelectedDate = DateTime.Now;
                else
                    Year = datePicker.SelectedDate.Value.Year;
    }
}