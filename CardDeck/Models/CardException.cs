using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck.Models
{
    /// <summary>
    /// Exception class to handles invalid card operations
    /// </summary>
    public class CardException : Exception
    {
        public CardException(string message) : base (message)
        {
            
        }
    }
    /// <summary>
    /// Exception class to handle invalid deck operations
    /// </summary>
    public class DeckException : Exception
    {
        public DeckException(string message) : base (message)
        {

        }
    }
}
