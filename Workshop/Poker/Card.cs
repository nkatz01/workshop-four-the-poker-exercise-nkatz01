using System;

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
             


           


        }


    }

        
    
}