using System;
using static Poker.FiveCardPokerScorer;
namespace Poker
{
    

        public class Card
        {
            public CardValue Value { get;   }
            public CardSuit Suit { get;   }

            public enum CardValue
            {
                Two = 2, Three, Four, Five, Six, Seven, Eight,
                Nine, Ten, Jack, Queen, King, Ace
            }
            public enum CardSuit
            {
                Spades, Diamonds, Clubs, Hearts
            }
    

        
         
        
       

        public Card(CardValue cardvalue, CardSuit cardsuit)
            {
                Value = cardvalue;
                Suit = cardsuit;
            }

        
        public override string ToString() => $"{Value} of {Suit}";


        private static void Main(string[] args)
        {


            var hand = new Hand();
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
            var Rank = Rankings();
            foreach(var rnk in Rank) {
            if (rnk.eval(hand.Cards) == true)
                {
                    Console.WriteLine( rnk.rank);
                }
            }
           // GetHandRank(hand.Cards).Should().Be(HandRank.HighCard);


        }


    }

        
    
}