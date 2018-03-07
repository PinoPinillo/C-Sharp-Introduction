using System;
using System.Collections.Generic;

namespace BlackJackDLL
{
    public class Deck
    {
        /// <summary>
        /// Cards inside the deck.
        /// </summary>
        List<Card> cards;
        /// <summary>
        /// Random comparator used to shuffle the deck.
        /// </summary>
        RandomComparator comparator = new RandomComparator();
        /// <summary>
        /// Random number used to get cards from the deck.
        /// </summary>
        Random randomNumber = new Random();

        /// <summary>
        /// Constructor.
        /// </summary>
        public Deck()
        {
            // Initializates the cards list
            cards = new List<Card>();

            // Fills the deck with 52 cards
            foreach (SuitEnum suit in Enum.GetValues(typeof(SuitEnum)))
            {
                foreach (CardEnum card in Enum.GetValues(typeof(CardEnum)))
                {
                    cards.Add(new Card(card, suit));
                }
            }
        }

        /// <summary>
        /// Gets the first card and removes it from the deck.
        /// </summary>
        /// <returns></returns>
        public Card AskForCard()
        {
            Card card = cards[0];
            cards.Remove(card);
            return card;
        }

        /// <summary>
        /// Gets a random card and removes it from the deck.
        /// </summary>
        /// <returns></returns>
        public Card AskForRandomCard()
        {
            Card card = cards[randomNumber.Next(0, cards.Count)];
            cards.Remove(card);
            return card;
        }

        /// <summary>
        /// Calculates the probability of getting a card greater than 21 - (player score).
        /// </summary>
        /// <param name="actualValue">The player score</param>
        /// <returns></returns>
        public float CalculateLosingProbability(int actualValue)
        {
            int valueLeft = 21 - actualValue;
            int cont = 0;

            foreach (Card card in cards)
            {
                if (card.Value > valueLeft)
                    cont++;
            }

            return cont / (float)cards.Count;
        }

        /// <summary>
        /// Sorts the deck with the card default comparator.
        /// </summary>
        public void SortDeck()
        {
            cards.Sort();
        }

        /// <summary>
        /// Shuffles the deck using a random comparator.
        /// </summary>
        public void ShuffleDeck()
        {
            cards.Sort(new RandomComparator());
        }
    }

    /// <summary>
    /// Random Comparator used to shuffle the deck.
    /// </summary>
    class RandomComparator : IComparer<Card>
    {
        Random randomNumber = new Random();
        public int Compare(Card card1, Card card2)
        {
            return randomNumber.Next(-1, 2);
        }
    }
}
