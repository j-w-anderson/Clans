using Engine;
using Engine.Model;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PandemicLegacy
{
    static class GameData
    {
        static string[,] CityData = new string[,] {{"Kairo","2","3","707","356","Khartum","Algier","Istanbul","Riad","Bagdad",""},
                                                    {"Algier","2","3","638","347","Kairo","Madrid","Paris","Istanbul","",""},
                                                    {"Bagdad","4","3","773","331","Kairo","Riad","Istanbul","Teheran","",""},
                                                    {"Teheran","4","3","833","282","Moskau","Bagdad","Delhi","Karatschi","",""},
                                                    {"Riad","4","3","782","397","Bagdad","Kairo","Karatschi","","",""},
                                                    {"Karatschi","4","3","846","355","Mumbai","Delhi","Teheran","Riad","",""},
                                                    {"Delhi","4","3","903","331","Teheran","Karatschi","Mumbai","Kalkutta","Chennai",""},
                                                    {"Mumbai","4","3","852","413","Delhi","Chennai","Karatschi","","",""},
                                                    {"Chennai","4","3","909","450","Mumbai","Delhi","Kalkutta","Jakarta","",""},
                                                    {"Kalkutta","4","3","956","353","Delhi","Chennai","Bangkok","Hong Kong","",""},
                                                    {"Istanbul","3","3","718","291","Mailand","Algier","Bagdad","Moskau","Kairo","St Petersburg"},
                                                    {"Moskau","3","3","780","247","St Petersburg","Istanbul","Teheran","","",""},
                                                    {"Madrid","3","2","562","296","New York","London","Paris","Sao Paulo","Algier",""},
                                                    {"London","3","2","573","223","New York","Madrid","Paris","Essen","",""},
                                                    {"Essen","3","2","651","209","London","Paris","Mailand","St Petersburg","",""},
                                                    {"Paris","3","2","631","261","London","Madrid","Essen","Mailand","Algier",""},
                                                    {"Mailand","3","2","682","246","Essen","Paris","Istanbul","","",""},
                                                    {"St Petersburg","3","2","739","192","Essen","Istanbul","Moskau","","",""},
                                                    {"San Francisco","0","2","195","292","Chicago","Los Angeles","Tokyo","Manila","",""},
                                                    {"Chicago","0","2","295","262","San Francisco","Atlanta","Montreal","Mexiko-Stadt","Los Angeles",""},
                                                    {"Atlanta","0","2","325","319","Chicago","Washington","Miami","","",""},
                                                    {"Washington","0","2","415","315","Atlanta","New York","Montreal","Miami","",""},
                                                    {"New York","0","2","429","270","Madrid","London","Montreal","Washington","",""},
                                                    {"Montreal","0","2","374","262","Chicago","New York","Washington","","",""},
                                                    {"Bangkok","4","1","968","416","Kalkutta","Hong Kong","Ho-Chi-Minh-Stadt","Jakarta","",""},
                                                    {"Hong Kong","4","1","1010","389","Ho-Chi-Minh-Stadt","Shanghai","Bangkok","Kalkutta","Taipeh","Manila"},
                                                    {"Seoul","4","1","1069","258","Tokyo","Shanghai","Peking","","",""},
                                                    {"Peking","4","1","1003","264","Shanghai","Seoul","","","",""},
                                                    {"Shanghai","4","1","1005","319","Hong Kong","Peking","Taipeh","Seoul","Tokyo",""},
                                                    {"Jakarta","5","1","968","517","Chennai","Bangkok","Ho-Chi-Minh-Stadt","Sydney","",""},
                                                    {"Ho-Chi-Minh-Stadt","5","1","1014","469","Manila","Hong Kong","Bangkok","Jakarta","",""},
                                                    {"Sydney","5","1","1137","621","Los Angeles","Manila","Jakarta","","",""},
                                                    {"Manila","5","1","1091","464","Ho-Chi-Minh-Stadt","Sydney","San Francisco","Taipeh","Hong Kong",""},
                                                    {"Taipeh","5","1","1088","352","Osaka","Manila","Hong Kong","Shanghai","",""},
                                                    {"Osaka","5","1","1131","351","Tokyo","Taipeh","","","",""},
                                                    {"Tokyo","5","1","1124","290","Osaka","Seoul","Shanghai","San Francisco","",""},
                                                    {"Lagos","2","0","623","447","Kinshasa","Khartum","Sao Paulo","","",""},
                                                    {"Kinshasa","2","0","676","500","Lagos","Khartum","Johannesburg","","",""},
                                                    {"Khartum","2","0","722","434","Lagos","Kinshasa","Johannesburg","Kairo","",""},
                                                    {"Johannesburg","2","0","720","580","Buenos Aires","Kinshasa","Khartum","","",""},
                                                    {"Los Angeles","0","0","212","369","San Francisco","Mexiko-Stadt","Chicago","Lima","Sydney",""},
                                                    {"Miami","0","0","369","368","Atlanta","Washington","Mexiko-Stadt","Bogota","",""},
                                                    {"Mexiko-Stadt","0","0","283","392","Los Angeles","Bogota","Miami","Lima","Chicago",""},
                                                    {"Bogota","1","0","367","458","Mexiko-Stadt","Lima","Sao Paulo","Buenos Aires","Miami",""},
                                                    {"Lima","1","0","340","542","Santiago","Bogota","Mexiko-Stadt","Los Angeles","",""},
                                                    {"Santiago","1","0","350","627","Lima","Buenos Aires","","","",""},
                                                    {"Buenos Aires","1","0","432","616","Santiago","Sao Paulo","Bogota","Johannesburg","",""},
                                                    {"Sao Paulo","1","0","476","552","Lagos","Madrid","Bogota","Buenos Aires","",""} };


        public static List<City> GetCities()
        {
            List<City> cities = new List<City>();
            for (int i = 0; i < 48; i++)
            {
                cities.Add(new City(CityData[i, 0], (ELEMENT)Convert.ToInt16(CityData[i, 2]), (REGION)Convert.ToInt16(CityData[i, 1]), new Point(Convert.ToDouble(CityData[i, 3]), Convert.ToDouble(CityData[i, 4]))));
            }
            for (int i = 0; i < 48; i++)
            {
                for (int j = 5; j < 11; j++)
                {
                    if (CityData[i, j] != "")
                    {
                        City adj = cities.FirstOrDefault(c => c.Name == CityData[i, j]);
                        if (adj == null)
                        {
                            throw new Exception();
                        }
                        cities[i].AddAdjacent(adj);
                    }
                }
            }
            return cities;
        }

        public static ObservableCollection<Path> GetPaths(ObservableCollection<City> cities)
        {
            ObservableCollection<Path> result = new ObservableCollection<Path>();
            List<City> Visited = new List<City>();
            foreach (City c1 in cities)
            {
                Visited.Add(c1);
                foreach (City c2 in c1.Adjacent)
                {
                    if (!Visited.Contains(c2))
                    {
                        result.Add(new Path(c1, c2));
                    }
                }
            }
            return result;
        }

        public static Deck BuildPlayerDeck(ObservableCollection<City> cities)
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            foreach (City city in cities)
            {
                cards.Add(new CityCard(city));
            }
            return new Deck(cards);
        }

        public static EpidemicCard GetEpidemicCard()
        {
            return new EpidemicCard();
        }

        internal static Deck BuildInfectionDeck(ObservableCollection<City> cities)
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            foreach (City city in cities)
            {
                cards.Add(new InfectionCard(city));
            }
            return new Deck(cards);
        }

        public static ObservableCollection<EventCard> GetAvailableEvents()
        {
            return new ObservableCollection<EventCard>() { new Airlift(),
                                                           new BorrowedTime(),
                                                           new Forecast(),
                                                           new GovernmentGrant(),
                                                           new NewAssignment(),
                                                           new OneQuietNight(),
                                                           new RemoteTreatment(),
                                                           new ResilientPopulation() };
        }
    }
}
