using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StageTwoRefactor.Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void CanCreateCardWithValue()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);

            Assert.AreEqual(CardSuit.Spades, card.Suit);
            Assert.AreEqual(CardValue.Ace, card.Value);
        }

        [TestMethod]
        public void CanCreateDescribeCard()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);

            Assert.AreEqual("Ace of Spades", card.ToString());
        }
    }
}