using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static Poker.Card;
using static Poker.Hand;
using FluentAssertions;
namespace Poker.Tests
{
    [TestClass]
   public class HandTests
    {

        [TestMethod]
        public void CanCreateHand()
        {
            var hand = new Hand();
            Assert.IsTrue(hand.Cards.Count == 0);

        }
        [TestMethod]
        public void CanHandDrawCard()
        {

            var card = new Card(Card.CardValue.Ace, Card.CardSuit.Spades);
            var hand = new Hand();
            hand.Draw(card);
             hand.Cards.First().Should().Be(card);
        }

   



    

    }
}
