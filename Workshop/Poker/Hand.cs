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
        public int CheckConsec(int stopper, List<int> values)
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

        public bool CheckAceFirstInSequenc(List<int> values)
        {
          return  (new List<int> { 2, 3, 4, 5, (int)CardValue.Ace }.All(x => values.Any(y => y == x)));

        }
        public List<int> StripIntVals(List<Card> Cards)
        {
            return Cards.Select(crd => (int)crd.Value).ToList();


        }

        public void GetHandRank()
        {
            if (Cards.Count() < 5)
            {
                throw new Exception("Cannot query HandRank for less than 5 cards");
            }
            List<int> values  ;

            //IEnumerable<Card> filtered = Cards.FindAll(i => i.Suit == CardSuit.Spades && !(i.Suit == CardSuit.Clubs || i.Suit == CardSuit.Hearts  || i.Suit == CardSuit.Diamonds)
            //|| (i.Suit == CardSuit.Hearts && !(i.Suit == CardSuit.Spades || i.Suit == CardSuit.Clubs || i.Suit == CardSuit.Diamonds))
            //|| (i.Suit == CardSuit.Clubs && !(i.Suit == CardSuit.Spades || i.Suit == CardSuit.Hearts || i.Suit == CardSuit.Diamonds))
            //|| (i.Suit == CardSuit.Diamonds && !(i.Suit == CardSuit.Spades || i.Suit == CardSuit.Hearts || i.Suit == CardSuit.Clubs))
            //);
          var query = Cards.GroupBy(i =>  i.Suit, i => (int)i.Value );
            foreach (IGrouping<CardSuit, int> g in query)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine(g.Key);
                // Iterate over each value in the 
                // IGrouping and print the value.
                foreach (int v in g)
                    Console.WriteLine("  {0}", v);
            }
            // Console.WriteLine(filtered.Count());
            ////foreach(var v in filtered)
            ////{
            ////    Console.WriteLine(v);
            ////}

            //if (filtered.Count() >= 5)
            //{
            //    values = StripIntVals(filtered.ToList());
            //    if (values.Contains(10) && values.Contains(11) && values.Contains(12) && values.Contains(13) && values.Contains(14))

            //        return HandRank.RoyalFlush;


            //    else
            //    {

            //        int count = CheckConsec(5, values);


            //        if (count >= 5 || CheckAceFirstInSequenc(values))
            //        {




            //            return HandRank.StraightFlush;
            //        }





            //        else
            //        {
            //            Console.WriteLine(CheckAceFirstInSequenc(values));
            //            return HandRank.Flush;
            //        }

            //    }
            //}

            //else
            //{
            //    values = StripIntVals(Cards);
            //    int count = CheckConsec(5, values);


            //    if (count >= 5 || CheckAceFirstInSequenc(values))
            //    {

            //        return HandRank.Straight;
            //    }
            //    else {
            //        return HandRank.HighCard;
            //    }
            //}




        }

       

        public void Draw(Card card)
        {
            Cards.Add(card);
        }
    }
}
