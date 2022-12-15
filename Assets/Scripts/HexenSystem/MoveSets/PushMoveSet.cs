using BoardSystem;
using HexenSystem.MoveSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem.MoveSets
{
    public class PushMoveSet<TPiece> : MoveSet<TPiece>
    {
        public PushMoveSet(Board<TPiece> board) : base(board)
        {
        }

        public override Hex[] GetInfluencePositions(Hex playerPosition, Hex dropPosition)
            => RemoveInvalid(GetMoves(playerPosition, dropPosition).Keys.ToArray());

        public override Hex[] GetValidDropPositions(Hex playerPosition)
            => RemoveInvalid(playerPosition.Neighbours);

        protected override Dictionary<Hex, Hex> GetMoves(Hex playerPosition, Hex dropPosition)
        {
            Hex pushPosition = dropPosition + (dropPosition - playerPosition);
            return new Dictionary<Hex, Hex>
            {
                { dropPosition, pushPosition },
                { dropPosition.RotateLeftAround(playerPosition), pushPosition.RotateLeftAround(playerPosition) },
                { dropPosition.RotateRightAround(playerPosition), pushPosition.RotateRightAround(playerPosition) }
            };
        }

        protected override Hex[] GetTakes(Hex playerPosition, Hex dropPosition)
            => Array.Empty<Hex>();
    }
}
