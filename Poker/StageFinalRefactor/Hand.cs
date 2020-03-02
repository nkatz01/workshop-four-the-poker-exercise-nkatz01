using System.Collections.Generic;

namespace StageFinalRefactor
{
    public class Hand
    {
        private readonly List<Card> _cards = new List<Card>();

        public IEnumerable<Card> Cards => _cards;

        public void Draw(Card card)
        {
            _cards.Add(card);
        }
    }
}