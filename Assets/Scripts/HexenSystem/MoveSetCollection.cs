using BoardSystem;
using HexenSystem.MoveSets;
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

        public MoveSetCollection(Board<TPiece> board)
        {
            _moveSets.Add(CardType.Teleport, new TeleportMoveSet<TPiece>(board));
            _moveSets.Add(CardType.Slash, new SlashMoveSet<TPiece>(board));
            _moveSets.Add(CardType.Push, new PushMoveSet<TPiece>(board));
            _moveSets.Add(CardType.Shoot, new ShootMoveSet<TPiece>(board));
            _moveSets.Add(CardType.Bomb, new BombMoveSet<TPiece>(board));
        }

        public bool TryGetMoveSet(CardType card, out MoveSet<TPiece> moveSet)
            => _moveSets.TryGetValue(card, out moveSet);

        public MoveSet<TPiece> this[CardType card] => _moveSets[card];
    }
}
