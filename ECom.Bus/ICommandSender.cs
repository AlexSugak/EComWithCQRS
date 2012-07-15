using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.Bus
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;
    }
}
