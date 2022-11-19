using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public class Board<TPiece>
    {
        public event EventHandler<PiecePlacedEventArgs<TPiece>> PiecePlaced;
        public event EventHandler<PieceMovedEventArgs<TPiece>> PieceMoved;
        public event EventHandler<PieceTakenEventArgs<TPiece>> PieceTaken;

        private Dictionary<Hex, TPiece> _pieces = new Dictionary<Hex, TPiece>();
        private int _size;

        public Board(int size)
        {
            _size = size;
        }

        public bool TryGetPiece(Hex position, out TPiece piece)
            => _pieces.TryGetValue(position, out piece);

        public bool IsValid(Hex position)
            => position.Length <= _size;

        public bool Place(Hex position, TPiece piece)
        {
            if (piece == null) return false;
            if (!IsValid(position)) return false;
            if (_pieces.ContainsKey(position)) return false;
            if (_pieces.ContainsValue(piece)) return false;

            _pieces.Add(position, piece);
            OnPiecePlaced(new PiecePlacedEventArgs<TPiece>(position, piece));
            return true;
        }

        public bool Move(Hex fromPosition, Hex toPosition)
        {
            if (!IsValid(toPosition)) return false;
            if (_pieces.ContainsKey(toPosition)) return false;
            if (!_pieces.TryGetValue(fromPosition, out var piece)) return false;

            _pieces.Remove(fromPosition);
            _pieces.Add(toPosition, piece);
            OnPieceMoved(new PieceMovedEventArgs<TPiece>(fromPosition, toPosition, piece));
            return true;
        }

        public bool Take(Hex position)
        {
            if (!IsValid(position)) return false;
            if (!_pieces.TryGetValue(position, out var piece)) return false;

            _pieces.Remove(position);
            OnPieceTaken(new PieceTakenEventArgs<TPiece>(position, piece));
            return true;
        }

        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece> args)
        {
            var handler = PiecePlaced;
            handler?.Invoke(this, args);
        }
        protected virtual void OnPieceMoved(PieceMovedEventArgs<TPiece> args)
        {
            var handler = PieceMoved;
            handler?.Invoke(this, args);
        }
        protected virtual void OnPieceTaken(PieceTakenEventArgs<TPiece> args)
        {
            var handler = PieceTaken;
            handler?.Invoke(this, args);
        }
    }

    public class PiecePlacedEventArgs<TPiece> : EventArgs
    {
        public Hex Position { get; }
        public TPiece Piece { get; }

        public PiecePlacedEventArgs(Hex position, TPiece piece)
        {
            Position = position;
            Piece = piece;
        }
    }
    public class PieceMovedEventArgs<TPiece> : EventArgs
    {
        public Hex FromPosition { get; }
        public Hex ToPosition { get; }
        public TPiece Piece { get; }

        public PieceMovedEventArgs(Hex fromPosition, Hex toPosition, TPiece piece)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
            Piece = piece;
        }
    }
    public class PieceTakenEventArgs<TPiece> : EventArgs
    {
        public Hex Position { get; }
        public TPiece Piece { get; }

        public PieceTakenEventArgs(Hex position, TPiece piece)
        {
            Position = position;
            Piece = piece;
        }
    }
}