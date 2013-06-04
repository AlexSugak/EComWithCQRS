using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Domain
{
    public static class TimeProvider
    {
        private static Func<DateTime> defaultProvider = () => DateTime.Now;
        private static Func<DateTime> current;

        public static Func<DateTime> Current
        {
            set
            {
                if (TimeProvider.current != null)
                {
                    throw new InvalidOperationException("Time provider is already set");
                }
                TimeProvider.current = value;
            }
        }

        public static DateTime Now
        {
            get
            {
                if (TimeProvider.current == null)
                    TimeProvider.current = TimeProvider.defaultProvider;

                return TimeProvider.current();
            }
        }
    }
}
