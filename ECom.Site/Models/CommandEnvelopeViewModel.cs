using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.Messages;
using ECom.Utility;

namespace ECom.Site.Models
{
    public interface ICommandEnvelopeViewModel
    {
        ICommand Command { get; set; }
        string Id { get; set; }
    }

    public class CommandEnvelopeViewModel<T> : ICommandEnvelopeViewModel
        where T : ICommand
    {
        public CommandEnvelopeViewModel()
        {
        }

        public CommandEnvelopeViewModel(T cmd, IIdentity id)
            : this(cmd, id != null ? id.GetId() : null)
        {
        }

        public CommandEnvelopeViewModel(T cmd, string id)
        {
            Argument.ExpectNotNull(() => cmd);

            Command = cmd;
            Id = id;
        }

        public T Command { get; set; }

        public string Id { get; set; }

        ICommand ICommandEnvelopeViewModel.Command
        {
            get
            {
                return Command;
            }
            set
            {
                Command = (T)value;
            }
        }
    }

    public static class CommandEnvelopeExtentions
    {
        public static CommandEnvelopeViewModel<T> WithEnvelope<T>(this T cmd)
            where T : ICommand<IIdentity>
        {
            return new CommandEnvelopeViewModel<T>(cmd, cmd.Id);
        }

        public static CommandEnvelopeViewModel<T> WithEnvelope<T>(this T cmd, string id)
            where T : ICommand
        {
            return new CommandEnvelopeViewModel<T>(cmd, id);
        }
    }
}