using System.Collections.Generic;
using System.Linq;

namespace StageThreeRefactor
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

        // The ToPairs extension method maps a collection of cards, to a collection of pairs.
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
    }
}