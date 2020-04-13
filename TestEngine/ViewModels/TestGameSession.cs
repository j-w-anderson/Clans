
using Engine;
using Engine.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void DeckSizeTest()
        {
            GameSession gameSession = new GameSession();
            Assert.AreEqual(36,gameSession.ResourceDeck.DrawPile.Count);
        }

        [TestMethod]
        public void BurnTest()
        { 
            GameSession gameSession = new GameSession();
            gameSession.ResourceDeck.Burn(10);
            Assert.AreEqual(26,gameSession.ResourceDeck.DrawPile.Count);
            Assert.AreEqual(gameSession.ResourceDeck.BurnPile.Count, 10);
        }

        [TestMethod]
        public void BurnThenShuffleTest()
        {
            GameSession gameSession = new GameSession();
            gameSession.ResourceDeck.Burn(10);
            gameSession.ResourceDeck.Shuffle();
            Assert.AreEqual(36,gameSession.ResourceDeck.DrawPile.Count);
            Assert.AreEqual(0,gameSession.ResourceDeck.BurnPile.Count);
        }
    }
}
