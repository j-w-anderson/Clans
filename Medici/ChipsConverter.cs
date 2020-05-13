using Engine.Utils;
using Engine.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Clans
{
    public class ChipsToVisibility : IValueConverter
    {

        public object Convert(object chips, Type targetType, object parameter, CultureInfo culture)
        {
            int chips_lost = 12-int.Parse(chips.ToString());
            int this_chip = int.Parse(parameter.ToString());

            return chips_lost-this_chip<=0?Visibility.Visible:Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ChipsYConverter: IValueConverter
    { 
        public object Convert(object target, Type targetType, object cid, CultureInfo culture)
        {
            double Y = GameData.EpochTrack[int.Parse(cid as string),3] / 100.0 * 1135 * 600 / 900;
            return Y + 13;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
