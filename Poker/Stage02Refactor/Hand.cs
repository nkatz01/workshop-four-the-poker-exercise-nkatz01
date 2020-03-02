using System.Collections.Generic;
using System.Linq;

namespace StageTwoRefactor
{
    public class Hand
    {
        private readonly List<Card> _cards = new List<Card>();

        // simplified to an expression-bodied member
        public IEnumerable<Card> Cards => _cards;

        // simplified to an expression-bodied member
        public void Draw(Card card)
        {
            _cards.Add(card);
        }

        // simplified to an expression-bodied member
        public Card HighCard()
        {
            return _cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);
        }

        // Optional
        // The return early pattern can be replaced with tenary operators
        // then shortended to an expression-bodied member
        public HandRank GetHandRank()
        {
            return HasRoyalFlush() ? HandRank.RoyalFlush :
                HasFlush() ? HandRank.Flush :
                HandRank.HighCard;
        }

        // simplified to an expression-bodied member
        private bool HasFlush()
        {
            return _cards.All(c => _cards.First().Suit == c.Suit);
        }

        // simplified to an expression-bodied member
        public bool HasRoyalFlush()
        {
            return HasFlush() && _cards.All(c => c.Value > CardValue.Nine);
        }
    }
}