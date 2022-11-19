using BoardSystem;
using GameSystem.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private Board<PieceView> _board;

        void Start()
        {
            InitBoard();


        }

        private void InitBoard()
        {
            // subscribe to BoardView events
            var boardView = FindObjectOfType<BoardView>();
            boardView.Click += BoardClicked;

            // subscribe to DeckView events
            var deckView = FindObjectOfType<DeckView>();
            deckView.CardDrag += CardDragged;
            deckView.CardDrop += CardDropped;

            // subscribe to Board(Model) events
            _board = new Board<PieceView>(boardView.Size);
            _board.PiecePlaced += (s, e)
                => e.Piece.Placed(e.Position.WorldPosition);
            _board.PieceMoved += (s, e)
                => e.Piece.Moved(e.FromPosition.WorldPosition, e.ToPosition.WorldPosition);
            _board.PieceTaken += (s, e)
                => e.Piece.Taken();

            // initialise PieceViews in the board model
            var pieceViews = FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(Hex.HexPosition(pieceView.WorldPosition), pieceView);
        }

        private void CardDropped(object sender, CardEventArgs e)
        {
            Debug.Log($"Card {e.CardNumber} dropped over {e.Position}");
        }

        private void CardDragged(object sender, CardEventArgs e)
        {
            Debug.Log($"Card {e.CardNumber} dragged over {e.Position}");
        }

        private void BoardClicked(object sender, BoardClickEventArgs e)
        {
            // test code: move a clicked piece one tile
            Hex fromPosition = e.Position;
            Hex toPosition = e.Position + Hex.downRight;
            if (_board.TryGetPiece(e.Position, out var piece))
                _board.Move(fromPosition, toPosition);
        }
    }
}
