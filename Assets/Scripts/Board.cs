using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Board
{
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

        return true;
    }

    public bool Move (Hex fromPosition, Hex toPosition)
    {
        if (!IsValid(toPosition)) return false;
        if (_pieces.ContainsKey(toPosition)) return false;
        if (!_pieces.TryGetValue(fromPosition, out var piece)) return false;

        _pieces.Remove(fromPosition);
        _pieces.Add(toPosition, piece);

        return true;
    }

    public bool Take (Hex position)
    {
        if (!IsValid(position)) return false;
        if (!_pieces.TryGetValue(position, out var piece)) return false;

        _pieces.Remove(position);

        return true;
    }
}
