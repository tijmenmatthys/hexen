using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandQueueLib
{
    public class DelegateCommand : ICommand
    {
        private Func<bool> _commit;
        private Func<bool> _rollback;

        public DelegateCommand(Func<bool> commit, Func<bool> rollback)
        {
            _commit = commit;
            _rollback = rollback;
        }

        public bool Commit() => _commit();

        public bool Rollback() => _rollback();
    }
}
