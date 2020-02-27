using System;
using System.Collections.Generic;
using System.Text;
using static Poker.Card;
using static Poker.Hand;
using System.Linq;
namespace Poker
{


    public static class FiveCardPokerScorer
    {

        //  private static bool Ace2345(IEnumerable<Card> cards) => new List<CardValue> { CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five, CardValue.Ace}.All(i => cards.Any(x => i == x.Value));
        private static bool CheckSpecificCards(this List<CardValue> cards1, IEnumerable<Card> cards2) => cards1.All(i => cards2.Any(x => i == x.Value));

        private static bool AllSameSuit(IEnumerable<Card> cards) => cards.GroupBy(i => i.Suit).Count() == 1;//alternatively HasFlush acheives the same
       // private static bool Ten11121314(IEnumerable<Card> cards) => 
        private static bool HasRoyalFlush(IEnumerable<Card> cards) => AllSameSuit(cards) && new List<CardValue> { CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace }.CheckSpecificCards(cards); //https://stackoverflow.com/questions/13359327/check-if-listint32-values-are-consecutive/13359693

               //
private static bool HasStraightFlush(IEnumerable<Card> cards) => AllSameSuit(cards) &&  (cards.Zip(cards.Skip(1), (first, next) => first.Value + 1 == next.Value).All(i => i) || new List<CardValue> { CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five, CardValue.Ace }.CheckSpecificCards(cards)) ;
        private static bool HasFourOfAKind(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count()>=4).Count()!=0;
        private static bool HasFullHouse(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 3).Count() != 0 && cards.GroupBy(i => i.Value).Where(group => group.Count() == 2).Count() != 0;

        private static bool HasFlush(IEnumerable<Card> cards) => cards.All(c => cards.First().Suit == c.Suit);
        private static bool HasStraight(IEnumerable<Card> cards) => (cards.Zip(cards.Skip(1), (first, next) => first.Value + 1 == next.Value).All(i => i) || new List<CardValue> { CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five, CardValue.Ace }.CheckSpecificCards(cards));
        private static bool HasThreeOfAKind(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 3).Count() != 0;

        private static bool HasTwoPair(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 2).Count() ==2;

        private static bool HasPair(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 2).Count() == 1 && cards.GroupBy(i => i.Value).Where(group => group.Count() == 1).Select(i => i.Key).Distinct().Count() == 3 &&   cards.GroupBy(x => x.Value).Where(group => group.Count() == 2).Select(x => x.Key).Any(i => !cards.GroupBy(i => i.Value).Where(group => group.Count() == 1).Select(i => i.Key).Contains(i)); 
           
        public static Card HighCard(IEnumerable<Card> cards) =>
      cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);

        static public HandRank GetHandRank(IEnumerable<Card> cards)
        {
            if (HasRoyalFlush(cards))
                return HandRank.RoyalFlush;
            else if (HasStraightFlush(cards.OrderBy(i => i.Value)))
                return HandRank.StraightFlush;
            else if (HasFourOfAKind(cards))
                return HandRank.FourOfAKind;
            else if (HasFullHouse(cards))
                return HandRank.FullHouse;
            else if (HasFlush(cards))
                return HandRank.Flush;
            else if (HasStraight(cards.OrderBy(i => i.Value)))
                return HandRank.Straight;
            else if (HasThreeOfAKind(cards))
                return HandRank.ThreeOfAKind;
            else if (HasTwoPair(cards))
                return HandRank.TwoPair;
            else
                if (HasPair(cards))
                return HandRank.Pair;

                return HandRank.HighCard;
        }

        //static  public int CheckConsec(int stopper, List<CardValue> values)

        //  {
        //      values.Sort();
        //      int count = 1;
        //      int i = 0;

        //      while (count < stopper)
        //      {
        //          count = 1;

        //          while (i + 1 < values.Count() && (values[i] - values[i + 1]) == -1)

        //          {
        //              count++;
        //              i++;
        //              if (count >= stopper)
        //                  break;

        //          }

        //          if (i + 1 == values.Count())
        //          {

        //              break;
        //          }

        //          i++;

        //      }
        //      return count;
        //  }

        //static  public bool CheckAceFirstInSequenc(List<CardValue> values)
        //  {
        //      return (new List<CardValue> { CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five, CardValue.Ace }.All(x => values.Any(y => y == x)));

        //  }
        // static public List<int> StripIntVals(List<Card> Cards)
        //  {
        //      return Cards.Select(crd => (int)crd.Value).ToList();


        //  }

        //static  public HandRank GetHandRank(IEnumerable<Card> Cards)
        //  {
        //      if (Cards.Count() < 5)
        //      {
        //          throw new Exception("Cannot query HandRank for less than 5 cards");
        //      }

        //      List<CardValue> values = new List<CardValue>();

        //      bool fiveOfSameSuit = false;

        //      var summary = Cards.GroupBy(i => i.Suit, i => i.Value, (suitName, suitVal) => new
        //      {
        //          key = suitName,
        //          Count = suitVal.Count(),
        //          Min = suitVal.Min(),
        //          Max = suitVal.Max(),
        //          suitValue = suitVal
        //      });



        //      foreach (var suit in summary)
        //      {
        //          if (suit.Count >= 5)//has 5 of same suit
        //          {

        //              values = suit.suitValue.ToList();//collect card values as well
        //              fiveOfSameSuit = true;
        //              break;
        //          }
        //      }
        //      if (values.Count() == 0)
        //      { //didn't find  five of same suit
        //          foreach (var suit in summary)
        //          {

        //              values = values.Concat(suit.suitValue).ToList();

        //          }
        //      }

        //      if (new List<CardValue> { CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace }.All(x => values.Any(y => y == x)))
        //          return HandRank.RoyalFlush;



        //      int count = CheckConsec(5, (List<CardValue>)values);//maybe has consec of same suit


        //      if (fiveOfSameSuit && (count >= 5 || CheckAceFirstInSequenc((List<CardValue>)values)))

        //          return HandRank.StraightFlush;




        //      var allInGroups = values.GroupBy(x => x);    //maybe has four, full(3 & 2) of a kind?
        //      var fourOfAKind = allInGroups.Where(group => group.Count() >= 4).Select(group => group.Key).ToList();
        //      if (fourOfAKind.Count() > 0)

        //          return HandRank.FourOfAKind;

        //      var threeOfAKind = allInGroups.Where(group => group.Count() >= 3).Select(group => group.Key).ToList();

        //      var twoOfAKind = allInGroups.Where(group => group.Count() >= 2).Select(group => group.Key).Except(threeOfAKind).ToList();

        //      if (twoOfAKind.Count() > 0 && threeOfAKind.Count() > 0)

        //          return HandRank.FullHouse;



        //      if (fiveOfSameSuit)//any card

        //          return HandRank.Flush;


        //      if (count >= 5 || CheckAceFirstInSequenc((List<CardValue>)values))  //maybe consec of any suit
        //          return HandRank.Straight;

        //      if (threeOfAKind.Count() > 0)
        //          return HandRank.ThreeOfAKind;


        //      if (twoOfAKind.Count() == 2)


        //          return HandRank.TwoPair;

        //      var pair = twoOfAKind.Any(x => !values.Except(twoOfAKind).Contains(x)) && values.Except(twoOfAKind).Distinct().Count() == values.Except(twoOfAKind).Count();

        //      if (twoOfAKind.Count() > 0 && pair)

        //          return HandRank.Pair;

        //      return HandRank.HighCard;




        //  }
    }
}
