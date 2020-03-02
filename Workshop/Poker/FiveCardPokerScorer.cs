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

        private static bool checkCorner(this IEnumerable<Card> cards) => cards.Except(cards.Where(i => i.Value == CardValue.Ace)).All(c => c.Value < CardValue.Six)  && cards.GroupBy(i => i.Value).Select(g => g.Key).Contains(CardValue.Ace);

        private static bool AllSameSuit(IEnumerable<Card> cards) => cards.GroupBy(i => i.Suit).Count() == 1;//alternatively HasFlush acheives the same
        private static bool HasRoyalFlush(IEnumerable<Card> cards) => AllSameSuit(cards) && cards.All(c => c.Value > CardValue.Nine); //https://stackoverflow.com/questions/13359327/check-if-listint32-values-are-consecutive/13359693

              
private static bool HasStraightFlush(IEnumerable<Card> cards) => AllSameSuit(cards) &&  (cards.OrderBy(i => i.Value).Zip(cards.OrderBy(i => i.Value).Skip(1), (first, next) => first.Value + 1 == next.Value).All(i => i) || cards.checkCorner());
        private static bool HasFourOfAKind(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count()>=4).Count()!=0;
        private static bool HasFullHouse(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 3).Count() != 0 && cards.GroupBy(i => i.Value).Where(group => group.Count() == 2).Count() != 0;

        private static bool HasFlush(IEnumerable<Card> cards) => cards.All(c => cards.First().Suit == c.Suit);
        private static bool HasStraight(IEnumerable<Card> cards) => cards.OrderBy(i => i.Value).Zip(cards.OrderBy(i => i.Value).Skip(1), (first, next) => first.Value + 1 == next.Value).All(i => i) || cards.checkCorner();
        private static bool HasThreeOfAKind(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 3).Count() != 0;

        private static bool HasTwoPair(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 2).Count() ==2;

        private static bool HasPair(IEnumerable<Card> cards) => cards.GroupBy(i => i.Value).Where(group => group.Count() == 2).Count() == 1 && cards.GroupBy(i => i.Value).Where(group => group.Count() == 1).Select(i => i.Key).Distinct().Count() == 3 &&   cards.GroupBy(x => x.Value).Where(group => group.Count() == 2).Select(x => x.Key).Any(i => !cards.GroupBy(i => i.Value).Where(group => group.Count() == 1).Select(i => i.Key).Contains(i)); 
           
        public static Card HighCard(IEnumerable<Card> cards) =>
      cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);

        static public HandRank GetHandRank(IEnumerable<Card> cards)//remem to change to ?: style
        {
            var ranking = Rankings();
            return ranking.OrderByDescending(r => r.rank).Where(r => r.eval(cards) == true).Select(i => i.rank).First();
       
        }

        public static List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>    Rankings() =>//change to private
        new List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>

        {        (cards => HasRoyalFlush(cards) , HandRank.RoyalFlush ),
                (cards => HasStraightFlush(cards) , HandRank.StraightFlush ),
                (cards => HasStraight(cards) , HandRank.Straight ),
                (cards => HasFlush(cards) , HandRank.Flush ),
                (cards => HasFullHouse(cards) , HandRank.FullHouse) ,
                (cards => HasFourOfAKind(cards) , HandRank.FourOfAKind) ,
                (cards => HasThreeOfAKind(cards), HandRank.ThreeOfAKind ),
               (cards =>  HasTwoPair(cards) , HandRank.TwoPair) ,
               (cards => HasPair(cards) , HandRank.Pair ),
               (cards => true, HandRank.HighCard )
        };




    }
}
