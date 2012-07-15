using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;

namespace ECom.Messages
{
    [Serializable]
    public sealed class NullId : IIdentity
    {
        public const string TagValue = "";
        public static readonly IIdentity Instance = new NullId();

        public string GetId()
        {
            return "";
        }

        public string GetTag()
        {
            return "";
        }
    }

    [Serializable]
	public abstract class GuidIdentity : AbstractIdentity<Guid>
	{
		public GuidIdentity() { }

		public GuidIdentity(Guid id)
		{
			Argument.ExpectNotEmptyGuid(() => id);
			Id = id;
		}

		public override Guid Id { get; protected set; }
	}

    [Serializable]
	public abstract class StringIdentity : AbstractIdentity<string>
	{
		public StringIdentity() { }

		public StringIdentity(string id)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => id);
			Id = id;
		}

		public override string Id { get; protected set; }
	}

    [Serializable]
	public sealed class CatalogId : GuidIdentity
	{
        public const string TagValue = "catalog";

		public CatalogId() { }
		public CatalogId(Guid id)
			: base(id)
		{
		}

		public override string GetTag()
		{
			return TagValue;
		}
	}

    [Serializable]
	public sealed class ProductId : GuidIdentity
	{
        public const string TagValue = "product";

		public ProductId() { }
		public ProductId(Guid id)
			: base(id)
		{
		}

        public ProductId(string id)
            : base(new Guid(id))
        {
        }

		public override string GetTag()
		{
			return TagValue;
		}
	}

    [Serializable]
	public sealed class DiscountId : GuidIdentity
	{
        public const string TagValue = "discount";

		public DiscountId() { }
		public DiscountId(Guid id)
			: base(id)
		{
		}

		public override string GetTag()
		{
			return TagValue;
		}
	}

    [Serializable]
	public sealed class OrderId : GuidIdentity
	{
        public const string TagValue = "order";

		public OrderId() { }
		public OrderId(Guid id)
			: base(id)
		{
		}

		public override string GetTag()
		{
			return TagValue;
		}
	}
}
