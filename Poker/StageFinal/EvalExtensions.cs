﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace StageFinal
{
    public static class EvalExtensions
    {
        // Extension methods are an easy way to create a pipeline.
        // The ToPairs extension method is a specialized mapping method to convert
        // from IEnumerable<Card> to IEnumerable<KeyValuePair<CardValue, int>> (cards, to counted pairs)
        // LINQ is a great example of how effective extension methods can be.
        public static IEnumerable<KeyValuePair<CardValue, int>> ToKindAndQuantities(this IEnumerable<Card> cards)
        {
            var dict = new ConcurrentDictionary<CardValue, int>();
            foreach (var card in cards) dict.AddOrUpdate(card.Value, 1, (cardValue, quantity) => ++quantity);

            return dict;
        }

        // The SelectConsecutive method iterates over two consecutive items in a collection.
        // This is done using the yield keyword
        // Each call to the iterator function proceeds to the next execution of the yield return statement
        // This method is very similar to the source code found in LINQ methods
        public static IEnumerable<TResult> SelectConsecutive<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TSource, TResult> selector)
        {
            var index = -1;
            foreach (var element in source.Take(source.Count() - 1)
            ) // skip the last, it will be evaluated by source.ElementAt(index + 1)
            {
                checked
                {
                    index++;
                } // explicitly enable overflow checking

                yield return
                    selector(element,
                        source.ElementAt(
                            index + 1)); // delegate element and element[+1] to Func<TSource, TSource, TResult>
            }
        }
    }
}