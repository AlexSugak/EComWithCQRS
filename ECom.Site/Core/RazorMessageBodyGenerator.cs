using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RazorEngine;

namespace Email
{
	public class RazorMessageBodyGenerator : IMessageBodyGenerator
	{
		public string Generate<TData>(string template, TData data) where TData : class
		{
			Razor.SetTemplateBase(typeof(HtmlTemplateBase<>));

			return Razor.Parse(template, data);
		}
	}
}
