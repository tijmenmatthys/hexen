using BoardSystem;
using HexenSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class DeckView : MonoBehaviour
    {
        public EventHandler<CardViewEventArgs> CardDrag;
        public EventHandler<CardViewEventArgs> CardDrop;

        private List<CardView> _cardViews = new List<CardView>();

        public int Size => _cardViews.Count;

        private void OnEnable()
        {
            foreach (CardView cardview in GetComponentsInChildren<CardView>())
                _cardViews.Add(cardview);
        }

        public void OnDeckChanged(object sender, EventArgs e)
        {
            List<CardType> newDeck = ((Deck<CardType>)sender).VisibleCards;
            foreach (CardView cardView in _cardViews)
                cardView.Hide(true);
            for (int i = 0; i < newDeck.Count; i++)
            {
                _cardViews[i].Hide(false);
                _cardViews[i].Type = newDeck[i];
                _cardViews[i].Index = i;
            }
        }

        public void CardDragged(int index, CardType type, Hex position)
        {
            OnCardDrag(new CardViewEventArgs(index, type, position));
        }

        public void CardDropped(int index, CardType type, Hex position)
        {
            OnCardDrop(new CardViewEventArgs(index, type, position));
        }

        private void OnCardDrag(CardViewEventArgs e)
        {
            var handler = CardDrag;
            handler?.Invoke(this, e);
        }
        private void OnCardDrop(CardViewEventArgs e)
        {
            var handler = CardDrop;
            handler?.Invoke(this, e);
        }
    }
    public class CardViewEventArgs
    {
        public int Index { get; }
        public CardType Type { get; }
        public Hex Position { get; }

        public CardViewEventArgs(int index, CardType type, Hex position)
        {
            Index = index;
            Type = type;
            Position = position;
        }
    }
}