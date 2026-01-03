using GenericTypeConstraintsPatterns.Entity;
using System.Windows;
using System.Windows.Controls;

namespace GenericTypeConstraintsPatterns.Selectors
{
    /* C# クラスは 振る舞い（SelectTemplate）の定義 */

    public sealed class EntityTemplateSelector : DataTemplateSelector
    {
        public EntityTemplateSelector()
        {
            UserTemplate = new();
            LogTemplate = new();
        }

        public DataTemplate? UserTemplate { get; set; }
        public DataTemplate? LogTemplate { get; set; }


        /// <summary>
        ///参照は0だが実際はリソースディクショナリで呼ばれる
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is UserEntity)
            {
                    if(UserTemplate is null)
                    throw new InvalidOperationException(nameof(UserTemplate));

                return  UserTemplate;
            }

            if (item is LogEntity)
            {
                if (LogTemplate is null)
                    throw new InvalidOperationException(nameof(LogTemplate));

                return LogTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }

}
