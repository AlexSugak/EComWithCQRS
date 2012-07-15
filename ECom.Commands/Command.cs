using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Commands
{
    [Serializable]
    public class Command : ICommand
    {
        public Guid Id { get; private set; }

        public Command()
        {
            Id = Guid.NewGuid();
        }

        public Command(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("command id cannot be empty Guid");

            Id = id;
        }
    }
}
