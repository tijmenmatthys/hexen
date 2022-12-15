using BoardSystem;
using HexenSystem.MoveSets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HexenSystem
{
    public class Engine<TPiece>
        where TPiece : IPiece
    {
        private readonly Board<TPiece> _board;

        public MoveSetCollection<TPiece> MoveSets { get; }

        public Engine(Board<TPiece> board)
        {
            _board = board;
            MoveSets = new MoveSetCollection<TPiece>(board);
        }

        public bool PlayCard(Hex dropPosition, CardType card)
        {
            if (!_board.IsValid(dropPosition)) return false;
            if (!TryGetPlayerPosition(out Hex playerPosition)) return false;
            if (!MoveSets.TryGetMoveSet(card, out MoveSet<TPiece> moveSet)) return false;
            if (!moveSet.GetValidDropPositions(playerPosition).Contains(dropPosition)) return false;

            return moveSet.Execute(playerPosition, dropPosition);
        }

        public bool TryGetPlayerPosition(out Hex playerPosition)
        {
            foreach (Hex position in _board.GetOccupiedPositions())
            {
                if (!_board.TryGetPiece(position, out TPiece piece)) continue;
                if (piece.PlayerType == PlayerType.Player)
                {
                    playerPosition = position;
                    return true;
                }
            }
            playerPosition = Hex.zero;
            return false;
        }
    }
}
