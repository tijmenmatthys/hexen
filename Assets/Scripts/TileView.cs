using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileView : MonoBehaviour, IPointerClickHandler
{
    private BoardView _boardView;

    public Hex HexPosition => Hex.HexPosition(transform.position);

    private void Awake()
    {
        _boardView = GetComponentInParent<BoardView>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _boardView.TileClicked(this);
    }
}
