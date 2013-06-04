using ECom.Infrastructure;
using ECom.Messages;
using ECom.ReadModel;
using ECom.ReadModel.Parsers;

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