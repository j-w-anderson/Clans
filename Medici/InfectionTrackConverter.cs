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

    public class InfectionTrackXConverter : IValueConverter
    {

        public object Convert(object count, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)count * 39.5 + 867;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CurrentPlayerConverter : IValueConverter
    {

        public object Convert(object ID, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)ID * 187;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
