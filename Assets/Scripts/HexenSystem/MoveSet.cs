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

        public abstract List<Hex> GetValidDropPositions();
        public abstract List<Hex> GetAffectedPositions(Hex dropPosition);
        public abstract bool Execute(Hex dropPosition);
    }
}
