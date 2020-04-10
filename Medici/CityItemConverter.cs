using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Medici
{
    public class CityObjectToColor : IValueConverter
    {

        public object Convert(object color, Type targetType, object parameter, CultureInfo culture)
        {
            switch (color)
            {
                case ELEMENT.BLACK:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.RED:
                    return new SolidColorBrush(Colors.Red);
                case ELEMENT.BLUE:
                    return new SolidColorBrush(Colors.Blue);
                case ELEMENT.YELLOW:
                    return new SolidColorBrush(Colors.Yellow);
                case ELEMENT.FADED:
                    return new SolidColorBrush(Colors.DarkSeaGreen);
                case ELEMENT.VACCINE:
                    return new SolidColorBrush(Colors.Orange);
                case ELEMENT.QUARANTINE:
                    return new SolidColorBrush(Colors.White);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CityObjectToFontColor : IValueConverter
    {

        public object Convert(object color, Type targetType, object parameter, CultureInfo culture)
        {
            switch (color)
            {
                case ELEMENT.BLACK:
                    return new SolidColorBrush(Colors.LightGray);
                case ELEMENT.RED:
                    return new SolidColorBrush(Colors.Maroon);
                case ELEMENT.BLUE:
                    return new SolidColorBrush(Colors.LightSkyBlue);
                case ELEMENT.YELLOW:
                    return new SolidColorBrush(Colors.Brown);
                case ELEMENT.FADED:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.VACCINE:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.QUARANTINE:
                    return new SolidColorBrush(Colors.White);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
