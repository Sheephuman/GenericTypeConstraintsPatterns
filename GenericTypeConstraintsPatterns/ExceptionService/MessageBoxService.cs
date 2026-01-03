

namespace GenericTypeConstraintsPatterns
{
    using System;
    using System.Windows;



    /// <summary>
    /// 継承にしない理由
    /*
     * 
     * class MyJsonException : JsonException
これをやると、

・catch (JsonException) では捕まる
・でも「System.Text.Json が投げたのか、自分が投げたのか」区別が曖昧
・ライブラリ境界がにじむ
・将来 JsonException が sealed になったら即死

つまり「拡張」ではなく「侵入」になる。
     */
    /// </summary>
    public static class MessageBoxService
    {
        private const string DefaultTitle = "Application Error";

        public static void ShowError(string message, string? title = null)
        {
            Show(
                message,
                title ?? DefaultTitle,
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }

        public static void ShowError(Exception exception, string? title = null)
        {
            ShowError(exception.Message, title);
        }

        public static void ShowInfo(string message, string? title = null)
        {
            Show(
                message,
                title ?? "Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        private static void Show(
            string message,
            string title,
            MessageBoxButton buttons,
            MessageBoxImage icon
        )
        {
            if (Application.Current?.Dispatcher?.CheckAccess() == true)
            {
                MessageBox.Show(message, title, buttons, icon);
            }
            else
            {
                Application.Current?.Dispatcher?.Invoke(() =>
                    MessageBox.Show(message, title, buttons, icon));
            }
        }
    }

}
