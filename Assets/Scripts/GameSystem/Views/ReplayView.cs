using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem.Views
{
    public class ReplayView : MonoBehaviour
    {
        public event EventHandler ClickUndo;
        public event EventHandler ClickRedo;

        public void OnClickUndo() => OnClickUndo(EventArgs.Empty);
        public void OnClickRedo() => OnClickRedo(EventArgs.Empty);

        protected virtual void OnClickUndo(EventArgs eventArgs)
        {
            var handler = ClickUndo;
            handler?.Invoke(this, eventArgs);
        }
        protected virtual void OnClickRedo(EventArgs eventArgs)
        {
            var handler = ClickRedo;
            handler?.Invoke(this, eventArgs);
        }
    }
}
