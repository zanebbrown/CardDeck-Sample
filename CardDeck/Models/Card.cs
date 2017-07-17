using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck.Models
{
    [Flags]
    public enum Suit
    {
        Clubs = 1,
        Diamonds = 2,
        Hearts = 4,
        Spades = 8
    }
    /// <summary>
    /// Card object represents a card value used in traditional 52-card deck.
    /// Ace - King, in suite Clubs, Diamonds, Hearts or Spades.
    /// </summary>
    public class Card
    {
    #region Private Members
        private Suit _suit;
        private int _value;
    #endregion
    #region Public Properties
        /// <summary>
        /// Returns suit value of the card
        /// </summary>
        public Suit Suit
        {
            get
            {
                return _suit;
            }
        }
        /// <summary>
        /// Return numeric value of the card
        /// </summary>
        public int Value
        {
            get
            {
                return _value;
            }
        }
        /// <summary>
        /// Returns standard value of the card
        /// </summary>
        public string DisplayValue
        {
            get
            {
                return GetDisplay(_suit, _value);
            }
        }
    #endregion
    #region Constructors
        /// <summary>
        /// Creates and object that contain the value of single card 
        /// </summary>
        /// <param name="value">numeric value between 1-13</param>
        /// <param name="suit">enum value Clubs, Diamonds, Hearts or Spades</param>
        /// <exception>Card Exception invalid value or suit</exception>
        public Card(int value, Suit suit)
        {
            if (value > 13)
                throw new CardException("Card value cannot exceed 13");
            if (value < 1)
                throw new CardException("Card value must be at least 1");
            this._value = value;
            this._suit = suit;
        }
    #endregion
    #region Public Methods
        public bool Equal(Card card)
        {
            if (this.DisplayValue == card.DisplayValue)
                return true;
            else
                return false;
        }
        public static string GetDisplay(Suit suit, int value)
        {
            string retval = "";
            switch (value)
            {
                case 1:
                    retval = "A";
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    retval = value.ToString();
                    break;
                case 11:
                    retval = "J";
                    break;
                case 12:
                    retval = "Q";
                    break;
                case 13:
                    retval = "K";
                    break;
            }
            switch (suit)
            {
                case Suit.Clubs:
                    retval += "C";
                    break;
                case Suit.Diamonds:
                    retval += "D";
                    break;
                case Suit.Hearts:
                    retval += "H";
                    break;
                case Suit.Spades:
                    retval += "S";
                    break;
            }
            return retval;
        }
    #endregion
    }
}
