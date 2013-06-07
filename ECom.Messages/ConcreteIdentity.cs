using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ECom.Utility;
using System.Runtime.Serialization;

namespace ECom.Messages
{
    [DataContract(IsReference=false)]
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

    [DataContract]
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

    [DataContract]
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

    [DataContract]
	public abstract class IntIdentity : AbstractIdentity<int>
	{
		public IntIdentity() { }

		public IntIdentity(int id)
		{
			Id = id;
		}

		public override int Id { get; protected set; }
	}

    [DataContract]
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

    [DataContract]
	public sealed class ProductId : StringIdentity
	{
        public const string TagValue = "product";

		public ProductId() { }

        public ProductId(string id)
            : base(id)
        {
        }

		public override string GetTag()
		{
			return TagValue;
		}
	}

    [DataContract]
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

    [DataContract]
	public sealed class OrderId : IntIdentity
	{
        public const string TagValue = "order";

		public OrderId() { }
		public OrderId(int id)
			: base(id)
		{
		}

		public override string GetTag()
		{
			return TagValue;
		}
	}

    [DataContract]
	public sealed class OrderItemId : IntIdentity
	{
		public const string TagValue = "order-item";

		public OrderItemId() { }
		public OrderItemId(int itemId)
			: base(itemId)
		{
		}

		public override string GetTag()
		{
			return TagValue;
		}
	}

    [DataContract]
	public sealed class UserId : StringIdentity
	{
		public const string TagValue = "user";

		public UserId(string email)
			: base(email)
		{
		}

		public override string GetTag()
		{
			return TagValue;
		}
	}


}
