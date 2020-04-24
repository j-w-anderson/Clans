using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Clans
{
    public class PanicLevelColorConverter : IValueConverter
    {

        public object Convert(object status, Type targetType, object parameter, CultureInfo culture)
        {
            switch (status)
            {
                case STATUS.NORMAL:
                    return new SolidColorBrush(Colors.Transparent);
                case STATUS.UNSTABLE:
                    return new SolidColorBrush(Colors.White);
                case STATUS.RIOTING2:
                case STATUS.RIOTING3:
                    return new SolidColorBrush(Colors.Yellow);
                case STATUS.COLLAPSING:
                    return new SolidColorBrush(Colors.Orange);
                case STATUS.FALLEN:
                    return new SolidColorBrush(Colors.Red);
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PanicLevelNumberConverter : IValueConverter
    {

        public object Convert(object status, Type targetType, object parameter, CultureInfo culture)
        {
            switch (status)
            {
                case STATUS.NORMAL:
                    return "";
                case STATUS.UNSTABLE:
                    return "1";
                case STATUS.RIOTING2:
                    return "2";
                case STATUS.RIOTING3:
                    return "3";
                case STATUS.COLLAPSING:
                    return "4";
                case STATUS.FALLEN:
                    return "5";
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PanicLevelImageConverter : IValueConverter
    {

        public object Convert(object status, Type targetType, object parameter, CultureInfo culture)
        {
            switch (status)
            {
                case STATUS.NORMAL:
                    return "";
                case STATUS.UNSTABLE:
                    return "Images/PanicLevel1s.png";
                case STATUS.RIOTING2:
                    return "Images/PanicLevel2s.png";
                case STATUS.RIOTING3:
                    return "Images/PanicLevel3s.png";
                case STATUS.COLLAPSING:
                    return "Images/PanicLevel4s.png";
                case STATUS.FALLEN:
                    return "Images/PanicLevel5s.png";
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
