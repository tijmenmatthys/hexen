using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem.MoveSets
{
    public class TeleportMoveSet<TPiece> : MoveSet<TPiece>
    {
        public TeleportMoveSet(Board<TPiece> board) : base(board)
        {
        }

        public override Hex[] GetInfluencePositions(Hex playerPosition, Hex dropPosition)
            => new Hex[] { dropPosition };

        public override Hex[] GetValidDropPositions(Hex playerPosition)
            => _board.GetEmptyPositions();

        protected override Dictionary<Hex, Hex> GetMoves(Hex playerPosition, Hex dropPosition)
            => new Dictionary<Hex, Hex>() { { playerPosition, dropPosition } };

        protected override Hex[] GetTakes(Hex playerPosition, Hex dropPosition)
            => Array.Empty<Hex>();
    }
}
