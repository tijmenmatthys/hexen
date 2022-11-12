using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var boardView = FindObjectOfType<BoardView>();
        boardView.Click += OnBoardClick;
    }

    private void OnBoardClick(object sender, BoardClickEventArgs e)
    {
        Debug.Log(e.Position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
