using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineLib
{
    public abstract class State<TStateID>
    {
        public StateMachine<TStateID> StateMachine { get; set; }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnSuspend() { }
        public virtual void OnResume() { }
    }
}
