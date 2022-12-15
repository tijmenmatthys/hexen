using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem.MoveSets
{
    public abstract class MoveSet<TPiece>
    {
        protected readonly Board<TPiece> _board;

        protected MoveSet(Board<TPiece> board)
        {
            _board = board;
        }

        public abstract Hex[] GetValidDropPositions(Hex playerPosition);
        public abstract Hex[] GetInfluencePositions(Hex playerPosition, Hex dropPosition);
        protected abstract Dictionary<Hex, Hex> GetMoves(Hex playerPosition, Hex dropPosition);
        protected abstract Hex[] GetTakes(Hex playerPosition, Hex dropPosition);
        public virtual bool Execute(Hex playerPosition, Hex dropPosition)
        {
            var takes = GetTakes(playerPosition, dropPosition);
            foreach (Hex takePosition in takes)
            {
                _board.Take(takePosition);
            }

            var moves = GetMoves(playerPosition, dropPosition);
            foreach (KeyValuePair<Hex, Hex> move in moves)
            {
                _board.Move(move.Key, move.Value);
            }

            return true;
        }

        protected Hex[] RemoveInvalid(Hex[] original)
        {
            List<Hex> valid = new List<Hex>();
            foreach (Hex position in original)
                if (_board.IsValid(position))
                    valid.Add(position);
            return valid.ToArray();
        }
    }
}
