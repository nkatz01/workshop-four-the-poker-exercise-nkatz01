using System.Collections.Generic;
using System.Linq;

namespace StageFinal
{
    public class Hand
    {
        private readonly List<Card> _cards = new List<Card>();
        public IEnumerable<Card> Cards => _cards;

        public void Draw(Card card)
        {
            _cards.Add(card);
        }

        public Card HighCard()
        {
            return _cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);
        }

        private bool HasFlush()
        {
            return _cards.All(c => _cards.First().Suit == c.Suit);
        }

        public bool HasRoyalFlush()
        {
            return HasFlush() && _cards.All(c => c.Value > CardValue.Nine);
        }

        private bool HasOfAKind(int num)
        {
            return _cards.ToKindAndQuantities().Any(c => c.Value == num);
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

        private bool HasStraight()
        {
            return _cards.OrderBy(card => card.Value)
                .SelectConsecutive((n, next) => n.Value + 1 == next.Value).All(value => value);
        }

        private bool HasStraightFlush()
        {
            return HasStraight() && HasFlush();
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
    }
}