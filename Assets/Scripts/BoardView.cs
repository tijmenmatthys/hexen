using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    public event EventHandler<BoardClickEventArgs> Click;

    [SerializeField] private int _size = 5;
    [SerializeField] GameObject _TilePrefab;

    public int Size { get { return _size; } }

    public void TileClicked(TileView tileView)
    {
        OnClick(new BoardClickEventArgs(tileView.HexPosition));
    }

    private void OnClick(BoardClickEventArgs e)
    {
        var handler = Click;
        handler?.Invoke(this, e);
    }

    public void ResetBoard()
    {
        RemoveTiles();
        CreateTiles();
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
}

public class BoardClickEventArgs : EventArgs
{
    public Hex Position { get; }
    public BoardClickEventArgs(Hex position)
    {
        Position = position;
    }
}
