using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Messages
{
    public interface IHandle<T> where T : IMessage
    {
        void Handle(T message);
    }
}
