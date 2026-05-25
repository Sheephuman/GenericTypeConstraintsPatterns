using System.Collections.ObjectModel;
using Prism.Mvvm;
using SheepWomanValidation.Validation;

namespace SheepWomanValidation.Entity;

public class SheepWomanEntity : BindableBase, IValidatable<SheepWomanEntity>
{
    private SheepAttribute _attribute = SheepAttribute.FluffySheepEar;
    private AgeGroup _age = AgeGroup.Twenties;
    private ObservableCollection<string> _hobbies = new();
    private PersonalityType _personality = PersonalityType.Obedient;
    private string _freeComment = string.Empty;

    public SheepAttribute Attribute
    {
        get => _attribute;
        set => SetProperty(ref _attribute, value);
    }

    public AgeGroup Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }

    public ObservableCollection<string> Hobbies
    {
        get => _hobbies;
        set => SetProperty(ref _hobbies, value);
    }

    public PersonalityType Personality
    {
        get => _personality;
        set => SetProperty(ref _personality, value);
    }

    public string FreeComment
    {
        get => _freeComment;
        set => SetProperty(ref _freeComment, value);
    }

    public ValidationResult Validate()
    {
        var errors = new List<string>();

        if (Attribute == SheepAttribute.Other && string.IsNullOrWhiteSpace(FreeComment))
        {
            errors.Add("「その他」を選んだ場合は自由コメントを入力してください。");
        }

        if (Hobbies.Count == 0)
        {
            errors.Add("趣味を少なくとも1つ選択してください。");
        }

        return errors.Count == 0
            ? ValidationResult.Success()
            : ValidationResult.Failure(errors.ToArray());
    }
}
