using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public class Deck<TCard>
    {
        // the add/remove/move events are only fired for the visible part of the card deck
        // since that's the only thing that needs to be updated visually
        public event EventHandler<EventArgs> Change;

        private int _visibleSize;
        private List<TCard> _cards = new List<TCard>();

        public List<TCard> VisibleCards
        {
            get
            {
                if (_cards.Count >= _visibleSize) return _cards.GetRange(0, _visibleSize);
                else return _cards;
            }
        }

        public Deck(int visibleSize)
        {
            _visibleSize = visibleSize;
        }

        public void RemoveCard(int position)
        {
            _cards.RemoveAt(position);
            OnDeckChanged(EventArgs.Empty);
        }

        public void AddCard(TCard card)
        {
            _cards.Add(card);
            OnDeckChanged(EventArgs.Empty);
        }

        protected virtual void OnDeckChanged(EventArgs args)
        {
            var handler = Change;
            handler?.Invoke(this, args);
        }
    }
}
