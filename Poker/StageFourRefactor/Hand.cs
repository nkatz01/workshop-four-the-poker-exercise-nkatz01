﻿using System.Collections.Generic;
using System.Linq;

namespace StageFourRefactor
{
    public class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        public IEnumerable<Card> Cards => cards;

        public void Draw(Card card)
        {
            cards.Add(card);
        }

        public Card HighCard()
        {
            return cards.Aggregate((result, nextCard) => result.Value > nextCard.Value ? result : nextCard);
        }

        public HandRank GetHandRank()
        {
            return HasRoyalFlush() ? HandRank.RoyalFlush :
                HasStraightFlush() ? HandRank.StraightFlush :
                HasStraight() ? HandRank.Straight :
                HasFlush() ? HandRank.Flush :
                HasFullHouse() ? HandRank.FullHouse :
                HasFourOfAKind() ? HandRank.FourOfAKind :
                HasThreeOfAKind() ? HandRank.ThreeOfAKind :
                HasPair() ? HandRank.Pair :
                HandRank.HighCard;
        }

        private bool HasFlush()
        {
            return cards.All(c => cards.First().Suit == c.Suit);
        }

        private bool HasRoyalFlush()
        {
            return HasFlush() && cards.All(c => c.Value > CardValue.Nine);
        }

        private bool HasOfAKind(int num)
        {
            return cards.ToKindAndQuantities().Any(c => c.Value == num);
        }

        private bool HasPair()
        {
            return HasOfAKind(2);
        }

        private bool HasThreeOfAKind()
        {
            return HasOfAKind(3);
        }

        private bool HasFourOfAKind()
        {
            return HasOfAKind(4);
        }

        private bool HasFullHouse()
        {
            return HasThreeOfAKind() && HasPair();
        }

        // The Zip and Skip LINQ methods are replaced by a custom extension method, SelectConsecutive
        // Select consecutive works like LINQ select, except it can evaluate two consecutive items in an collection
        // This is done using a yield keyword, the source code is in EvalExtensions.cs
        private bool HasStraight()
        {
            return cards.OrderBy(card => card.Value).SelectConsecutive((n, next) => n.Value + 1 == next.Value)
                .All(value => value);
        }

        private bool HasStraightFlush()
        {
            return HasStraight() && HasFlush();
        }
    }
}