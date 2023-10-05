using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.VisualBasic;

namespace GUI.Views.UserControls;

public partial class SetForSaleView : UserControl
{
    public SetForSaleView()
    {
        InitializeComponent();
    }

    public static int Year;

    private void DatePicker_OnSelectedDateChanged(object? sender, DatePickerSelectedValueChangedEventArgs e)
    {
        if (sender is DatePicker datePicker)
            if (datePicker.SelectedDate != null)
                if (datePicker.SelectedDate.Value.Year > DateTime.Now.Year)
                {
                    datePicker.SelectedDate = DateTime.Now;
                }
                else
                {
                    Year = datePicker.SelectedDate.Value.Year;
                }
    }
}