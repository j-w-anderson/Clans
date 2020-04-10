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
    public class OutbreakCountXConverter : IValueConverter
    {

        public object Convert(object count, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)count) % 2 == 0 ? 45 : 80;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OutbreakCountYConverter : IValueConverter
    {

        public object Convert(object count, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)count) * 23.5 + 294;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
