using ECom.Domain;
using System;

namespace ECom.CommandHandlers.Tests
{
    internal static class StaticsInitializer
    {
        static StaticsInitializer()
        {
            TimeProvider.Current = () => DateTime.MinValue;
        }

        public static void Dummy()
        {
        }
    }
}
