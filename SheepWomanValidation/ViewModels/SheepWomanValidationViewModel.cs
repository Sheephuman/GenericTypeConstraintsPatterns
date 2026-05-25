using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using SheepWomanValidation.Entity;
using SheepWomanValidation.Validation;

namespace SheepWomanValidation.ViewModels;

public class SheepWomanValidationViewModel<T> : BindableBase
    where T : SheepWomanEntity, IValidatable<T>, new()
{
    public T Entity { get; } = new();

    private ObservableCollection<string> _validationErrors = new();

    public ObservableCollection<string> ValidationErrors
    {
        get => _validationErrors;
        set => SetProperty(ref _validationErrors, value);
    }

    public ICommand ValidateCommand { get; }

    public SheepWomanValidationViewModel()
    {
        ValidateCommand = new DelegateCommand(ExecuteValidate);
    }

    private void ExecuteValidate()
    {
        ValidationErrors.Clear();

        var result = Entity.Validate();

        if (!result.IsValid)
        {
            foreach (var err in result.Errors)
                ValidationErrors.Add(err);

            MessageBox.Show(
                "入力に問題があります。エラーを修正してください。",
                "検証失敗",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        else
        {
            MessageBox.Show(
                "検証成功！\n\nこのひつじ属性女性は問題なく登録可能です",
                "検証成功",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
