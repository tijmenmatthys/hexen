using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem.MoveSets
{
    public class BombMoveSet<TPiece> : MoveSet<TPiece>
    {
        public BombMoveSet(Board<TPiece> board) : base(board)
        {
        }

        public override Hex[] GetInfluencePositions(Hex playerPosition, Hex dropPosition)
            => RemoveInvalid(dropPosition.GetNeighboursInRange(1));

        public override Hex[] GetValidDropPositions(Hex playerPosition)
            => _board.GetAllPositions();

        protected override Dictionary<Hex, Hex> GetMoves(Hex playerPosition, Hex dropPosition)
            => new Dictionary<Hex, Hex>();

        protected override Hex[] GetTakes(Hex playerPosition, Hex dropPosition)
        {
            Hex[] takes = GetInfluencePositions(playerPosition, dropPosition);
            return takes.Except(new Hex[] { playerPosition }).ToArray();
        }
    }
}
