using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace StageThree
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

        // The Any LINQ method validates that there are dictionary items with a specified pair count value.
        private bool HasOfAKind(int num)
        {
            return GetKindAndQuantities(_cards).Any(c => c.Value == num);
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

        /* Since a collection of pairs needs to be built, a mutable collection must be temporarily created.
         Because the scope is limited, risk for side-effect is minimal. In addition, the ConcurrentDictionary's AddOrUpdate method is Thread Safe.
         While the ConcurrentDictionary mutates state via AddOrUpdate, it does use a functional principal called a higher order function.
         Higer order functions are simply a function that accepts or returns another function. 
         The AddOrUpdate method accepts a function which delegates how the item is updated.
        */
        private IEnumerable<KeyValuePair<CardValue, int>> GetKindAndQuantities(IEnumerable<Card> cards)
        {
            var dict = new ConcurrentDictionary<CardValue, int>();
            foreach (var card in cards)
                // Add the value to the dictionary, or increase the count
                dict.AddOrUpdate(card.Value, 1, (cardValue, quantity) => ++quantity);

            return dict;
        }
    }
}