
using Engine;
using Engine.Model;
using Engine.Utils;
using Engine.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void InitialDistributionTest()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            Assert.IsTrue(gameSession.Regions.All(r => r.Huts.Sum() == 1));
        }

        [TestMethod]
        public void IntialChipTest()
        {
            GameSession gameSession = new GameSession();
            Assert.AreEqual(12, gameSession.Chips);
        }

        [TestMethod]
        public void IntialScoreTest()
        {
            GameSession gameSession = new GameSession();
            Assert.IsTrue(gameSession.Players.All(p=>p.Points==0));
        }

        [TestMethod]
        public void HiddenClanTest()
        {
            GameSession gameSession = new GameSession();
            gameSession.StartGame(new Random());
            CollectionAssert.AllItemsAreUnique(gameSession.Players.Select(p => p.HiddenClan).ToList<Clan>());
        }

        [TestMethod]
        public void SelectableRegions()
        {
            GameSession gameSession = new GameSession();
            gameSession.StartGame(new Random());
            Assert.IsTrue(gameSession.Regions.All(r => r.Selectable == true));
        }

        [TestMethod]
        public void DeelectedRegions()
        {
            GameSession gameSession = new GameSession();
            gameSession.StartGame(new Random());
            gameSession.CurrentMode.DeselectRegions();
            Assert.IsTrue(gameSession.Regions.All(r => r.Selectable == false));
        }
        [TestMethod]
        public void VillageTest1_Single()
        {
            GameSession gameSession = new GameSession();
            gameSession.Regions[11].AddHut(gameSession.Clans[0]);
            CollectionAssert.AreEquivalent(new ObservableCollection<Region>() { gameSession.Regions[11] },
                                           gameSession.FindNewVillages());
        }

        [TestMethod]
        public void VillageTest2_Create2()
        {
            //Regions 11,12,13 form a line
            GameSession gameSession = new GameSession();
            ObservableCollection<Region> regions = gameSession.Regions;
            regions[11].AddHut(gameSession.Clans[1]);
            regions[12].AddHut(gameSession.Clans[2]);
            regions[13].AddHut(gameSession.Clans[3]);
            regions[12].MoveTo(regions[13]);
            CollectionAssert.AreEquivalent(new ObservableCollection<Region>() { regions[11], regions[13] },
                                           gameSession.FindNewVillages());
        }
        [TestMethod]
        public void VillageTest3_FullBoard()
        {
            //Regions 20,22,23,24 completely surround region 21
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            ObservableCollection<Region> regions = gameSession.Regions;
            regions[20].MoveTo(regions[21]);
            regions[22].MoveTo(regions[21]);
            regions[23].MoveTo(regions[21]);
            regions[24].MoveTo(regions[21]);
            CollectionAssert.AreEquivalent(new ObservableCollection<Region>() { regions[21] },
                                           gameSession.FindNewVillages());
        }
        
        [TestMethod]
        public void VilliageSelectibilityTest()
        {
            GameSession gameSession = new GameSession();
            gameSession.StartGame(new Random());
           
            ObservableCollection<Region> newVillages = new ObservableCollection<Region>() { gameSession.Regions[20],
                                                                                            gameSession.Regions[23],
                                                                                            gameSession.Regions[27]};
            gameSession.CurrentMode = new SelectVillage(gameSession, newVillages);
            CollectionAssert.AreEquivalent(newVillages, 
                                           gameSession.Regions.Where(r => r.Selectable).ToObservableCollection<Region>());
        }
    }
}
