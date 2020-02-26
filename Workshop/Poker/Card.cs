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

        //public override string ToString()
        //{
        //    // Value.ToString() + " of " + Suit.ToString();
        //    return $"{Value} of {Suit}";
        //        }

        public override string ToString() => $"{Value} of {Suit}";


        private static void Main(string[] args)
        {
            Card c = new Card(CardValue.Ace, CardSuit.Spades);
            Console.WriteLine(c.ToString());

            var hand = new Hand();
            hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Two, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Four, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.GetHandRank();

        }


    }

        
    
}