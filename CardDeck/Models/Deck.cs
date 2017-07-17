using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck.Models
{
    [Flags]
    public enum DeckSortMethod
    {
        BySuitAscending = 1,
        BySuitDescending = 2,
        ByNumberAscending = 4,
        ByNumberDescending = 8
    }
    
    /// <summary>
    /// Deck object represents a deck of cards used in traditional 52-card deck games.
    /// </summary>
    public class Deck
    {
    #region Private Members
        private List<Card> _cards;
    #endregion
    #region Public Properties
        public List<Card> Cards
        {
            get
            {
                return _cards;
            }
        }
    #endregion
    #region Constructors
        public Deck(Suit suits, int startRange, int endRange)
        {
            CreateDeck(suits, startRange, endRange);
        }
    #endregion
    #region Public Methods
        /// <summary>
        /// Rearrange cards into a random order
        /// </summary>
        public void Shuffle()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int iOriginal = this.Cards.Count;
            //copy the original list to a temporary array
            List<Card> temp = this.Cards.ToList<Card>();
            //clear the deck
            this.Cards.Clear();
            //create random seed out of 52;
            int index = 0;
            Card current = default(Card);
            //loop through till all cards in array are gone
            while (temp.Count > 0)
            {
                //get random index
                index = rnd.Next(0, temp.Count);
                //copy value to current
                current = temp[index];
                //remove object from temp list
                temp.RemoveAt(index);
                //add back to Cards list
                this.Cards.Add(current);
            }
            //verify card count is the same
            if (iOriginal != this.Cards.Count)
                throw new DeckException("Card count isn't the same after shuffle.");
        }

        public void Sort(DeckSortMethod sort)
        {
            Dictionary<Suit, int> orderMap = new Dictionary<Suit, int>()
            {
                {Suit.Clubs, 0 },
                {Suit.Diamonds, 1 },
                {Suit.Hearts, 2 },
                {Suit.Spades, 3 }
            };
            IOrderedEnumerable<Card> sorted = null; 
            switch (sort)
            {
                case DeckSortMethod.ByNumberAscending:
                    sorted = Cards.OrderBy(m => m.Value).ThenBy(m => orderMap[m.Suit]);
                    break;
                case DeckSortMethod.ByNumberDescending:
                    sorted = Cards.OrderByDescending(m => m.Value).ThenByDescending(m => orderMap[m.Suit]);
                    break;
                case DeckSortMethod.BySuitAscending:
                    sorted = Cards.OrderBy(m => orderMap[m.Suit]).ThenBy(m => m.Value);
                    break;
                case DeckSortMethod.BySuitDescending:
                    sorted = Cards.OrderByDescending(m => orderMap[m.Suit]).ThenByDescending(m => m.Value);
                    break;
            }
            _cards = sorted.ToList<Card>();
        }
        /// <summary>
        /// Compares all cards to determine if the card orders are the same
        /// </summary>
        /// <param name="compare"></param>
        /// <returns></returns>
        public bool CompareDeck(Deck compare)
        {
            if (Cards.Count != compare.Cards.Count)
                throw new DeckException("Deck must have same number of cards.");
            for (int i = 0; i < this.Cards.Count; i++)
            {
                if (!Cards[i].Equal(compare.Cards[i]))
                    return false;
            }
            return true;
        }
    #endregion
    #region Private Methods
        private void CreateDeck(Suit suits, int startRange, int endRange)
        {
            if (endRange > 13)
                throw new CardException("Card value cannot exceed 13");
            if (startRange < 1)
                throw new CardException("Card value must be at least 1");
            
            _cards = new List<Card>();
            if (suits.HasFlag(Suit.Clubs))
                _cards.AddRange(CreateSuit(Suit.Clubs, startRange, endRange));

            if (suits.HasFlag(Suit.Diamonds))
               _cards.AddRange(CreateSuit(Suit.Diamonds, startRange, endRange));

            if (suits.HasFlag(Suit.Hearts))
                _cards.AddRange(CreateSuit(Suit.Hearts, startRange, endRange));

            if (suits.HasFlag(Suit.Spades))
                _cards.AddRange(CreateSuit(Suit.Spades, startRange, endRange));

        }
        private List<Card> CreateSuit(Suit suit, int startRange, int endRange)
        {
            List<Card> retval = new List<Models.Card>();
            for (int i = startRange; i <= endRange; i++)
            {
                retval.Add(new Models.Card(i, suit));
            }
            return retval;
        }
    #endregion
    }
}
