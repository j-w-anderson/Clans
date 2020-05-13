using Engine.Model;
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
    public class PlayerColorConverter : IValueConverter
    {
        public object Convert(object cid, Type targetType, object parameter, CultureInfo culture)
        {
            int clan_id = int.Parse(cid.ToString());
            switch (clan_id)
            {
                case 0:
                    return new SolidColorBrush(Colors.Orange);
                case 1:
                    return new SolidColorBrush(Colors.Pink);
                case 2:
                    return new SolidColorBrush(Colors.Brown);
                case 3:
                    return new SolidColorBrush(Colors.Gray);
                case 4:
                    return new SolidColorBrush(Colors.Blue);
            }
            throw new ArgumentOutOfRangeException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ActivePlayerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
