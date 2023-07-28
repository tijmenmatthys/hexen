using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem
{
    public abstract class MoveSet<TPiece>
    {
        protected readonly Board<TPiece> _board;

        protected MoveSet(Board<TPiece> board)
        {
            _board = board;
        }

        public abstract List<Hex> GetValidDropPositions(Hex playerPosition);
        public abstract List<Hex> GetHitPositions(Hex PlayerPosition, Hex dropPosition);
        public abstract Dictionary<Hex, Hex> GetMoves(Hex PlayerPosition, Hex dropPosition);
        public abstract List<Hex> GetTakes(Hex PlayerPosition, Hex dropPosition);
        public virtual bool Execute(Hex playerPosition, Hex dropPosition)
        {
            bool success = true;

            var takes = GetTakes(playerPosition, dropPosition);
            foreach (Hex takePosition in takes)
            {
                success &= _board.Take(takePosition);
            }

            var moves = GetMoves(playerPosition, dropPosition);
            foreach (KeyValuePair<Hex, Hex> move in moves)
            {
                success &= _board.Move(move.Key, move.Value);
            }

            return success;
        }
    }
}
