using System;
using System.Collections.Generic;
using System.Text;
using Poker;
using static Poker.Card;
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

 
        public int CheckConsec(int stopper, List<CardValue> values)
 
        {
            values.Sort();
            int count = 1;
            int i = 0;

            while (count < stopper)
            {
                count = 1;

                while (i + 1 < values.Count() && (values[i] - values[i + 1]) == -1)

                {
                    count++;
                    i++;
                    if (count >= stopper)
                        break;

                }

                if (i + 1 == values.Count())
                {

                    break;
                }

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
 
                    values = suit.suitValue.ToList();//collect card values as well
                    fiveOfSameSuit = true;
                    break;
                }
            }
            if (values.Count() == 0)
            { //didn't find  five of same suit
                foreach (var suit in summary)
                {

                    values = values.Concat(suit.suitValue).ToList();
                   
                }
            }

            if (new List<CardValue> { CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace }.All(x => values.Any(y => y == x)))
                return HandRank.RoyalFlush;



            int count = CheckConsec(5, (List<CardValue>)values);//maybe has consec of same suit


            if (fiveOfSameSuit && (count >= 5 || CheckAceFirstInSequenc((List<CardValue>)values)))

                return HandRank.StraightFlush;



          
            var allInGroups = values.GroupBy(x => x);    //maybe has four, full(3 & 2) of a kind?
            var fourOfAKind = allInGroups.Where(group => group.Count() >= 4).Select(group => group.Key).ToList();
            if (fourOfAKind.Count() > 0)

                return HandRank.FourOfAKind;

            var threeOfAKind = allInGroups.Where(group => group.Count() >= 3).Select(group => group.Key).ToList();

            var twoOfAKind = allInGroups.Where(group => group.Count() >= 2).Select(group => group.Key).Except(threeOfAKind).ToList();

            if (twoOfAKind.Count() > 0 && threeOfAKind.Count() > 0)

                return HandRank.FullHouse;

           

            if (fiveOfSameSuit)//any card

                return HandRank.Flush;

          
            if (count >= 5 || CheckAceFirstInSequenc((List<CardValue>)values))  //maybe consec of any suit
                return HandRank.Straight;

            if (threeOfAKind.Count() > 0)
                return HandRank.ThreeOfAKind;

             
            if (twoOfAKind.Count() == 2)


                return HandRank.TwoPair;

            var pair = twoOfAKind.Any(x => !values.Except(twoOfAKind).Contains(x)) && values.Except(twoOfAKind).Distinct().Count() == values.Except(twoOfAKind).Count();

            if (twoOfAKind.Count() > 0 && pair)

                return HandRank.Pair;

            return HandRank.HighCard;


 

        }


 
        public void Draw(Card card)
        {
            Cards.Add(card);
        }
    }
}
