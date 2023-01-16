using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuView : MonoBehaviour
{
    public event EventHandler MainMenuClicked;

    public void MainMenu()
    {
        OnMainMenuClicked(EventArgs.Empty);
    }

    protected virtual void OnMainMenuClicked(EventArgs eventArgs)
    {
        var handler = MainMenuClicked;
        handler?.Invoke(this, eventArgs);
    }
}
