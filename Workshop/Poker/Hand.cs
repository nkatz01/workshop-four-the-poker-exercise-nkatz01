using System;
using System.Collections.Generic;
using System.Text;
using Poker;
using static Poker.Card;
using System.Linq;

namespace Poker
 {
 
    public class Hand
    {
        public enum HandRank
        {
            HighCard,
            Pair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        public List<Card> Cards { get; }
        public Hand()
        {
            Cards = new List<Card>();
        }

        //public Card HighCard()
        //{

        //    int HighestValue = (int)Cards.Max(i => i.Value);
        //    Card card = Cards.First(obj => (int)obj.Value == HighestValue);
        //    return card;
        //}

 
       


 
        public void Draw(Card card)
        {
            Cards.Add(card);
        }
    }
}
