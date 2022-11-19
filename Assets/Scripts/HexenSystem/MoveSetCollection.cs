using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexenSystem
{
    public class MoveSetCollection<TPiece>
    {
        private Dictionary<CardType, MoveSet<TPiece>> _moveSets
            = new Dictionary<CardType,MoveSet<TPiece>>();

        public bool TryGetMoveSet(CardType card, out MoveSet<TPiece> moveSet)
            => _moveSets.TryGetValue(card, out moveSet);
    }
}
