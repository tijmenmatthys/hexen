using BoardSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent OnDehighlight;
        [SerializeField] private UnityEvent OnHighlightValidDrop;
        [SerializeField] private UnityEvent OnHighlightInfluence;

        private BoardView _boardView;

        public Hex HexPosition => Hex.HexPosition(transform.position);

        private void Awake()
        {
            _boardView = GetComponentInParent<BoardView>();
        }

        // not used in Hexen, but keep it in case we need it later
        public void OnPointerClick(PointerEventData eventData)
        {
            _boardView.TileClicked(this);
        }

        public void Dehighlight() => OnDehighlight?.Invoke();
        public void HighlightValidDrop() => OnHighlightValidDrop?.Invoke();
        public void HighlightInfluence() => OnHighlightInfluence?.Invoke();
    }
}
