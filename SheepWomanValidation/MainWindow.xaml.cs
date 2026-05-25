using System.Windows;
using SheepWomanValidation.Entity;
using SheepWomanValidation.ViewModels;

namespace SheepWomanValidation;

public partial class SheepWomanInputForm : Window
{
    public SheepWomanInputForm()
    {
        InitializeComponent();
        DataContext = new SheepWomanValidationViewModel<SheepWomanEntity>();
    }
}
