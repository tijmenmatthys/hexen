using BoardSystem;
using HexenSystem.MoveSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem.MoveSets
{
    internal class ShootMoveSet<TPiece> : MoveSet<TPiece>
    {
        public ShootMoveSet(Board<TPiece> board) : base(board)
        {
        }

        public override Hex[] GetInfluencePositions(Hex playerPosition, Hex dropPosition)
            => RemoveInvalid(GetTakes(playerPosition, dropPosition));

        public override Hex[] GetValidDropPositions(Hex playerPosition)
            => RemoveInvalid(playerPosition.Neighbours);

        protected override Dictionary<Hex, Hex> GetMoves(Hex playerPosition, Hex dropPosition)
            => new Dictionary<Hex, Hex>();

        protected override Hex[] GetTakes(Hex playerPosition, Hex dropPosition)
            => Hex.Line(dropPosition, dropPosition - playerPosition, _board.Size * 2);
    }
}
