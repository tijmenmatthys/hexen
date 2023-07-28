using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem.MoveSets
{
    internal class SwapMoveSet<TPiece> : MoveSet<TPiece>
    {
        public SwapMoveSet(Board<TPiece> board) : base(board)
        {
        }

        public override Hex[] GetInfluencePositions(Hex playerPosition, Hex dropPosition)
            => new Hex[] { playerPosition, dropPosition };

        public override Hex[] GetValidDropPositions(Hex playerPosition)
        {
            Hex[] piecePositions = _board.GetOccupiedPositions();
            Hex[] enemyPositions = piecePositions.Except(new Hex[] { playerPosition }).ToArray();
            return enemyPositions;
        }

        protected override Dictionary<Hex, Hex> GetMoves(Hex playerPosition, Hex dropPosition)
        {
            Hex emptyPosition = _board.GetEmptyPositions()[0];
            return new Dictionary<Hex, Hex>
            {
                {playerPosition, emptyPosition},
                {dropPosition, playerPosition},
                {emptyPosition, dropPosition}
            };
        }

        protected override Hex[] GetTakes(Hex playerPosition, Hex dropPosition)
            => Array.Empty<Hex>();
    }
}
