using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandQueueLib
{
    public interface ICommand
    {
        public bool Commit();

        public bool Rollback();
    }
}
