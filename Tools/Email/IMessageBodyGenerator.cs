using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
	public interface IMessageBodyGenerator
	{
		string Generate<TData>(string template, TData data) where TData : class;
	}
}
