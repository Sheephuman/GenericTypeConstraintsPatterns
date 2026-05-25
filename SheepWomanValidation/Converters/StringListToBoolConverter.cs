using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SheepWomanValidation.Converters;

/// <summary>
/// 文字列リスト（趣味など）と CheckBox の IsChecked を双方向バインディングする Converter。
/// App.xaml で共有インスタンスとして登録し、ConvertBack でリストを直接更新する。
/// </summary>
public class StringListToBoolConverter : IValueConverter
{
    private IList? _currentList;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string item)
        {
            return false;
        }

        _currentList = value as IList;

        if (value is IEnumerable<string> strings)
        {
            return strings.Contains(item, StringComparer.OrdinalIgnoreCase);
        }

        if (value is IEnumerable enumerable)
        {
            foreach (var element in enumerable)
            {
                if (string.Equals(element?.ToString(), item, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string item || value is not bool isChecked || _currentList is null)
        {
            return Binding.DoNothing;
        }

        var existingIndex = FindIndex(_currentList, item);

        if (isChecked)
        {
            if (existingIndex < 0)
            {
                _currentList.Add(item);
            }
        }
        else if (existingIndex >= 0)
        {
            _currentList.RemoveAt(existingIndex);
        }

        return Binding.DoNothing;
    }

    private static int FindIndex(IList list, string item)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (string.Equals(list[i]?.ToString(), item, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }
}
