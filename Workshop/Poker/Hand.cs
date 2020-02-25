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
			HighCard ,
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

			int  HighestValue = (int)Cards.Max(i => i.Value);
			Card card = Cards.First(obj =>(int)obj.Value == HighestValue);
			return card;
		}

		//public int CompareTo(Card other) { return other.Value.CompareTo(other.Value); }


		public HandRank GetHandRank()
		{
			 
			IEnumerable<Card> filtered = Cards.FindAll(i =>  i.Suit == CardSuit.Spades ||   (i.Suit == CardSuit.Hearts &&  !(i.Suit == CardSuit.Spades || i.Suit == CardSuit.Clubs ||  i.Suit == CardSuit.Diamonds))
			|| (i.Suit == CardSuit.Clubs && !(i.Suit == CardSuit.Spades || i.Suit == CardSuit.Hearts || i.Suit == CardSuit.Diamonds))
			|| (i.Suit == CardSuit.Diamonds && !(i.Suit == CardSuit.Spades || i.Suit == CardSuit.Hearts || i.Suit == CardSuit.Clubs))
			);
			if (filtered.Count() >= 5) {
				List<int> values = Cards.Select(crd => (int)crd.Value).ToList();
				if (values.Contains(10) && values.Contains(11) && values.Contains(12) && values.Contains(13) && values.Contains(14))
					return HandRank.RoyalFlush;
				else
				{

					var sequences = values.Distinct()
					 .GroupBy(num => Enumerable.Range(num, int.MaxValue - num + 1)
											   .TakeWhile(values.Contains)
											   .Last())  //use the last member of the consecutive sequence as the key
					 .Where(seq => seq.Count() >= 3)
					 .Select(seq => seq.OrderBy(num => num)); // not necessary unless ordering is desirable inside e
															  //foreach (IOrderedEnumerable<int> s in sequences)
															  //{
															  //	Console.WriteLine(s);
															  //}
				//	Console.WriteLine("{0}", string.Join(",", values));
					return HandRank.Flush;
				}

			}
			//https://stackoverflow.com/questions/3844611/detecting-sequence-of-at-least-3-sequential-numbers-from-a-given-list
			//static IOrderedEnumerable<int> Iter(IOrderedEnumerable<int> collec)
			//{
			//	foreach (int n in collec)
			//	{
			//		yield return n;
			//	}
			//}

			//IEnumerable<Card> filteredSpades = Cards.Where(i => i.Suit == CardSuit.Spades);
			//	IEnumerable<Card> filteredHearts = Cards.Where(i => i.Suit == CardSuit.Hearts);
			//IEnumerable<Card> filteredClubs = Cards.Where(i => i.Suit == CardSuit.Clubs);
			//IEnumerable<Card> filteredDiamonds = Cards.Where(i => i.Suit == CardSuit.Diamonds);
			//if (filteredClubs.Contains(i => i.Value = ))






			//bool flush = (Cards.Count(i => i.Suit == CardSuit.Spades) >= 5
			//		|| Cards.Count(i => i.Suit == CardSuit.Hearts) >= 5
			//			|| Cards.Count(i => i.Suit == CardSuit.Clubs) >= 5
			//		|| Cards.Count(i => i.Suit == CardSuit.Diamonds) >= 5);
			//bool royalFlush = flush & Cards.

			//return HandRank.Flush;

			//	Card card = Cards.Find(i => i.Value == CardValue.Ten);

			//if (card != null)
			//{
			//else if (Cards.Count(i => i.Suit == CardSuit.Spades) >= 5)

			else
				return HandRank.HighCard;
		


		}



		public void Draw(Card card)
		{
			Cards.Add(card);
		}
	}
}
