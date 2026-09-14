using System.Windows;
using System.Windows.Media;

namespace Calculator.Infrastructure
{
    public static class ThemeManager
    {
        public static void ChangeTheme(ResourceDictionary res, string theme)
        {
            if (theme == "Светлая")
            {
                res["MainBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(240, 240, 245)); //светло-серый
                res["ButtonBackgroundBrush"] = new SolidColorBrush(Colors.White); //белый
                res["ButtonForegroundBrush"] = new SolidColorBrush(Colors.Black); //черный
                res["ForegroundBrush"] = new SolidColorBrush(Colors.Black); //черный
                res["OpForegroundBrush"] = new SolidColorBrush(Color.FromRgb(0, 122, 255)); //синий
                res["EqualBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(100, 200, 255)); //светло-синий
                res["NeonBorderBrush"] = new SolidColorBrush(Colors.Transparent); //прозрачный
                res["HoverOverlayBrush"] = new SolidColorBrush(Colors.Black); //черный для затемнения
            }
            else if (theme == "Темная")
            {
                res["MainBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(20, 20, 20)); //темно-серый
                res["ButtonBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(40, 40, 40)); //темно-серый
                res["ButtonForegroundBrush"] = new SolidColorBrush(Colors.White); //белый
                res["ForegroundBrush"] = new SolidColorBrush(Colors.White); //белый
                res["OpForegroundBrush"] = new SolidColorBrush(Color.FromRgb(0, 200, 255)); //голубой
                res["EqualBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(0, 130, 180)); //темно-голубой
                res["NeonBorderBrush"] = new SolidColorBrush(Colors.Transparent); //прозрачный
                res["HoverOverlayBrush"] = new SolidColorBrush(Color.FromRgb(0, 200, 255)); //голубой для неона при наведении
            }
            else if (theme == "Серая")
            {
                res["MainBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(59, 59, 59)); //базовый серый
                res["ButtonBackgroundBrush"] = new SolidColorBrush(Colors.DarkSlateGray); //темно-серо-зеленый грифельный
                res["ButtonForegroundBrush"] = new SolidColorBrush(Colors.GhostWhite); //призрачно-белый
                res["ForegroundBrush"] = new SolidColorBrush(Colors.GhostWhite); //призрачно-белый
                res["OpForegroundBrush"] = new SolidColorBrush(Colors.DarkOrange); //темно-оранжевый
                res["EqualBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(210, 105, 30)); //шоколадно-оранжевый
                res["NeonBorderBrush"] = new SolidColorBrush(Colors.Transparent); //прозрачный
                res["HoverOverlayBrush"] = new SolidColorBrush(Colors.Black); //черный для затемнения при наведении
            }
            else if (theme == "Неоновая")
            {
                res["MainBackgroundBrush"] = new SolidColorBrush(Colors.Black); //черный
                res["ButtonBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(15, 0, 5)); //темно-красный
                res["ButtonForegroundBrush"] = new SolidColorBrush(Colors.White); //белый
                res["ForegroundBrush"] = new SolidColorBrush(Color.FromRgb(255, 0, 70)); //неоново-красный
                res["OpForegroundBrush"] = new SolidColorBrush(Color.FromRgb(255, 0, 70)); //неоново-красный
                res["EqualBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(220, 0, 50)); //темно-красный
                res["NeonBorderBrush"] = new SolidColorBrush(Color.FromRgb(255, 0, 70)); //неоново-красный, светящаяся рамка
                res["HoverOverlayBrush"] = new SolidColorBrush(Color.FromRgb(255, 0, 70)); //ярко-розовый при наведении
            }
        }
    }
}
