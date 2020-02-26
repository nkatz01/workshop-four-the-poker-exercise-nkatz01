using System;
using System.Collections.Generic;
using System.Text;
using Poker;
using static Poker.Card;
using System.Linq;

namespace Poker
{//change c
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

        public Card HighCard()
        {

            int HighestValue = (int)Cards.Max(i => i.Value);
            Card card = Cards.First(obj => (int)obj.Value == HighestValue);
            return card;
        }

        //public int CompareTo(Card other) { return other.Value.CompareTo(other.Value); }
        public int CheckConsec(int stopper, List<CardValue> values)
        {
            values.Sort();
            int count = 0;
            int i = 0;

            while (count < stopper)
            {
                count = 0;

                while (i + 1 < values.Count() && (values[i] - values[i + 1]) == -1)

                {
                    count++;
                    i++;
                    if (count >= stopper)
                        break;

                }

                if (i + 1 == values.Count())
                    break;
                i++;

            }
            return count;
        }

        public bool CheckAceFirstInSequenc(List<CardValue> values)
        {
            return (new List<CardValue> { CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five, CardValue.Ace }.All(x => values.Any(y => y == x)));

        }
        public List<int> StripIntVals(List<Card> Cards)
        {
            return Cards.Select(crd => (int)crd.Value).ToList();


        }

        public HandRank GetHandRank()
        {
            if (Cards.Count() < 5)
            {
                throw new Exception("Cannot query HandRank for less than 5 cards");
            }

            List<CardValue> values = new List<CardValue>();
            bool fiveOfSameSuit = false;

            var summary = Cards.GroupBy(i => i.Suit, i => i.Value, (suitName, suitVal) => new
            {
                key = suitName,
                Count = suitVal.Count(),
                Min = suitVal.Min(),
                Max = suitVal.Max(),
                suitValue = suitVal
            });



            foreach (var suit in summary)
            {
                if (suit.Count >= 5)//has 5 of same suit
                {
                    fiveOfSameSuit = true;
                    values = suit.suitValue.ToList();
                    break;
                }

            }

            if (fiveOfSameSuit)
            {

                if (new List<CardValue> { CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace }.All(x => values.Any(y => y == x)))
                    return HandRank.RoyalFlush;

                else
                {

                    int count = CheckConsec(5, (List<CardValue>)values);


                    if (count >= 5 || CheckAceFirstInSequenc((List<CardValue>)values))


                        return HandRank.StraightFlush;

                    else

                        return HandRank.Flush;


                }
            }
            else
                return HandRank.HighCard;

        }



        public void Draw(Card card)
        {
            Cards.Add(card);
        }
    }
}
