using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Clans
{

    public class RegionXConverter : IValueConverter
    {

        public object Convert(object X, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)X / 100.0 * 1135;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RegionYConverter : IValueConverter
    {

        public object Convert(object y, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)y / 100.0 * 1135 * 600 / 900+20;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HutsIndexer : IValueConverter
    {
        public object Convert(object huts, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<int> Huts = huts as ObservableCollection<int>;
            return Huts[0].ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class HutsGreaterThanZero : IValueConverter
    {
        public Object Convert(object hut_count, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)hut_count;
            return (count> 0) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
