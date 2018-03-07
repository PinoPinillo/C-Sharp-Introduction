using System;

namespace BlackJackDLL
{
    /// <summary>
    /// The suits enum
    /// </summary>
    public enum SuitEnum
    {
        Spades,
        Diamonds,
        Clubs,
        Hearts
    }

    /// <summary>
    /// Cards enum
    /// </summary>
    public enum CardEnum
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        J,
        Q,
        K,
        Ace
    }

    public class Card : IComparable<Card>, IEquatable<Card>
    {
        /// <summary>
        /// The card number (2, 3, ... 10, J, Q, K or Ace).
        /// </summary>
        public CardEnum Number { get; set; }
        /// <summary>
        /// Card suit (Spades, Diamonds, Clubs or Hearts).
        /// </summary>
        public SuitEnum Suit { get; set; }
        /// <summary>
        /// Card value in BlackJack game.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="suit"></param>
        public Card(CardEnum number, SuitEnum suit)
        {
            Number = number;
            Suit = suit;

            // Gets the value depending on the card number
            switch (Number)
            {
                case (CardEnum.Two):
                case (CardEnum.Three):
                case (CardEnum.Four):
                case (CardEnum.Five):
                case (CardEnum.Six):
                case (CardEnum.Seven):
                case (CardEnum.Eight):
                case (CardEnum.Nine):
                case (CardEnum.Ten):
                    Value = (int)Number;
                    break;
                case (CardEnum.J):
                case (CardEnum.Q):
                case (CardEnum.K):
                    Value = 10;
                    break;
                case (CardEnum.Ace):
                    Value = 11;
                    break;
                default:
                    Value = 0;
                    break;
            }
        }

        /// <summary>
        /// Compares this card with another one depending on their suit. If the cards has the same suit,
        /// compares their number.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Card other)
        {
            int suitResult = Suit.CompareTo(other.Suit);
            if (suitResult != 0)
                return suitResult;
            else
                return Number.CompareTo(other.Number);
        }

        /// <summary>
        /// Compares this card with another one and returns true if both cards has the same suit and the same number.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Card other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Shows this card information.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Number.ToString() + " of " + Suit.ToString();
        }
    }
}
