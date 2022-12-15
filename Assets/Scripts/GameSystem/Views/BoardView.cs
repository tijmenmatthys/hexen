using BoardSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class BoardView : MonoBehaviour
    {
        public event EventHandler<BoardClickEventArgs> Click;

        [SerializeField] private int _size = 5;
        [SerializeField] private GameObject _TilePrefab;

        private readonly Dictionary<Hex, TileView> _tiles = new Dictionary<Hex,TileView>();
        private Hex[] _highlightedPositions = Array.Empty<Hex>();

        public int Size { get { return _size; } }

        private void OnEnable()
        {
            foreach (TileView tile in GetComponentsInChildren<TileView>())
                _tiles[tile.HexPosition] = tile;
        }

        public void TileClicked(TileView tileView)
        {
            OnClick(new BoardClickEventArgs(tileView.HexPosition));
        }
        public void ResetBoard()
        {
            RemoveTiles();
            CreateTiles();
        }

        public void DeHighlightTiles()
        {
            foreach (var tile in _tiles.Values)
                tile.Dehighlight();
        }
        public void HighlightInfluenceTiles(Hex[] positions)
            => HighlightTiles(positions, true);

        public void HighlightValidDropTiles(Hex[] positions)
            => HighlightTiles(positions, false);

        private void HighlightTiles(Hex[] positions, bool IsHighlightInfluence)
        {
            foreach (var position in _highlightedPositions)
                _tiles[position].Dehighlight();

            _highlightedPositions = positions;

            foreach (var position in _highlightedPositions)
            {
                if (IsHighlightInfluence) _tiles[position].HighlightInfluence();
                else _tiles[position].HighlightValidDrop();
            }
        }

        private void RemoveTiles()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(transform.GetChild(i).gameObject);
        }

        private void CreateTiles()
        {
            foreach (Hex hex in Hex.zero.GetNeighboursInRange(_size))
            {
                GameObject tile = Instantiate(_TilePrefab, transform);
                tile.transform.position = hex.WorldPosition;
                tile.name = "Tile" + hex.ToString();
            }
        }

        // not used in Hexen, but keep it in case we need it later
        private void OnClick(BoardClickEventArgs e)
        {
            var handler = Click;
            handler?.Invoke(this, e);
        }
    }

    public class BoardClickEventArgs : EventArgs
    {
        public Hex Position { get; }
        public BoardClickEventArgs(Hex position)
        {
            Position = position;
        }
    }
}
