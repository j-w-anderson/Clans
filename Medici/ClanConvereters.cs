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
    public class ClanColorConverter : IValueConverter
    {
        public object Convert(object cid, Type targetType, object parameter, CultureInfo culture)
        {
            int clan_id = int.Parse(cid.ToString());
            switch (clan_id)
            {
                case 0:
                    return new SolidColorBrush(Colors.Red);
                case 1:
                    return new SolidColorBrush(Colors.Green);
                case 2:
                    return new SolidColorBrush(Colors.Yellow);
                case 3:
                    return new SolidColorBrush(Colors.Purple);
                case 4:
                    return new SolidColorBrush(Colors.Cyan);
            }
            throw new ArgumentOutOfRangeException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ClanScoreXConverter : IValueConverter
    {
        public object Convert(object points, Type targetType, object parameter, CultureInfo culture)
        {

            int Points = int.Parse(points.ToString())/10;
            int offset = (int.Parse(points.ToString())%10) * 4-10;
            double X = 0;
            if (Points == 0)
            {
                X = GameData.ScoreTrack[0,0] / 100 * 1123;
            } else
            {
                X = GameData.ScoreTrack[(Points-1)%50, 0] / 100.0 * 1123;
            }
            return X+offset+2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ClanScoreYConverter : IValueConverter
    {

        public object Convert(object points, Type targetType, object parameter, CultureInfo culture)
        {
            int Points = int.Parse(points.ToString())/10;
            int offset = (int.Parse(points.ToString())%10) * 4 - 10;
            double Y = 0;
            if (Points == 0)
            {
                Y = 2 * GameData.ScoreTrack[0, 1] - GameData.ScoreTrack[1, 1];
            }
            else
            {
                Y = GameData.ScoreTrack[(Points - 1)%50, 1] ;
            }
            Y = Y / 100.0 * 760 + 15; 
            return Y+offset;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
