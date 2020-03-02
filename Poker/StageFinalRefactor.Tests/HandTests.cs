using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StageFinalRefactor.Tests
{
    public class HandTests
    {
        [TestMethod]
        public void CanCreateHand()
        {
            var hand = new Hand();
            hand.Cards.Any().Should().BeFalse();
        }

        [TestMethod]
        public void CanHandDrawCard()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);
            var hand = new Hand();

            hand.Draw(card);
            hand.Cards.First().Should().Be(card);
        }
    }
}