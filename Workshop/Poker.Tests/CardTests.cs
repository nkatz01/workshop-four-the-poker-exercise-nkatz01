using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using static Poker.Card;

namespace Poker.Tests
{
	 
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void CanCreateCardWithValue()
        {
           var card = new Card(CardValue.Ace, CardSuit.Clubs);

          
            Assert.IsNotNull(card);
            Assert.AreEqual(CardSuit.Clubs, card.Suit);
            Assert.AreEqual(CardValue.Ace, card.Value);
        }


        [TestMethod]
        public void CanDescribeCard()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);
            Assert.AreEqual("Ace of Spades", card.ToString());
        }

      



    }
}