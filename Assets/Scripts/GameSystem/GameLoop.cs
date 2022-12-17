using BoardSystem;
using GameSystem.GameStates;
using Helper;
using HexenSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private StateMachine<GameStateType> _gameStateMachine;

        void Start()
        {
            _gameStateMachine = new StateMachine<GameStateType>();
            _gameStateMachine.Register(GameStateType.Play, new PlayState());
            _gameStateMachine.Register(GameStateType.MainMenu, new MainMenuState());
            _gameStateMachine.SetInitialStateID(GameStateType.MainMenu);
        }

        
    }
}
