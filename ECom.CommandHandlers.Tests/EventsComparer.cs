using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using System.Reflection;

namespace ECom.CommandHandlers.Tests
{
    internal class AssertEvents
    {
        public static bool AreSame(IEnumerable<IEvent> occured, IEnumerable<IEvent> expected)
        {
            var occuredEvents = occured.ToArray();
            var expectedEvents = expected.ToArray();

            if (occuredEvents.Length != expectedEvents.Length)
            {
                return false;
            }

            for (int i = 0; i < occuredEvents.Length; i++)
            {
                var left = occuredEvents[i];
                var right = expectedEvents[i];
                if (left.GetType() != right.GetType())
                {
                    return false;
                }

                if (PublicInstancePropertiesEqual(left, right))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private static bool PublicInstancePropertiesEqual<T>(T left, T right, params string[] ignore) where T : class
        {
            if (left != null && right != null)
            {
                Type type = left.GetType();
                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(left, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(right, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }

                foreach (FieldInfo fi in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(fi.Name))
                    {
                        object selfValue = type.GetField(fi.Name).GetValue(left);
                        object toValue = type.GetField(fi.Name).GetValue(right);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return left == right;
        }
    }
}
