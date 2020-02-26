using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static Poker.Card;
using static Poker.Hand;
namespace Poker.Tests
{
    [TestClass]
   public class HandTests
    {
//change a
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
            Assert.AreEqual(hand.Cards.First(), card);
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
            Assert.AreEqual(CardValue.King, hand.HighCard().Value);
        }

        //[TestMethod]
        //public void CanScoreHighCard()
        //{
        //    var hand = new Hand();
        //    //hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        //    //    hand.Draw(new Card(CardValue.Five,  CardSuit.Hearts));
        //    //    hand.Draw(new Card(CardValue.King,  CardSuit.Hearts));
        //    //    hand.Draw(new Card(CardValue.Two,   CardSuit.Hearts));
        //    Assert.AreEqual(HandRank.HighCard, hand.GetHandRank());
        //}

        //[TestMethod]
        //public void CanScoreFlush()
        //{
        //    var hand = new Hand();
        //    hand.Draw(new Card(CardValue.Two, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Six, CardSuit.Spades));
        //    Assert.AreEqual(HandRank.Flush, hand.GetHandRank());
        //}

        //[TestMethod]
        //public void CanScoreRoyalFlush()
        //{
        //    var hand = new Hand();
        //    hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.King, CardSuit.Spades));
        //    hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
        //    Assert.AreEqual(HandRank.RoyalFlush, hand.GetHandRank());
        //}



    }
}
