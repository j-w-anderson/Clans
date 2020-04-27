
using Engine;
using Engine.Model;
using Engine.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestRegion
    {
        [TestMethod]
        public void TestEmpty()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            gameSession.Regions[0].Huts = new ObservableCollection<int>() { 0, 0, 0, 0, 0 };
            Assert.IsTrue(gameSession.Regions[0].Empty);
        }

        [TestMethod]
        public void TestDestinations()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            CollectionAssert.AreEquivalent(gameSession.Regions[1].Adj, gameSession.Regions[1].GetDestinations());
        }

        [TestMethod]
        public void OriginTest1()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            Assert.IsTrue(gameSession.Regions[2].IsValidOrigin());
        }

        [TestMethod]
        public void OriginTest2()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            gameSession.Regions[3].Huts = new ObservableCollection<int>() { 0, 0, 0, 0, 0 };
            Assert.IsFalse(gameSession.Regions[3].IsValidOrigin());
        }

        [TestMethod]
        public void OriginTest3()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            gameSession.Regions[4].Huts = new ObservableCollection<int>() { 2, 2, 2, 2, 2 };
            Assert.IsFalse(gameSession.Regions[4].IsValidOrigin());
        }


        [TestMethod]
        public void OriginTest4()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            gameSession.Regions[5].Huts = new ObservableCollection<int>() { 2, 2, 2, 2, 2 };
            gameSession.Regions[5].Adj[0].Huts = new ObservableCollection<int>() { 2, 2, 2, 2, 2 };
            Assert.IsTrue(gameSession.Regions[4].IsValidOrigin());
        }

        [TestMethod]
        public void OriginTest5()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            gameSession.Regions[6].Village = true;
            Assert.IsFalse(gameSession.Regions[6].IsValidOrigin());
        }


        [TestMethod]
        public void MoveTest1()
        {
            GameSession gameSession = new GameSession();
            gameSession.DistributeHuts(new Random());
            Region origin = gameSession.Regions[7];
            Region destination = gameSession.Regions[7].Adj[0];
            origin.Huts = new ObservableCollection<int>() { 1, 0, 1, 0, 1 };
            destination.Huts = new ObservableCollection<int>() { 0, 1, 0, 1, 0 };
            origin.MoveTo(gameSession.Regions[7].Adj[0]);
            Assert.IsTrue(origin.Empty);
            CollectionAssert.AreEqual(new ObservableCollection<int>() { 1, 1, 1, 1, 1 }, destination.Huts);
        }


    }
}
