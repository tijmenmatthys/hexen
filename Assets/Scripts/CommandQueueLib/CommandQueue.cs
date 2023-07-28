using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandQueueLib
{
    public class CommandQueue
    {
        public event EventHandler Changed;

        private List<ICommand> _commands = new List<ICommand>();
        private int _currentCommand = -1;

        public bool IsAtStart => _currentCommand < 0;
        public bool IsAtEnd => _currentCommand >= _commands.Count - 1;

        public bool Execute(ICommand command)
        {
            if (command.Commit())
            {
                _commands.Add(command);
                _currentCommand++;
                OnChanged(EventArgs.Empty);
                return true;
            }
            return false;
        }

        public void ToEnd()
        {
            while (!IsAtEnd) Redo();
        }

        public void Undo()
        {
            Debug.Log($"Command Queue Undo - current command id = {_currentCommand}");
            if (IsAtStart) return;

            _commands[_currentCommand].Rollback();
            _currentCommand--;
            OnChanged(EventArgs.Empty);
        }

        public void Redo()
        {
            Debug.Log($"Command Queue Redo - current command id = {_currentCommand}");
            if (IsAtEnd) return;

            _currentCommand++;
            _commands[_currentCommand].Commit();
            OnChanged(EventArgs.Empty);
        }

        protected virtual void OnChanged(EventArgs eventArgs)
        {
            var handler = Changed;
            handler?.Invoke(this, eventArgs);
        }
    }
}
