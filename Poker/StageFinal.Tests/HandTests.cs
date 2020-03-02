using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StageFinal.Tests
{
    [TestClass]
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

        [TestMethod]
        public void CanGetHighCard()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));

            hand.HighCard().Value.Should().Be(CardValue.King);
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

            hand.GetHandRank().Should().Be(HandRank.HighCard);
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

            hand.GetHandRank().Should().Be(HandRank.Flush);
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

            hand.GetHandRank().Should().Be(HandRank.RoyalFlush);
        }


        [TestMethod]
        public void CanScorePair()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));

            hand.GetHandRank().Should().Be(HandRank.Pair);
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

            hand.GetHandRank().Should().Be(HandRank.ThreeOfAKind);
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

            hand.GetHandRank().Should().Be(HandRank.FourOfAKind);
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

            hand.GetHandRank().Should().Be(HandRank.FullHouse);
        }

        [TestMethod]
        public void CanScoreStraight()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));

            hand.GetHandRank().Should().Be(HandRank.Straight);
        }

        [TestMethod]
        public void CanScoreStraightUnordered()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Queen, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));

            hand.GetHandRank().Should().Be(HandRank.Straight);
        }

        [TestMethod]
        public void CanScoreStraightFlush()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Eight, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Six, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));

            hand.GetHandRank().Should().Be(HandRank.StraightFlush);
        }
    }
}