﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.Domain;
using ECom.ReadModel;
using ECom.ReadModel.Parsers;
using ECom.Messages;

namespace ECom.Site
{
    public class ServiceLocator
    {
        public static Bus.Bus Bus { get; set; }
        public static IEventStore EventStore { get; set; }
        public static IDtoManager DtoManager { get; set; }
		public static ProductPageParserFactory ProductPageParserFactory = new ProductPageParserFactory();
		public static IDomainIdentityGenerator IdentityGenerator { get; set; }
    }
}