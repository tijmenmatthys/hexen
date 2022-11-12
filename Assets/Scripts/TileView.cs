using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileView : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler Click;
    public Hex HexPosition => Hex.FromWorldPosition(transform.position);

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick(EventArgs.Empty);
    }

    private void OnClick(EventArgs e)
    {
        var handler = Click;
        handler?.Invoke(this, e);
    }
}
