using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RazorEngine;
using Email;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace ECom.Site.Core
{
	public class RazorMessageBodyGenerator : IMessageBodyGenerator
	{
        public string Generate<TData>(string template, TData data) where TData : class
        {
            var config = new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(RazorEngine.Templating.HtmlTemplateBase<>)
            };

            using (var service = new TemplateService(config))
            {
                Razor.SetTemplateService(service);

                return Razor.Parse(template, data);
            }
        }
	}
}
