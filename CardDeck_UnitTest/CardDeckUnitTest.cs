using System;
using CardDeck;
using CardDeck.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardDeck_UnitTest
{
    [TestClass]
    public class CardDeckUnitTest
    {
        [TestMethod]
        public void TestCreateDeck()
        {
            Suit allSuits = ( Suit.Clubs | Suit.Diamonds | Suit.Hearts | Suit.Spades );
            int iStart = 1;
            int iEnd = 13;
            Deck objDeck = new Deck(allSuits, iStart, iEnd);
            Assert.AreEqual(52, objDeck.Cards.Count);
        }

        [TestMethod]
        public void TestShuffleDeck()
        {
            //Create 2 Decks
            Suit allSuits = (Suit.Clubs | Suit.Diamonds | Suit.Hearts | Suit.Spades);
            int iStart = 1;
            int iEnd = 13;
            Deck objDeck1 = new Deck(allSuits, iStart, iEnd);
            Deck objDeck2 = new Deck(allSuits, iStart, iEnd);
            //Shuffle deck
            objDeck1.Shuffle();
            //Loop through deck till difference in card order is found
            bool bSame = objDeck1.CompareDeck(objDeck2);
            Assert.AreEqual(false, bSame);
        }

        /// <summary>
        /// Sort Deck by Suit Ascending give precidents to Suit over the card value
        /// Ace of Clubs - King of Clubs, Ace of Diamonds - King of Diamonds, etc...
        /// </summary>
        [TestMethod]
        public void TestSortDeckBySuitAsc()
        {
            int iStart = 1;
            int iEnd = 13;
            Suit allSuits = (Suit.Clubs | Suit.Diamonds | Suit.Hearts | Suit.Spades);
            Deck objDeck = new Deck(allSuits, iStart, iEnd);
            //Shuffle deck
            objDeck.Shuffle();
            //Sort Deck by Suit, Value Ascending
            objDeck.Sort(DeckSortMethod.BySuitAscending);
            //build control list
            List<string> list = GetControlList(DeckSortMethod.BySuitAscending, iStart, iEnd);
            bool bSame = true;
            for (int i = 0; i < objDeck.Cards.Count; i++)
            {
                if (objDeck.Cards[i].DisplayValue != list[i])
                {
                    bSame = false;
                    break;
                }
            }
            Assert.AreEqual(true, bSame);
        }

        /// <summary>
        /// Sort Deck by Suit Ascending give precidents to card value over the suit
        /// Ace of Clubs Ace of Diamonds, Ace of Hearts, Ace of Spades, etc...
        /// </summary>
        [TestMethod]
        public void TestSortDeckByNumberAsc()
        {
            int iStart = 1;
            int iEnd = 13;
            Suit allSuits = (Suit.Clubs | Suit.Diamonds | Suit.Hearts | Suit.Spades);
            Deck objDeck = new Deck(allSuits, iStart, iEnd);
            //Shuffle deck
            objDeck.Shuffle();
            //Sort Deck by Suit, Value Ascending
            objDeck.Sort(DeckSortMethod.ByNumberAscending);
            //build control list
            List<string> list = GetControlList(DeckSortMethod.ByNumberAscending, iStart, iEnd);
            bool bSame = true;
            for (int i = 0; i < objDeck.Cards.Count; i++)
            {
                if (objDeck.Cards[i].DisplayValue != list[i])
                {
                    bSame = false;
                    break;
                }
            }
            Assert.AreEqual(true, bSame);
        }

        /// <summary>
        /// Sort Deck by Suit Descending give precidents to suit over the card value 
        /// King of Spades - Ace of Spades, King of Hearts - Ace of Hearts, etc...
        /// </summary>
        [TestMethod]
        public void TestSortDeckBySuitDesc()
        {
            int iStart = 1;
            int iEnd = 13;
            Suit allSuits = (Suit.Clubs | Suit.Diamonds | Suit.Hearts | Suit.Spades);
            Deck objDeck = new Deck(allSuits, iStart, iEnd);
            //Shuffle deck
            objDeck.Shuffle();
            //Sort Deck by Suit, Value Ascending
            objDeck.Sort(DeckSortMethod.BySuitDescending);
            //build control list
            List<string> list = GetControlList(DeckSortMethod.BySuitDescending, iStart, iEnd);
            bool bSame = true;
            for (int i = 0; i < objDeck.Cards.Count; i++)
            {
                if (objDeck.Cards[i].DisplayValue != list[i])
                {
                    bSame = false;
                    break;
                }
            }
            Assert.AreEqual(true, bSame);
        }

        /// <summary>
        /// Sort Deck by Suit Descending give precidents to card value over the suit
        /// King of Spades, King of Hearts, King of Diamonds, King of Clubs, etc...
        /// </summary>
        [TestMethod]
        public void TestSortDeckByNumberDesc()
        {
            int iStart = 1;
            int iEnd = 13;
            Suit allSuits = (Suit.Clubs | Suit.Diamonds | Suit.Hearts | Suit.Spades);
            Deck objDeck = new Deck(allSuits, iStart, iEnd);
            //Shuffle deck
            objDeck.Shuffle();
            //Sort Deck by Suit, Value Ascending
            objDeck.Sort(DeckSortMethod.ByNumberDescending);
            //build control list
            List<string> list = GetControlList(DeckSortMethod.ByNumberDescending, iStart, iEnd);
            bool bSame = true;
            for (int i = 0; i < objDeck.Cards.Count; i++)
            {
                if (objDeck.Cards[i].DisplayValue != list[i])
                {
                    bSame = false;
                    break;
                }
            }
            Assert.AreEqual(true, bSame);
        }

        /// <summary>
        /// Gets a control list used to compare sort order of a deck of cards
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private List<string> GetControlList(DeckSortMethod sort, int start, int end)
        {
            Dictionary<int, Suit> orderMap = new Dictionary<int, Suit>()
            {
                {0, Suit.Clubs },
                {1, Suit.Diamonds },
                {2, Suit.Hearts },
                {3, Suit.Spades }
            };
            //flip enum order if descending
            if (sort == DeckSortMethod.ByNumberDescending || sort == DeckSortMethod.BySuitDescending)
            {
                orderMap = new Dictionary<int, Suit>()
                {
                    {0, Suit.Spades },
                    {1, Suit.Hearts },
                    {2, Suit.Diamonds },
                    {3, Suit.Clubs }
                };
            }
            
            List<string> retval = new List<string>();
            Suit current;
            switch (sort)
            {
                case DeckSortMethod.ByNumberAscending:
                    for (int i = start; i <= end; i++)
                    {
                        foreach (int key in orderMap.Keys)
                        {
                            current = orderMap[key];
                            retval.Add(Card.GetDisplay(current, i));
                        }
                    }
                    break;
                case DeckSortMethod.ByNumberDescending:
                    for (int i = end; i >= start; i--)
                    {
                        foreach (int key in orderMap.Keys)
                        {
                            current = orderMap[key];
                            retval.Add(Card.GetDisplay(current, i));
                        }
                    }
                    break;
                case DeckSortMethod.BySuitAscending:
                    foreach (int key in orderMap.Keys)
                    {
                        current = orderMap[key];
                        for (int i = start; i <= end; i++)
                        {
                            retval.Add(Card.GetDisplay(current, i));
                        }
                    }
                    break;
                case DeckSortMethod.BySuitDescending:
                    foreach (int key in orderMap.Keys)
                    {
                        current = orderMap[key];
                        for (int i = end; i >= start; i--)
                        {
                            retval.Add(Card.GetDisplay(current, i));
                        }
                    }
                    break;
            }
            return retval;
        }
    }
}
