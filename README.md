# Functional Programming Workshop — Worksheet Four

Welcome to the functional programming workshop which will hopefully consolidate and expand your knowledge of FP techniques using `C#`.

## Prerequisites

You need to have read, understood, and experimented with the materials we have presented so far in the course. We will presume that you are using an IDE such as [Visual Studio][vs] or [Rider][rider] (not Visual Code or similar editors).

## Aims and objectives

The aim of this workshop is to familiarise you with Functional Programming techniques via `C#`.  
We will also that this as an opportunity to practice the extreme programming technique of *pair programming*.  
We will examine the following topics:

+ Immutable types,
+ Basic LINQ concepts,
+ Advanced LINQ concepts (`yield`),
+ `Func` delegates,
+ Expression bodied members,
+ Extension methods and pipe-lining,
+ Tuple types, and
+ Thread safe collections (advanced topic and only in passing).

## Getting started

Provided that you are using the latest version of one of the above IDEs (or reasonably recent) then there is no setup required.
We are going to follow a *Test Driven Development* (TDD) approach to software development, one of the Agile approaches.

Each *exercise* builds upon your answer to the previous exercise. 
You will be adding and modifying your various methods/classes/tests.
You should "`add`" and "`commit`" to your repository as you complete each exercise; in that way we can see  the progression of your code.

## Keeping in Sync

Once you have accepted and cloned the workshop you will need to add the source repository so that you can `pull` additional solutions.
To link your repository perform the following actions:

1. Issue the command:
	
	```
	git remote add upstream https://github.com/SDP-SPIII-2020/workshop-poker
	```
	
	To add the source repository and to be able to obtain the "updates".
	
2. Then issue the command:

	```
	git pull upstream master
	```

	when directed to during the workshop.

## Pairs

Find someone to work with for the duration of this workshop. 
We will be practicing the extreme programming technique of "pair programming".

+ When you work with a partner, you must work together in front of one computer for every problem that week.
+ While you are working, the computer screen should be visible to both people. 
+ One person should type, while the other person observes, critiques and plans what to do next. 
+ You **must** switch roles periodically, say every 15 minutes.
+ Failing to switch partners can result in a reduction of your overall exercise grade for the course.

## The exercises

We use the vehicle of a "real" application, that of a poker scoring game (it does not play the game).
We will explore the advantages/disadvantages of the functional programming approach as compared to imperative programming.

### Exercise: Create the "solution" and some "projects"

In this part of the exercise you'll create a new project to hold the unit tests.
To keep things simple, in this workshop we will use one project for all of our tests and another project for our application code. 

1. Create a solution using `.NET Core`; you can name it what you wish (e.g., `workshop`, or `fpworkshop`).
2. Create a new project in your solution called `Poker.Tests` based upon the test framework you wish to use (e.g., `MSTest`); this will be a "Unit Test Project" (the name may vary depending on whether you are using `Rider` or `Visual Studio`).
3. Next, you will write your first unit test for a poker card in the following exercise.

### Exercise: Create a Card test

In this exercise you will create a unit test with either `MSTest`, `NUnit`, or `xUnit` (it is up to you which one you use).
The rest of the examples we present will use `MSTest` — please see the articles on "[The Complete Unit Testing Collection][unit]" for a comparison of the unit testing frameworks, and unit testing in general.

3. Under the `Poker.Tests` project create an empty test class, `CardTests`; this will create an empty test with the `[TestMethod]` attribute (if you are using `MSTest` as the unit test framework). 

	```
	[TestClass]
    	public class CardTests
	```
4. In `CardTests.cs` add a new test method named `CanCreateCard`. 
Test methods in `MSTest` use the `[TestMethod]` attribute. 

	```
	[TestMethod]
        public void CanCreateCard()
	```
5. In the `CanCreateCard` method write a test that confirms that a new `Card` object can be created.

	```
	var card = new Card();
	Assert.IsNotNull(card); 
	```
	The actual test will be different for each unit testing framework; for example, for `xUnit`, the clause would be `Assert.NotNull(card);`.

6. As there is no class defined yet, we will consider that this the first failing test. 

In the next exercise you'll create a `Card` object to satisfy this test.

### Exercise: Create a `Card` object

1. Create another project in your solution called `Poker` that will hold all the application source code ("Console Application", `.NETCoreApp`).
2. Create a `Card` class (should create a file named `Card.cs`).
3. Make the card class public.

	```
	public class Card {}
	```

4. This should be enough to satisfy the first unit test. 
5. Add a *reference* to the test project for the application code project.
5. Build the project, open the "Test Explorer" and click "Run All". 
6. You should receive a green check-mark next to the `CanCreateCard` test.

### Exercise: Complete the `Card` class

A card must be able to represent a `Suit` and `Value` to be useful. 
We will now add some properties to the card and tests to insure a card has a `Suit` and `Value` when it is created.

1. Give the card properties to hold a `CardValue` and `CardSuit`:

	```
	public class Card
	{
    	public CardValue Value { get; set; }
    	public CardSuit Suit { get; set; }
	}
	```
2. Create *enums* for `CardValue` and `CardSuit`:

	```
	// CardValue.cs
    public enum CardValue
    {
        Two = 2, Three, Four, Five, Six, Seven, Eight,
        Nine, Ten, Jack, Queen, King, Ace
    }
	```
	and
	```
	// CardSuit.cs
    public enum CardSuit
    {
        Spades, Diamonds, Clubs, Hearts
    }
	```
3. Change the `CanCreateCard` test so that a `Card` has a value when it is created. 
4. Rename the test to `CanCreateCardWithValue`. 
5. Test to make sure the properties are `NotNull`.

	```
	[TestMethod]
	public void CanCreateCardWithValue() {

        var card = new Card();

        Assert.IsNotNull(card.Suit);
        Assert.IsNotNull(card.Value);
    }
	```
	Note that the test will pass even when no value was assigned. 
	This is because enums have a *default* value.
6. To insure that a value is intentionally set, add a constructor to the `Card` class that requires a `Suit` and `Value`.

	```
	public class Card
	{
    	public Card(CardValue value, CardSuit suit)
    	{
        	Value = value;
        	Suit = suit;
    	}

    	public CardValue Value { get; set; }
    	public CardSuit Suit { get; set; }
	}
	```
7. Change the assertion so that it checks for a predetermined `Suit` and `Value`.

	```
	var card = new Card(CardValue.Ace, CardSuit.Clubs);
	Assert.AreEqual(CardSuit.Clubs, card.Suit);
    Assert.AreEqual(CardValue.Ace, card.Value);    
	```
8. Re-run the test to verify that the test passes.

When we create a `Card` it must have its properties set to a value. 
In the next exercise you'll refactor the `Card` class so that it is immutable, meaning that its properties cannot be changed once an object is created.

### Exercise: Describing a Card

One positive aspect of `C#` programming is that both functional and OOP styles of programming are not mutually exclusive. 
In this exercise we will override the inherited `ToString` method of the 
`Card` object and use it to describe the `Card`'s values.

1. Create a new test `CanDescribeCard`, this test should test a `Card` with the values of `CardValue.Ace` and `CardSuit.Spades`. 
The `ToString` method should return `"Ace of Spades"` (not the song!).

	```
	[TestMethod]
    public void CanDescribeCard()
    {
    	var card = new Card(CardValue.Ace, CardSuit.Spades);
		Assert.AreEqual("Ace of Spades", card.ToString());
	}
	```	
2. Run the test to verify that it fails.
3. Update the `Card` object to make the test pass. 
Override the `ToString` method on the `Card` class and use the string interpolation syntax to write out the `Card`'s description.

	```
	public override string ToString()
    {
        return $"{Value} of {Suit}";
    }
	```
4. Re-run the test to verify that the test passes.

The `ToString` method is a simple, single line of code that returns a value. 
We will now use an expression-bodied member to reduce the amount of code. 

1. Locate the `ToString` method of the `Card` class you have just written.
2. Remove the braces `{}` from the method, and replace the `return` statement with a lambda arrow `=>`. 
The arrow implies that the method will return a value, and the result is a much simpler syntax.

	```
	public override string ToString() => $"{Value} of {Suit}";
	```
3. Re-run the test to verify that the test passes.

### Exercise: Immutable `Card` object

In functional programming immutable objects are used to reduce complexity and avoid unintended changes in state. 
An immutable object's state cannot be modified after it is created, lowering the risk of side-effects.

1. In the `Card` class, remove the `set` declarations from each property resulting in readonly properties.

	```
	public class Card
	{
    	public Card(CardValue value, CardSuit suit)
    	{
        	Value = value;
        	Suit = suit;
    	}

    	public CardValue Value { get; }
    	public CardSuit Suit { get; }

    	public override string ToString() => $"{Value} of {Suit}";
	}
	```
	Now the properties can only be set during the object's initialisation.
	
2. Re-run **all** of the tests to verify that they still pass.

You should now be comfortable with running unit tests, and we will not 
instruct you to run the tests after every code change.

### Exercise: Create a `Hand`

The poker hand will be used throughout the remainder of this workshop. 
The `Hand` will represent a player's hand of cards. 
For this workshop a five card hand will be scored using both *imperative* and *functional* programming.

1. Create a test class named `HandTests` under your test project.
2. Add the following tests to your class:

	```
	[TestMethod]
    public void CanCreateHand()
    {
    	var hand = new Hand();
    	Assert.IsFalse(hand.Cards.Count == 0);
        Assert.IsFalse(hand.Cards.Count == 0);
    }

	[TestMethod]
    public void CanHandDrawCard()
    {
    	var card = new Card(CardValue.Ace, CardSuit.Spades);
        var hand = new Hand();
		hand.Draw(card);
 		Assert.AreEqual(hand.Cards.First(), card);
    }
	```
3. Create a `Hand` class that satisfies the tests.

	The `Hand` class will hold a players hand of cards. 
In the next exercise you will be scoring the `Hand` of cards from the `Cards` property in the `Hand` class.

	```
	public class Hand
	{
    	public Hand()
    	{
        	Cards = new List<Card>();
    	}
    
    	public List<Card> Cards { get;}

    	public void Draw(Card card)
    	{
        	Cards.Add(card);
    	}
	}
	```
	
The `Hand` class holds a players hand of cards. 
In the next exercises we will *score* the `Hand` of cards from the `Cards` property in the `Hand` class.

### Exercise: Obtaining the high card

In a game where all players hands are equal in rank, the winner is decided by comparing the highest card in their hands. 

1. Add a `HighCard` method to the `Hand` that returns the highest `CardValue` in the hand.
2. Below is a test method you can use to validate your `HighCard` method:

	```
	[TestMethod]
    public void CanGetHighCard()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
        Assert.Equals(CardValue.King, hand.HighCard().Value);
    }	
	```

When this test passes, move on to the next exercise.

### Exercise: Hand rankings

In the previous exercise, you determined the high card value in a hand.
Now we will add a `GetHandRank` method that will return the `Hand`'s `HandRank`.

1. Add the `HandRank` enum to your project.

	```
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
	```
2. Use the following tests to create a `GetHandRank` method on the `Hand` object that will return the correct `HandRank` for the test. 
Only score the ranks below; you will be refactoring as the workshop progresses and scoring additional `HandRank`s as you become more confident with the OOP and Functional Programming features of this application.

	```
	[TestMethod]
    public void CanScoreHighCard()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
        Assert.AreEqual(HandRank.HighCard, hand.GetHandRank());
    }

    [TestMethod]
    public void CanScoreFlush()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Two, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Six, CardSuit.Spades));
        Assert.AreEqual(HandRank.Flush, hand.GetHandRank());
    }
        
    [TestMethod]
    public void CanScoreRoyalFlush()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
        hand.Draw(new Card(CardValue.King, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
        Assert.AreEqual(HandRank.RoyalFlush, hand.GetHandRank());
    }
	```
	
3. When these tests pass, move onto the next exercise.

### Exercise: Hand rankings — review

Now that we have written code for the `HighCard`, `Flush`, and `RoyalFlush` hand ranks, you should review the answers presented in class.
These outline solutions will hopefully show you how the code can be written using a functional programming approach. 

### Exercise: Hand Tests refactored

We can now refactor the tests using *method chains*. 
*Pipelines* are often found in functional programming languages. 
Pipelines allow functions to be chained or composed to produce easily maintainable and readable code.

1. Add a reference to `FluentAssertions` in `HandTests.cs`.

	```
	using FluentAssertions;
	```
	
	To achieve this you will need to add a new dependency to the `CsharpPoker.Tests` project using the `NuGet` facility; please refer to the inclass demonstration.
2. 	Modify the `Assert` statement to use the [`FluentAssertions`][fa] chain instead. 
To do this, start with the value that will be tested and continue with the method `Should().Be(expectedValue)`.

	```
	Assert.AreEqual(CardValue.King, hand.HighCard().Value);
	```
	becomes
	```
	hand.HighCard().Value.Should().Be(CardValue.King);
	```
	Below is a example of the completed `CanGetHighCard` test:
	```
	[TestMethod]
	public void CanGetHighCard()
	{
    	var hand = new Hand();
    	hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
    	hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
    	hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
    	hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
    	hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));

    	hand.HighCard().Value.Should().Be(CardValue.King);
	}
	```
3. Refactor all your tests to use Fluent Assertions.

### Exercise: Scoring Pairs

We are going to continue to update the poker hand using the imperative and OOP styles of programming which we then modify to adopt the functional programming approach.

1. Add the following test code to score pairs (*of a kind*) hand ranks.

	```
	[TestMethod]
    public void CanScorePair()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
        hand.GetHandRank().Should().Be(HandRank.Pair);
    }

    [TestMethod]
    public void CanScoreThreeOfAKind()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.GetHandRank().Should().Be(HandRank.ThreeOfAKind);
    }

    [TestMethod]
    public void CanScoreFourOfAKind()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.GetHandRank().Should().Be(HandRank.FourOfAKind);
    }

    [TestMethod]
    public void CanScoreFullHouse()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.GetHandRank().Should().Be(HandRank.FullHouse);
    }	
	```
2. When your code passes these tests move on to the next exercise.
3. Review the outline solution that implements the "Pairs (of a kind)" hand ranks to see how the code can be written using functional programming techniques.

### Exercise: Scoring a Straight

We continue by updating the poker hand to include further scoring methods.

1. Score a "Straight".

	```
    [TestMethod]
    public void CanScoreStraight()
    {
    	var hand = new Hand();
    	hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));

        hand.GetHandRank().Should().Be(HandRank.Straight);
    }

    [TestMethod]
    public void CanScoreStraightUnordered()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Queen, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));

        hand.GetHandRank().Should().Be(HandRank.Straight);
    }	
	```
	
2. When all the tests pass, review the outline solution and then move onto the last phase of the workshop.

## Refactoring

In the previous exercises you solved additional hand ranks using LINQ 
and a custom LINQ-like extension method.
Now we will refactor to use a more functional approach to scoring a hand.

As always, run the tests as you feel appropriate but at least after every code change.


### Exercise: Refactor with Pure Functions

Until now we have combined our `Hand` object with the behaviour of scoring. 
Ideally these concepts should not be mixed. 
As there is no state to be concerned about, the scoring functions can be extracted with relative ease.

1. Create a new `public static class` named `FiveCardPokerScorer`.
2. Create a new test class named `FiveCardPokerScorerTests`.
3. Move the corresponding tests from `HandTests` to `FiveCardPokerScorerTests`.

	```
	[TestMethod]
    public void CanGetHighCard()
    {
    	var hand = new Hand();
        hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Two, CardSuit.Hearts);
        
        FiveCardPokerScorer.HighCard(hand.Cards).Value.Should()
        	.Be(CardValue.King);
    }	
	```
4. Edit `Hand.cs` and move the scoring functionality to your new `FiveCardPokerScorer` class.
As the functionality will be external to the data, the functions will need to be modified to accept the data as a parameter. 
In addition, this will bring the application more in-line with a functional programming approach as the new functions can be considered "*pure functions*".

	```
	public static class FiveCardPokerScorer
	{
    	public static Card HighCard(IEnumerable<Card> cards) =>
    		cards.Aggregate((highCard, nextCard) => 
    		nextCard.Value > highCard.Value ? nextCard : highCard);

    	private static bool HasFlush(IEnumerable<Card> cards) => 
    		cards.All(c => cards.First().Suit == c.Suit);
	}
	```

### Exercise: Refactor GetHandRank with Tuples

1. Create a new public class named `Ranker`.
2. Edit `Hand.cs` and create a new `private` method named `Rankings` that generates a collection of `Tuple`s. 
	Use the following code as a starting point and complete the remaining items.
	
	```
	private List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)> 
		Rankings() =>
        	new List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>
        	{
            	// more ranks here
        	};
	```
3. Locate the `GetHandRank` method and remove the expression after the `=>`.

	```
	public static HandRank GetHandRank() =>
	```
4. After the `=>` write a new expression that uses the `Rankings` class that evaluates the hand rank. (Hint: think LINQ.)

### Exercise: Adding new hand ranks

The latest version of `GetHandRank` can be more flexible than the previous versions. 
As it is a collection, it can easily be added to and operated on using LINQ.

1. Adding new hand ranks is easy. 
	Simply add a new `Ranker` to the `Rankings` collection and the `GetHandRank` method will do the "heavy lifting".
2. Make the `GetHandRank` method even more robust by allowing `Rank`s to be added in any order.
3. Locate the `GetHandRank` method and order the rankings in descending order by their strength.

	```
	// With Tuples
	.OrderByDescending(rule => rule.rank)

	// Without Tuples
	.OrderByDescending(rule => rule.Rank)
	```
	
4. Solve the following unit test.

	```
	[TestMethod]
    public void CanScoreTwoPair()
    {
        var hand = new Hand();
        hand.Draw(new Card(CardValue.Ace, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));

        hand.GetHandRank().Should().Be(HandRank.TwoPair);
    }
	```
	
## Summary

Well, we have made it to the end of the workshop — well done.

What can we takeaway from this exercise:

1. Hopefully you have seen how to integrate some functional programming features into some working code and can now apply these techniques to your code (where appropriate).
2. There is a balance that can be found between imperative/OO and functional programming; one approach does not solve all problems.
3. That functional code, like LINQ expressions, is often easier to read (transparent?), maintain, and manage.
	
#### Credits

I've forgotten where I got this example from, at least a couple of sources.
Note to myself — need to find out and give *credit where credit is due*.

[fa]: https://fluentassertions.com
[vs]: localhost
[rider]: localhost
[unit]: https://dzone.com/articles/the-complete-unit-testing-collection?edition=571291&utm_source=Daily%20Digest&utm_medium=email&utm_campaign=Daily%20Digest%202020-01-31
