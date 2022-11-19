using BoardSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class DeckView : MonoBehaviour
    {
        public EventHandler<CardEventArgs> CardDrag;
        public EventHandler<CardEventArgs> CardDrop;

        public void CardDragged(int cardNumber, Hex position)
        {
            OnCardDrag(new CardEventArgs(cardNumber, position));
        }

        public void CardDropped(int cardNumber, Hex position)
        {
            OnCardDrop(new CardEventArgs(cardNumber, position));
        }

        private void OnCardDrag(CardEventArgs e)
        {
            var handler = CardDrag;
            handler?.Invoke(this, e);
        }
        private void OnCardDrop(CardEventArgs e)
        {
            var handler = CardDrop;
            handler?.Invoke(this, e);
        }
    }

    public class CardEventArgs
    {
        public int CardNumber { get; }
        public Hex Position { get; }

        public CardEventArgs(int cardNumber, Hex position)
        {
            CardNumber = cardNumber;
            Position = position;
        }
    }
}