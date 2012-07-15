using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.Commands
{
    public interface ICommand : Message
    {
        Guid Id { get; }
    }
}
