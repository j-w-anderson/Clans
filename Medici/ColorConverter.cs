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
    public class CityDotColorConverter : IValueConverter
    {

        public object Convert(object color, Type targetType, object parameter, CultureInfo culture)
        {
            switch (color)
            {
                case ELEMENT.YELLOW:
                    return new SolidColorBrush(Colors.Yellow);
                case ELEMENT.RED:
                    return new SolidColorBrush(Colors.Red);
                case ELEMENT.BLUE:
                    return new SolidColorBrush(Colors.Blue);
                case ELEMENT.BLACK:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.FADED:
                    return new SolidColorBrush(Colors.Lime);
                case ELEMENT.VACCINE:
                    return new SolidColorBrush(Colors.Orange);
                case ELEMENT.NUKED:
                    return new SolidColorBrush(Colors.Brown);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class CardColorConverter : IValueConverter
    {

        public object Convert(object color, Type targetType, object parameter, CultureInfo culture)
        {
            switch (color)
            {
                case RESOURCE.SAFFRON:
                    return new SolidColorBrush(Colors.Yellow);
                case RESOURCE.CHILI:
                    return new SolidColorBrush(Colors.IndianRed);
                case RESOURCE.INDIGO:
                    return new SolidColorBrush(Colors.Indigo);
                case RESOURCE.PEPPER:
                    return new SolidColorBrush(Colors.Black);
                case RESOURCE.TEA:
                    return new SolidColorBrush(Colors.ForestGreen);
                case RESOURCE.GOLD:
                    return new SolidColorBrush(Colors.Gold);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class CardFontColorConverter : IValueConverter
    {

        public object Convert(object color, Type targetType, object parameter, CultureInfo culture)
        {
            switch (color)
            {
                case RESOURCE.SAFFRON:
                    return new SolidColorBrush(Colors.Orange);
                case RESOURCE.CHILI:
                    return new SolidColorBrush(Colors.Pink);
                case RESOURCE.INDIGO:
                    return new SolidColorBrush(Colors.Blue);
                case RESOURCE.PEPPER:
                    return new SolidColorBrush(Colors.Gray);
                case RESOURCE.TEA:
                    return new SolidColorBrush(Colors.LightGreen);
                case RESOURCE.GOLD:
                    return new SolidColorBrush(Colors.Black);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PawnColorToLocConverter : IValueConverter
    {

        public object Convert(object color, Type targetType, object parameter, CultureInfo culture)
        {
            switch (color)
            {
                case ELEMENT.YELLOW:
                    return new SolidColorBrush(Colors.Yellow);
                case ELEMENT.RED:
                    return new SolidColorBrush(Colors.Red);
                case ELEMENT.BLUE:
                    return new SolidColorBrush(Colors.Blue);
                case ELEMENT.BLACK:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.FADED:
                    return new SolidColorBrush(Colors.Lime);
                case ELEMENT.VACCINE:
                    return new SolidColorBrush(Colors.Orange);
                case ELEMENT.NUKED:
                    return new SolidColorBrush(Colors.Brown);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class CubeCountToOutlineColor : IValueConverter
    {

        public object Convert(object count, Type targetType, object parameter, CultureInfo culture)
        {
            switch (count)
            {
                case 1:
                    return new SolidColorBrush(Colors.White);
                case 2:
                    return new SolidColorBrush(Colors.Orange);
                case 3:
                    return new SolidColorBrush(Colors.HotPink);
                default:
                    return new SolidColorBrush(Colors.White);

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
