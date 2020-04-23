using Engine;
using Engine.Model;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PandemicLegacy
{
    static class GameData
    {
        static string[,] RegionData = new string[,] {{"14","22","A","FOREST"},
                                                    {"4","54","B","FOREST"},
                                                    {"5","94","C","FOREST"},
                                                    {"45","17","D","FOREST"},
                                                    {"31","36","E","FOREST"},
                                                    {"21","72","F","FOREST"},
                                                    {"36","95","G","FOREST"},
                                                    {"57","88","G","FOREST"},
                                                    {"47","40","H","FOREST"},
                                                    {"46","68","I","FOREST"},
                                                    {"62","55","I","FOREST"},
                                                    {"74","86","J","FOREST"},
                                                    {"66","5","K","FOREST"},
                                                    {"80","22","K","FOREST"},
                                                    {"75","65","L","FOREST"},
                                                    {"10","13","A","GRASSLAND"},
                                                    {"8","34","B","GRASSLAND"},
                                                    {"4","75","C","GRASSLAND"},
                                                    {"16","93","C","GRASSLAND"},
                                                    {"26","10","D","GRASSLAND"},
                                                    {"47","7","D","GRASSLAND"},
                                                    {"38","28","E","GRASSLAND"},
                                                    {"24","45","E","GRASSLAND"},
                                                    {"36","55","F","GRASSLAND"},
                                                    {"43","84","G","GRASSLAND"},
                                                    {"63","26","H","GRASSLAND"},
                                                    {"54","61","I","GRASSLAND"},
                                                    {"64","80","J","GRASSLAND"},
                                                    {"79","6","K","GRASSLAND"},
                                                    {"80","46","L","GRASSLAND"},
                                                    {"17","6","A","MOUNTAIN"},
                                                    {"5","24","A","MOUNTAIN"},
                                                    {"14","48","B","MOUNTAIN"},
                                                    {"14","75","C","MOUNTAIN"},
                                                    {"57","10","D","MOUNTAIN"},
                                                    {"36","43","E","MOUNTAIN"},
                                                    {"29","67","F","MOUNTAIN"},
                                                    {"49","95","G","MOUNTAIN"},
                                                    {"49","28","H","MOUNTAIN"},
                                                    {"63","44","H","MOUNTAIN"},
                                                    {"60","68","I","MOUNTAIN"},
                                                    {"81","94","J","MOUNTAIN"},
                                                    {"73","13","K","MOUNTAIN"},
                                                    {"78","33","L","MOUNTAIN"},
                                                    {"79","56","L","MOUNTAIN"},
                                                    {"5","5","A","STEPPE"},
                                                    {"5","43","B","STEPPE"},
                                                    {"6","63","B","STEPPE"},
                                                    {"9","85","C","STEPPE"},
                                                    {"36","6","D","STEPPE"},
                                                    {"23","28","E","STEPPE"},
                                                    {"25","59","F","STEPPE"},
                                                    {"37","72","F","STEPPE"},
                                                    {"25","90","G","STEPPE"},
                                                    {"56","35","H","STEPPE"},
                                                    {"48","55","I","STEPPE"},
                                                    {"66","94","J","STEPPE"},
                                                    {"78","76","J","STEPPE"},
                                                    {"69","20","K","STEPPE"},
                                                    {"71","46","L","STEPPE"} };

        static double[,] ScoreTrack = new double[,] {{85.8,-92.6},
                                                    {85.8,-88.5272727272727},
                                                    {85.8,-84.4545454545455},
                                                    {85.8,-80.3818181818182},
                                                    {85.8,-76.3090909090909},
                                                    {85.8,-72.2363636363636},
                                                    {85.8,-68.1636363636364},
                                                    {85.8,-64.0909090909091},
                                                    {85.8,-60.0181818181818},
                                                    {85.8,-55.9454545454545},
                                                    {85.8,-51.8727272727273},
                                                    {85.8,-47.8},
                                                    {85.8,-43.7272727272727},
                                                    {85.8,-39.6545454545455},
                                                    {85.8,-35.5818181818182},
                                                    {85.8,-31.5090909090909},
                                                    {85.8,-27.4363636363636},
                                                    {85.8,-23.3636363636364},
                                                    {85.8,-19.2909090909091},
                                                    {85.8,-15.2181818181818},
                                                    {85.8,-11.1454545454546},
                                                    {85.8,-7.07272727272728},
                                                    {85.8,-3},
                                                    {88.28,-3},
                                                    {90.76,-3},
                                                    {93.24,-3},
                                                    {95.72,-3},
                                                    {98.2,-3},
                                                    {98.2,-7.07272727272727},
                                                    {98.2,-11.1454545454545},
                                                    {98.2,-15.2181818181818},
                                                    {98.2,-19.2909090909091},
                                                    {98.2,-23.3636363636364},
                                                    {98.2,-27.4363636363636},
                                                    {98.2,-31.5090909090909},
                                                    {98.2,-35.5818181818182},
                                                    {98.2,-39.6545454545454},
                                                    {98.2,-43.7272727272727},
                                                    {98.2,-47.8},
                                                    {98.2,-51.8727272727273},
                                                    {98.2,-55.9454545454545},
                                                    {98.2,-60.0181818181818},
                                                    {98.2,-64.0909090909091},
                                                    {98.2,-68.1636363636364},
                                                    {98.2,-72.2363636363636},
                                                    {98.2,-76.3090909090909},
                                                    {98.2,-80.3818181818182},
                                                    {98.2,-84.4545454545454},
                                                    {98.2,-88.5272727272727},
                                                    {98.2,-92.6}};

        static int[,] EpochTrack = new int[,]  {{1,0,92,-11},
                                                {1,1,92,-17},
                                                {1,2,92,-22},
                                                {1,3,92,-27},
                                                {2,0,92,-36},
                                                {2,1,92,-42},
                                                {2,2,92,-47},
                                                {3,0,92,-57},
                                                {3,1,92,-63},
                                                {4,0,92,-75},
                                                {4,1,92,-81},
                                                {5,0,92,-92}};

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

        public static Deck BuildResourceDeck()
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            List<RESOURCE> resources = new List<RESOURCE> { RESOURCE.TEA, RESOURCE.CHILI, RESOURCE.INDIGO, RESOURCE.PEPPER, RESOURCE.SAFFRON };
            List<int> values = new List<int> { 0, 1, 2, 3, 4, 5, 5 };
            foreach (RESOURCE r in resources)
            {
                foreach (int v in values)
                {
                    cards.Add(new Card(r, v));
                }
            }
            cards.Add(new Card(RESOURCE.GOLD, 10));

            return new Deck(cards);
        }

        public static List<int> BurnCount = new List<int> { 18, 18, 12, 6, 0 };

        public static int CardsToBurn(int nplayers)
        {
            return BurnCount[nplayers - 2];
        }


        static int[,] HoldRewards = new int[,] {{20, 0, 0, 0, 0, 0 },
                                                {30,15, 0, 0, 0, 0 },
                                                {30,20,10, 0, 0, 0 },
                                                {30,20,10, 5, 0, 0 },
                                                {30,20,15,10, 5, 0 } };

        public static int GetHoldReward(int rank, int nplayers)
        {
            return HoldRewards[nplayers - 2, rank];
        }

        static int[] TrackRewards = new int[] { 10, 5 };

        public static int GetTrackReward(int rank, int nplayers)
        {
            if (rank == 0) { return 10; }
            if (rank == 1 && nplayers != 2) { return 5; }
            return 0;
        }

        public static int GetTrackBonus(int level)
        {
            switch (level)
            {
                case 0:
                    return 20;
                case 1:
                    return 10;
                case 2:
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
