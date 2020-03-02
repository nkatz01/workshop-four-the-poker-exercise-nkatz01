using System;
using System.Collections.Generic;
using System.Text;
using Poker;
using static Poker.Card;
using static Poker.FiveCardPokerScorer;
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

        //

        //public List<Card> Cards { get; }

        private readonly List<Card> _cards = new List<Card>();
        public IEnumerable<Card> Cards => _cards;
        //public Hand()
        //{
        //    Cards = new List<Card>();
        //}

        //public Card HighCard()
        //{

        //    int HighestValue = (int)Cards.Max(i => i.Value);
        //    Card card = Cards.First(obj => (int)obj.Value == HighestValue);
        //    return card;
        //}

        //private List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>   Rankings() =>  new List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>
        //       {GetHandRank(Cards), 

        //};




        public void Draw(Card card)
        {
            //Cards.Add(card);
            _cards.Add(card);
        }
    }
}
