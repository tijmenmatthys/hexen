using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Board
{
    public event EventHandler<PiecePlacedEventArgs> PiecePlaced;
    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;

    private Dictionary<Hex, PieceView> _pieces = new Dictionary<Hex, PieceView>();
    private int _size;

    public Board(int size)
    {
        _size = size;
    }

    public bool TryGetPiece(Hex position, out PieceView piece)
        => _pieces.TryGetValue(position, out piece);

    public bool IsValid(Hex position)
        => position.Length <= _size;

    public bool Place(Hex position, PieceView piece)
    {
        if (piece == null) return false;
        if (!IsValid(position)) return false;
        if (_pieces.ContainsKey(position)) return false;
        if (_pieces.ContainsValue(piece)) return false;

        _pieces.Add(position, piece);
        OnPiecePlaced(new PiecePlacedEventArgs(position, piece));
        return true;
    }

    public bool Move (Hex fromPosition, Hex toPosition)
    {
        if (!IsValid(toPosition)) return false;
        if (_pieces.ContainsKey(toPosition)) return false;
        if (!_pieces.TryGetValue(fromPosition, out var piece)) return false;

        _pieces.Remove(fromPosition);
        _pieces.Add(toPosition, piece);
        OnPieceMoved(new PieceMovedEventArgs(fromPosition, toPosition, piece));
        return true;
    }

    public bool Take (Hex position)
    {
        if (!IsValid(position)) return false;
        if (!_pieces.TryGetValue(position, out var piece)) return false;

        _pieces.Remove(position);
        OnPieceTaken(new PieceTakenEventArgs(position, piece));
        return true;
    }

    protected virtual void OnPiecePlaced(PiecePlacedEventArgs args)
    {
        var handler = PiecePlaced;
        handler?.Invoke(this, args);
    }
    protected virtual void OnPieceMoved(PieceMovedEventArgs args)
    {
        var handler = PieceMoved;
        handler?.Invoke(this, args);
    }
    protected virtual void OnPieceTaken(PieceTakenEventArgs args)
    {
        var handler = PieceTaken;
        handler?.Invoke(this, args);
    }
}

public class PiecePlacedEventArgs : EventArgs
{
    public Hex Position { get; }
    public PieceView Piece { get; }

    public PiecePlacedEventArgs(Hex position, PieceView piece)
    {
        Position = position;
        Piece = piece;
    }
}
public class PieceMovedEventArgs : EventArgs
{
    public Hex FromPosition { get; }
    public Hex ToPosition { get; }
    public PieceView Piece { get; }

    public PieceMovedEventArgs(Hex fromPosition, Hex toPosition, PieceView piece)
    {
        FromPosition = fromPosition;
        ToPosition = toPosition;
        Piece = piece;
    }
}
public class PieceTakenEventArgs : EventArgs
{
    public Hex Position { get; }
    public PieceView Piece { get; }

    public PieceTakenEventArgs(Hex position, PieceView piece)
    {
        Position = position;
        Piece = piece;
    }
}