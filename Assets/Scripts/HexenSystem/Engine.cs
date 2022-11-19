using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexenSystem
{
    public class Engine<TPiece>
    {
        private readonly Board<TPiece> _board;

        public Engine(Board<TPiece> board)
        {
            _board = board;
        }

        public MoveSetCollection<TPiece> MoveSets
        {
            get { return null; } // TODO
        }

        public bool Move (Hex dropPosition, CardType card)
        {
            if (!_board.IsValid(dropPosition)) return false;
            if (!MoveSets.TryGetMoveSet(card, out MoveSet<TPiece> moveSet)) return false;
            if (!moveSet.GetValidDropPositions().Contains(dropPosition)) return false;

            return moveSet.Execute(dropPosition);
        }
    }
}
