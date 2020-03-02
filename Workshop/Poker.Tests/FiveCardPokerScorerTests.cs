using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static Poker.Card;
using static Poker.Hand;
using static Poker.FiveCardPokerScorer;
using System.Linq;
using FluentAssertions;

namespace Poker.Tests
{
    [TestClass]
  public  class FiveCardPokerScorerTests
    {

        [TestMethod]
        public void CanGetHighCard()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
            //Assert.AreEqual(CardValue.King, hand.HighCard().Value);
            HighCard(hand.Cards).Value.Should().Be(CardValue.King);
        }

        [TestMethod]
        public void CanScoreHighCard()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
            GetHandRank(hand.Cards).Should().Be(HandRank.HighCard);
        }

        [TestMethod]
        public void CanScoreFlush()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Two, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Six, CardSuit.Spades));
            //Assert.AreEqual(HandRank.Flush, hand.GetHandRank());
            GetHandRank(hand.Cards).Should().Be(HandRank.Flush);
        }

        [TestMethod]
        public void CanScoreStraightFlush()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Two, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Four, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
            //  Assert.AreEqual(HandRank.StraightFlush, hand.GetHandRank());
            GetHandRank(hand.Cards).Should().Be(HandRank.StraightFlush);

        }



        [TestMethod]
        public void CanScoreRoyalFlush()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
            hand.Draw(new Card(CardValue.King, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            // Assert.AreEqual(HandRank.RoyalFlush, hand.GetHandRank());
            GetHandRank(hand.Cards).Should().Be(HandRank.RoyalFlush);
        }

        [TestMethod]
        public void CanScoreStraight()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Four, CardSuit.Diamonds));
            hand.Draw(new Card(CardValue.Five, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            // Assert.AreEqual(HandRank.RoyalFlush, hand.GetHandRank());
            GetHandRank(hand.Cards).Should().Be(HandRank.Straight);
        }



        [TestMethod]
        public void CanScorePair()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            GetHandRank(hand.Cards).Should().Be(HandRank.Pair);
        }

        [TestMethod]
        public void CanScoreTwoPair()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            GetHandRank(hand.Cards).Should().Be(HandRank.TwoPair);
        }



        [TestMethod]
        public void CanScoreThreeOfAKind()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            GetHandRank(hand.Cards).Should().Be(HandRank.ThreeOfAKind);
        }

        [TestMethod]
        public void CanScoreFourOfAKind()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            GetHandRank(hand.Cards).Should().Be(HandRank.FourOfAKind);
        }

        [TestMethod]
        public void CanScoreFullHouse()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            GetHandRank(hand.Cards).Should().Be(HandRank.FullHouse);
        }

    }
}
