using CommandQueueLib;
using GameSystem.Views;
using StateMachineLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.GameStates
{
    public class ReplayState : State<GameStateType>
    {
        private readonly CommandQueue _commandQueue;

        private ReplayView _replayView;

        public ReplayState(CommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public override void OnEnter()
        {
            _replayView = GameObject.FindObjectOfType<ReplayView>();
            OnResume();
            _commandQueue.Undo();
        }

        public override void OnExit()
        {
            _commandQueue.ToEnd();
            OnSuspend();
        }

        public override void OnResume()
        {
            _replayView.ClickUndo += Undo;
            _replayView.ClickRedo += Redo;
        }

        public override void OnSuspend()
        {
            _replayView.ClickUndo -= Undo;
            _replayView.ClickRedo -= Redo;
        }
        private void Redo(object sender, EventArgs e)
        {
            _commandQueue.Redo();
            if (_commandQueue.IsAtEnd)
                StateMachine.Transition(GameStateType.Play, true);
        }

        private void Undo(object sender, EventArgs e)
        {
            _commandQueue.Undo();
        }
    }
}
