using BoardSystem;
using GameSystem.Views;
using HexenSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private List<CardType> _startingCards = new List<CardType>();

        private BoardView _boardView;
        private DeckView _deckView;

        private Board<PieceView> _board;
        private Deck<CardType> _deck;
        private Engine<PieceView> _engine;

        void Start()
        {
            InitViews();
            InitModels();
            LinkViews();
        }

        private void InitViews()
        {
            // subscribe to BoardView events
            _boardView = FindObjectOfType<BoardView>();
            _boardView.Click += BoardClicked;

            // subscribe to DeckView events
            _deckView = FindObjectOfType<DeckView>();
            _deckView.CardDrag += CardDragged;
            _deckView.CardDrop += CardDropped;
        }

        private void InitModels()
        {
            // Create Board model & subscribe to events
            _board = new Board<PieceView>(_boardView.Size);
            _board.PiecePlaced += (s, e)
                => e.Piece.Placed(e.Position.WorldPosition);
            _board.PieceMoved += (s, e)
                => e.Piece.Moved(e.FromPosition.WorldPosition, e.ToPosition.WorldPosition);
            _board.PieceTaken += (s, e)
                => e.Piece.Taken();

            // Create Deck model & subscribe to events
            _deck = new Deck<CardType>(_deckView.Size);
            _deck.Change += _deckView.OnDeckChanged;
            foreach (CardType card in _startingCards)
                _deck.AddCard(card);

            // Create Engine
            _engine = new Engine<PieceView>(_board);
        }

        private void LinkViews()
        {
            // Link PieceViews to the board model
            var pieceViews = FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(Hex.HexPosition(pieceView.WorldPosition), pieceView);
        }

        private void CardDropped(object sender, CardViewEventArgs e)
        {
            Debug.Log($"Card {e.Type} dropped over {e.Position}");

            if (_engine.PlayCard(e.Position, e.Type))
                _deck.RemoveCard(e.Index);

            _boardView.DeHighlightTiles();
        }

        private void CardDragged(object sender, CardViewEventArgs e)
        {
            Debug.Log($"Card {e.Type} dragged over {e.Position}");

            CardType cardType = e.Type;
            Hex hoverPosition = e.Position;
            if (!_engine.TryGetPlayerPosition(out Hex playerPosition)) return;
            
            var validDropPositions = _engine.MoveSets[cardType].GetValidDropPositions(playerPosition);
            if (!validDropPositions.Contains(hoverPosition))
                _boardView.HighlightValidDropTiles(validDropPositions);
            else
            {
                var influencePositions = _engine.MoveSets[cardType].GetInfluencePositions(playerPosition, hoverPosition);
                _boardView.HighlightInfluenceTiles(influencePositions);
            }
        }

        private void BoardClicked(object sender, BoardClickEventArgs e)
        {
            // not needed for Hexen, but keep it in case we need it later
        }
    }
}
