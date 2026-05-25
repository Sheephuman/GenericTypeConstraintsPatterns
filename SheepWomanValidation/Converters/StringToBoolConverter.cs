using System;
using System.Globalization;
using System.Windows.Data;

namespace SheepWomanValidation.Converters
{
    /// <summary>
    /// enum 型のプロパティと RadioButton の IsChecked を双方向でバインディングするためのコンバーター。
    /// 
    /// 【何をするクラス？】
    /// RadioButton は IsChecked が bool 型ですが、ViewModel のプロパティが enum 型の場合に
    /// 「この RadioButton が選ばれたら、この enum 値を ViewModel にセットする」処理を実現します。
    /// 
    /// 例：
    /// ・年齢層を AgeGroup 列挙体で管理したいとき
    /// ・性格傾向を PersonalityType 列挙体で管理したいとき
    /// </summary>
    public class EnumToBoolConverter : IValueConverter
    {
        /// <summary>
        /// ViewModel (source) → UI (RadioButton) 方向の変換
        /// 
        /// RadioButton の IsChecked に何をセットするかを決める
        /// </summary>
     
        /// <returns>IsChecked に設定する bool 値</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            return value.ToString() == parameter.ToString();
        }

        /// <summary>
        /// UI (RadioButton) → ViewModel (source) 方向の変換
        /// 
        /// RadioButton がクリックされてチェックされたときに ViewModel の enum に何を入れるか決める        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            if (value is bool trueValue && trueValue && parameter != null)
            {
                // parameter の文字列を enum に変換して ViewModel に返す
                return Enum.Parse(targetType, parameter.ToString()!);
            }

            // 戻り値を Binding.DoNothing にしないとバグる
            return Binding.DoNothing;
        }
    }
}
