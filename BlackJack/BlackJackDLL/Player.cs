using System.Collections.Generic;

namespace BlackJackDLL
{
    public class Player
    {
        /// <summary>
        /// Player's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Has the player a BlackJack?
        /// </summary>
        public bool BlackJack { get; set; }
        /// <summary>
        /// Player's cards.
        /// </summary>
        public List<Card> cards;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The player's name</param>
        public Player(string name)
        {
            Name = name;
            // By default the player does not have a BlackJack
            BlackJack = false;
            // Initializates the cards list
            cards = new List<Card>();
        }

        /// <summary>
        /// Calculates the total score depending on player's cards.
        /// </summary>
        /// <returns>The total score</returns>
        public int GetTotalValue()
        {
            int result = 0;

            foreach (Card card in cards)
            {
                result += card.Value;
            }

            return result;
        }

        /// <summary>
        /// Shows player's cards information and the total score.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = Name + " cards:\n";

            foreach (Card card in cards)
            {
                result += card.ToString() + "\n";
            }

            result += "\nYour total value is: " + GetTotalValue() + "\n";

            return result;
        }
    }
}
