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


    public class CityCardColorConverter : IValueConverter
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
                case ELEMENT.EVENT:
                    return new SolidColorBrush(Colors.Orange);
                case ELEMENT.EPIDEMIC:
                    return new SolidColorBrush(Colors.DarkGreen);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class CityCardFontColorConverter : IValueConverter
    {

        public object Convert(object color, Type targetType, object parameter, CultureInfo culture)
        {
            switch (color)
            {
                case ELEMENT.YELLOW:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.RED:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.BLUE:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.EVENT:
                    return new SolidColorBrush(Colors.Black);
                case ELEMENT.BLACK:
                    return new SolidColorBrush(Colors.White);
                case ELEMENT.EPIDEMIC:
                    return new SolidColorBrush(Colors.LimeGreen);
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
