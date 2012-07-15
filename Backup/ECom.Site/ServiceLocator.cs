using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel;

namespace ECom.Site
{
    public class ServiceLocator
    {
        public static Bus.Bus Bus { get; set; }
        public static IReadModelFacade ReadModel { get; set; }
    }
}