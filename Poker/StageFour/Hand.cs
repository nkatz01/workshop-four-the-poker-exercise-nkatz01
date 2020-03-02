using System.Collections.Generic;
using System.Linq;

namespace StageFour
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
            return _cards.Aggregate((result, nextCard) => result.Value > nextCard.Value ? result : nextCard);
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
            return _cards.All(c => _cards.First().Suit == c.Suit);
        }

        private bool HasRoyalFlush()
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

        // The Zip LINQ method operates on two collections at once
        // The second instance is offset by one, n + 1 is comapred with the next value in the offset collection.
        // If all evaluate to True, the collection is a straight
        private bool HasStraight()
        {
            return _cards.OrderBy(card => card.Value)
                .Zip(_cards.OrderBy(card => card.Value).Skip(1), (n, next) => n.Value + 1 == next.Value)
                .All(value => value /* true */);
        }

        private bool HasStraightFlush()
        {
            return HasStraight() && HasFlush();
        }
    }
}