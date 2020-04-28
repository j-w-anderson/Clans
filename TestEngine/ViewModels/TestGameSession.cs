
using Engine;
using Engine.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
    }
}
