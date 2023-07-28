using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public event EventHandler PlayClicked;

    public void Play()
    {
        OnPlayClicked(EventArgs.Empty);
    }

    protected virtual void OnPlayClicked(EventArgs eventArgs)
    {
        var handler = PlayClicked;
        handler?.Invoke(this, eventArgs);
    }
}
