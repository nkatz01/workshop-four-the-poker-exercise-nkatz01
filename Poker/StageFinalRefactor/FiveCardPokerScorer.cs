using System;
using System.Collections.Generic;
using System.Linq;

namespace StageFinalRefactor
{
    public class FiveCardPokerScorer
    {
        public static Card HighCard(IEnumerable<Card> cards)
        {
            return cards.Aggregate((highCard, nextCard) =>
                nextCard.Value > highCard.Value ? nextCard : highCard);
        }

        private static bool HasFlush(IEnumerable<Card> cards)
        {
            return cards.All(c => cards.First().Suit == c.Suit);
        }

        private static bool HasRoyalFlush(IEnumerable<Card> cards)
        {
            return HasFlush(cards) && cards.All(c => c.Value > CardValue.Nine);
        }

        private static bool HasOfAKind(IEnumerable<Card> cards, int num)
        {
            return cards.ToKindAndQuantities().Any(c => c.Value == num);
        }

        private static bool HasPair(IEnumerable<Card> cards)
        {
            return HasOfAKind(cards, 2);
        }

        private static bool HasThreeOfAKind(IEnumerable<Card> cards)
        {
            return HasOfAKind(cards, 3);
        }

        private static bool HasFourOfAKind(IEnumerable<Card> cards)
        {
            return HasOfAKind(cards, 4);
        }

        private static bool HasFullHouse(IEnumerable<Card> cards)
        {
            return HasThreeOfAKind(cards) && HasPair(cards);
        }

        private static bool HasStraight(IEnumerable<Card> cards)
        {
            return cards.OrderBy(card => card.Value)
                .SelectConsecutive((n, next) => n.Value + 1 == next.Value).All(value => value);
        }

        private static bool HasStraightFlush(IEnumerable<Card> cards)
        {
            return HasStraight(cards) && HasFlush(cards);
        }

        // A list of ranks gives added flexibility to how hand ranks can be scored.
        // Each ranker has an Eval delegate that returns a bool
        public static HandRank GetHandRank(IEnumerable<Card> cards)
        {
            return Rankings()
                .OrderByDescending(card => card.rank)
                .First(rule => rule.eval(cards)).rank;
        }

        private static List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)> Rankings()
        {
            return new List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>
            {
                (cards => HasRoyalFlush(cards), HandRank.RoyalFlush),
                (cards => HasStraightFlush(cards), HandRank.StraightFlush),
                (cards => HasFourOfAKind(cards), HandRank.FourOfAKind),
                (cards => HasFullHouse(cards), HandRank.FullHouse),
                (cards => HasFlush(cards), HandRank.Flush),
                (cards => HasStraight(cards), HandRank.Straight),
                (cards => HasThreeOfAKind(cards), HandRank.ThreeOfAKind),
                (cards => HasPair(cards), HandRank.Pair),
                (cards => true, HandRank.HighCard)
            };
        }
    }
}