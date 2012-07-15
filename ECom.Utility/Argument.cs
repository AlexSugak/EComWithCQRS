using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ECom.Utility
{
    public static class Argument
    {
        public static void ExpectNotNull(object argumentValue, string argumentName)
        {
			if (argumentValue == null)
			{
				throw new ArgumentNullException(argumentName);
			}
        }

		public static void ExpectNotNullOrWhiteSpace(string argumentValue, string argumentName)
        {
            if (String.IsNullOrWhiteSpace(argumentValue))
			{
				throw new ArgumentNullException(argumentName);
			}
        }

		public static void ExpectNotNull<T>(Expression<Func<T>> f)
        {
            var argumentName = (f.Body as MemberExpression).Member.Name;
            var func = f.Compile();
			if (func() == null)
			{
				throw new ArgumentNullException(argumentName);
			}
        }

		public static void ExpectNotNullOrWhiteSpace(Expression<Func<string>> f)
        {
            var argumentName = (f.Body as MemberExpression).Member.Name;
            var func = f.Compile();
			if (String.IsNullOrWhiteSpace(func()))
			{
				throw new ArgumentNullException(argumentName);
			}
        }

		public static void ExpectNotEmptyGuid(Expression<Func<Guid>> f)
        {
            var argumentName = (f.Body as MemberExpression).Member.Name;
            var func = f.Compile();
			if (func() == Guid.Empty)
			{
				throw new ArgumentException(string.Format("{0} cannot be an empty Guid", argumentName), argumentName);
			}
        }

        public static void Expect(Func<bool> condition, string paramName, string message)
        {
            if (!condition())
            {
                throw new ArgumentException(message, paramName);
            }
        }
    }
}
