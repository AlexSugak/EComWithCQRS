using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using System.Web.Mvc;

namespace ECom.Messages
{
    public interface ICommand : IMessage
    { }

    public interface ICommand<out T> : ICommand
        where T : IIdentity
    {
        T Id { get; }
        int Version { get; set; }
    }

    public interface IFunctionalCommand : ICommand
    {
    }
}
