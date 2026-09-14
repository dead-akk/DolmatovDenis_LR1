using System.Windows;
using Calculator.Infrastructure;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string BaseTheme = "Темная";
        public static string EngTheme = "Светлая";

        public static void ChangeTheme(ResourceDictionary res, string theme)
        {
            ThemeManager.ChangeTheme(res, theme);
        }
    }
}
