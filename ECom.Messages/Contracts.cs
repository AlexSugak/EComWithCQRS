
using System;
using System.CodeDom.Compiler;

namespace ECom.Messages 
{
		
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class UserCreated : IEvent<UserId>
    //{
    //    public UserId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public UserCreated () {}
    //    public UserCreated (UserId userId)
    //    {
    //        Id = userId;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as UserCreated;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return UserId.Equals(this.Id, other.Id);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(UserId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(UserCreated a, UserCreated b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(UserCreated a, UserCreated b)
    //    {
    //        return !(a == b);
    //    }
    //}

    [Serializable]
    [GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    public sealed class ReportUserLoggedIn : ICommand<UserId>
    {
        public UserId Id { get; set; }
        public int Version { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public ReportUserLoggedIn() { }
        public ReportUserLoggedIn(UserId userId, string userName, string photoUrl)
        {
            Id = userId;
            UserName = userName;
            PhotoUrl = photoUrl;
        }
        public override bool Equals(object obj)
        {
            var other = obj as ReportUserLoggedIn;
            if (other == null)
            {
                return false;
            }
            return UserId.Equals(this.Id, other.Id) && string.Equals(this.UserName, other.UserName) && string.Equals(this.PhotoUrl, other.PhotoUrl);
        }
        public override int GetHashCode()
        {
            var hash = 17;
            if (typeof(UserId).IsValueType || (Id != null))
                hash = hash * 29 + Id.GetHashCode();
            if (typeof(string).IsValueType || (UserName != null))
                hash = hash * 29 + UserName.GetHashCode();
            if (typeof(string).IsValueType || (PhotoUrl != null))
                hash = hash * 29 + PhotoUrl.GetHashCode();
            return hash;
        }
        public static bool operator ==(ReportUserLoggedIn a, ReportUserLoggedIn b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }
        public static bool operator !=(ReportUserLoggedIn a, ReportUserLoggedIn b)
        {
            return !(a == b);
        }
    }
	
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class UserLoggedInReported : IEvent<UserId>
    //{
    //    public UserId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public string UserName { get; set; }
    //    public string PhotoUrl { get; set; }
    //    public UserLoggedInReported () {}
    //    public UserLoggedInReported (UserId userId, string userName, string photoUrl)
    //    {
    //        Id = userId;
    //        UserName = userName;
    //        PhotoUrl = photoUrl;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as UserLoggedInReported;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return UserId.Equals(this.Id, other.Id) && string.Equals(this.UserName, other.UserName) && string.Equals(this.PhotoUrl, other.PhotoUrl);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(UserId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        if (typeof(string).IsValueType || (UserName != null)) 
    //            hash = hash * 29 + UserName.GetHashCode();
    //        if (typeof(string).IsValueType || (PhotoUrl != null)) 
    //            hash = hash * 29 + PhotoUrl.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(UserLoggedInReported a, UserLoggedInReported b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(UserLoggedInReported a, UserLoggedInReported b)
    //    {
    //        return !(a == b);
    //    }
    //}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class SetUserEmail : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public EmailAddress Email { get; set; }
		public SetUserEmail () {}
		public SetUserEmail (UserId userId, EmailAddress email)
		{
			Id = userId;
			Email = email;
		}
		public override bool Equals(object obj)
		{
			var other = obj as SetUserEmail;
			if (other == null)
			{
				return false;
			}
			return UserId.Equals(this.Id, other.Id) && EmailAddress.Equals(this.Email, other.Email);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(UserId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(EmailAddress).IsValueType || (Email != null)) 
				hash = hash * 29 + Email.GetHashCode();
			return hash;
		}
		public static bool operator ==(SetUserEmail a, SetUserEmail b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(SetUserEmail a, SetUserEmail b)
		{
			return !(a == b);
		}
	}
	
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class UserEmailSet : IEvent<UserId>
    //{
    //    public UserId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public EmailAddress Email { get; set; }
    //    public UserEmailSet () {}
    //    public UserEmailSet (UserId userId, EmailAddress email)
    //    {
    //        Id = userId;
    //        Email = email;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as UserEmailSet;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return UserId.Equals(this.Id, other.Id) && EmailAddress.Equals(this.Email, other.Email);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(UserId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        if (typeof(EmailAddress).IsValueType || (Email != null)) 
    //            hash = hash * 29 + Email.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(UserEmailSet a, UserEmailSet b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(UserEmailSet a, UserEmailSet b)
    //    {
    //        return !(a == b);
    //    }
    //}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CreateNewOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public UserId UserId { get; set; }
		public CreateNewOrder () {}
		public CreateNewOrder (OrderId orderId, UserId userId)
		{
			Id = orderId;
			UserId = userId;
		}
		public override bool Equals(object obj)
		{
			var other = obj as CreateNewOrder;
			if (other == null)
			{
				return false;
			}
			return OrderId.Equals(this.Id, other.Id) && UserId.Equals(this.UserId, other.UserId);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(OrderId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(UserId).IsValueType || (UserId != null)) 
				hash = hash * 29 + UserId.GetHashCode();
			return hash;
		}
		public static bool operator ==(CreateNewOrder a, CreateNewOrder b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(CreateNewOrder a, CreateNewOrder b)
		{
			return !(a == b);
		}
	}
	
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class NewOrderCreated : IEvent<OrderId>
    //{
    //    public OrderId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public UserId UserId { get; set; }
    //    public NewOrderCreated () {}
    //    public NewOrderCreated (OrderId orderId, UserId userId)
    //    {
    //        Id = orderId;
    //        UserId = userId;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as NewOrderCreated;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return OrderId.Equals(this.Id, other.Id) && UserId.Equals(this.UserId, other.UserId);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(OrderId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        if (typeof(UserId).IsValueType || (UserId != null)) 
    //            hash = hash * 29 + UserId.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(NewOrderCreated a, NewOrderCreated b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(NewOrderCreated a, NewOrderCreated b)
    //    {
    //        return !(a == b);
    //    }
    //}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AddProductToOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public OrderItemId OrderItemId { get; set; }
		public Uri ProductUri { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public Uri ImageUri { get; set; }
		public AddProductToOrder () {}
		public AddProductToOrder (OrderId orderId, OrderItemId orderItemId, Uri productUri, string name, string description, decimal price, int quantity, string size, string color, Uri imageUri)
		{
			Id = orderId;
			OrderItemId = orderItemId;
			ProductUri = productUri;
			Name = name;
			Description = description;
			Price = price;
			Quantity = quantity;
			Size = size;
			Color = color;
			ImageUri = imageUri;
		}
		public override bool Equals(object obj)
		{
			var other = obj as AddProductToOrder;
			if (other == null)
			{
				return false;
			}
			return OrderId.Equals(this.Id, other.Id) && OrderItemId.Equals(this.OrderItemId, other.OrderItemId) && Uri.Equals(this.ProductUri, other.ProductUri) && string.Equals(this.Name, other.Name) && string.Equals(this.Description, other.Description) && decimal.Equals(this.Price, other.Price) && int.Equals(this.Quantity, other.Quantity) && string.Equals(this.Size, other.Size) && string.Equals(this.Color, other.Color) && Uri.Equals(this.ImageUri, other.ImageUri);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(OrderId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(OrderItemId).IsValueType || (OrderItemId != null)) 
				hash = hash * 29 + OrderItemId.GetHashCode();
			if (typeof(Uri).IsValueType || (ProductUri != null)) 
				hash = hash * 29 + ProductUri.GetHashCode();
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			if (typeof(string).IsValueType || (Description != null)) 
				hash = hash * 29 + Description.GetHashCode();
			if (typeof(decimal).IsValueType || (Price != null)) 
				hash = hash * 29 + Price.GetHashCode();
			if (typeof(int).IsValueType || (Quantity != null)) 
				hash = hash * 29 + Quantity.GetHashCode();
			if (typeof(string).IsValueType || (Size != null)) 
				hash = hash * 29 + Size.GetHashCode();
			if (typeof(string).IsValueType || (Color != null)) 
				hash = hash * 29 + Color.GetHashCode();
			if (typeof(Uri).IsValueType || (ImageUri != null)) 
				hash = hash * 29 + ImageUri.GetHashCode();
			return hash;
		}
		public static bool operator ==(AddProductToOrder a, AddProductToOrder b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(AddProductToOrder a, AddProductToOrder b)
		{
			return !(a == b);
		}
	}
	
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class ProductAddedToOrder : IEvent<OrderId>
    //{
    //    public OrderId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public OrderItemId OrderItemId { get; set; }
    //    public Uri ProductUri { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public decimal Price { get; set; }
    //    public int Quantity { get; set; }
    //    public string Size { get; set; }
    //    public string Color { get; set; }
    //    public Uri ImageUri { get; set; }
    //    public ProductAddedToOrder () {}
    //    public ProductAddedToOrder (OrderId orderId, OrderItemId orderItemId, Uri productUri, string name, string description, decimal price, int quantity, string size, string color, Uri imageUri)
    //    {
    //        Id = orderId;
    //        OrderItemId = orderItemId;
    //        ProductUri = productUri;
    //        Name = name;
    //        Description = description;
    //        Price = price;
    //        Quantity = quantity;
    //        Size = size;
    //        Color = color;
    //        ImageUri = imageUri;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as ProductAddedToOrder;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return OrderId.Equals(this.Id, other.Id) && OrderItemId.Equals(this.OrderItemId, other.OrderItemId) && Uri.Equals(this.ProductUri, other.ProductUri) && string.Equals(this.Name, other.Name) && string.Equals(this.Description, other.Description) && decimal.Equals(this.Price, other.Price) && int.Equals(this.Quantity, other.Quantity) && string.Equals(this.Size, other.Size) && string.Equals(this.Color, other.Color) && Uri.Equals(this.ImageUri, other.ImageUri);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(OrderId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        if (typeof(OrderItemId).IsValueType || (OrderItemId != null)) 
    //            hash = hash * 29 + OrderItemId.GetHashCode();
    //        if (typeof(Uri).IsValueType || (ProductUri != null)) 
    //            hash = hash * 29 + ProductUri.GetHashCode();
    //        if (typeof(string).IsValueType || (Name != null)) 
    //            hash = hash * 29 + Name.GetHashCode();
    //        if (typeof(string).IsValueType || (Description != null)) 
    //            hash = hash * 29 + Description.GetHashCode();
    //        if (typeof(decimal).IsValueType || (Price != null)) 
    //            hash = hash * 29 + Price.GetHashCode();
    //        if (typeof(int).IsValueType || (Quantity != null)) 
    //            hash = hash * 29 + Quantity.GetHashCode();
    //        if (typeof(string).IsValueType || (Size != null)) 
    //            hash = hash * 29 + Size.GetHashCode();
    //        if (typeof(string).IsValueType || (Color != null)) 
    //            hash = hash * 29 + Color.GetHashCode();
    //        if (typeof(Uri).IsValueType || (ImageUri != null)) 
    //            hash = hash * 29 + ImageUri.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(ProductAddedToOrder a, ProductAddedToOrder b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(ProductAddedToOrder a, ProductAddedToOrder b)
    //    {
    //        return !(a == b);
    //    }
    //}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RemoveItemFromOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public OrderItemId OrderItemId { get; set; }
		public RemoveItemFromOrder () {}
		public RemoveItemFromOrder (OrderId orderId, OrderItemId orderItemId)
		{
			Id = orderId;
			OrderItemId = orderItemId;
		}
		public override bool Equals(object obj)
		{
			var other = obj as RemoveItemFromOrder;
			if (other == null)
			{
				return false;
			}
			return OrderId.Equals(this.Id, other.Id) && OrderItemId.Equals(this.OrderItemId, other.OrderItemId);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(OrderId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(OrderItemId).IsValueType || (OrderItemId != null)) 
				hash = hash * 29 + OrderItemId.GetHashCode();
			return hash;
		}
		public static bool operator ==(RemoveItemFromOrder a, RemoveItemFromOrder b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(RemoveItemFromOrder a, RemoveItemFromOrder b)
		{
			return !(a == b);
		}
	}
	
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class ItemRemovedFromOrder : IEvent<OrderId>
    //{
    //    public OrderId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public OrderItemId OrderItemId { get; set; }
    //    public ItemRemovedFromOrder () {}
    //    public ItemRemovedFromOrder (OrderId orderId, OrderItemId orderItemId)
    //    {
    //        Id = orderId;
    //        OrderItemId = orderItemId;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as ItemRemovedFromOrder;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return OrderId.Equals(this.Id, other.Id) && OrderItemId.Equals(this.OrderItemId, other.OrderItemId);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(OrderId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        if (typeof(OrderItemId).IsValueType || (OrderItemId != null)) 
    //            hash = hash * 29 + OrderItemId.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(ItemRemovedFromOrder a, ItemRemovedFromOrder b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(ItemRemovedFromOrder a, ItemRemovedFromOrder b)
    //    {
    //        return !(a == b);
    //    }
    //}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class SubmitOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public SubmitOrder () {}
		public SubmitOrder (OrderId orderId)
		{
			Id = orderId;
		}
		public override bool Equals(object obj)
		{
			var other = obj as SubmitOrder;
			if (other == null)
			{
				return false;
			}
			return OrderId.Equals(this.Id, other.Id);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(OrderId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			return hash;
		}
		public static bool operator ==(SubmitOrder a, SubmitOrder b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(SubmitOrder a, SubmitOrder b)
		{
			return !(a == b);
		}
	}
	
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class OrderSubmited : IEvent<OrderId>
    //{
    //    public OrderId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public UserId UserId { get; set; }
    //    public OrderSubmited () {}
    //    public OrderSubmited (OrderId orderId, UserId userId)
    //    {
    //        Id = orderId;
    //        UserId = userId;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as OrderSubmited;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return OrderId.Equals(this.Id, other.Id) && UserId.Equals(this.UserId, other.UserId);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(OrderId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        if (typeof(UserId).IsValueType || (UserId != null)) 
    //            hash = hash * 29 + UserId.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(OrderSubmited a, OrderSubmited b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(OrderSubmited a, OrderSubmited b)
    //    {
    //        return !(a == b);
    //    }
    //}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AddProduct : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public AddProduct () {}
		public AddProduct (ProductId productId, string name, decimal price)
		{
			Id = productId;
			Name = name;
			Price = price;
		}
		public override bool Equals(object obj)
		{
			var other = obj as AddProduct;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id) && string.Equals(this.Name, other.Name) && decimal.Equals(this.Price, other.Price);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			if (typeof(decimal).IsValueType || (Price != null)) 
				hash = hash * 29 + Price.GetHashCode();
			return hash;
		}
		public static bool operator ==(AddProduct a, AddProduct b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(AddProduct a, AddProduct b)
		{
			return !(a == b);
		}
	}
	
    //[Serializable]
    //[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
    //public sealed class ProductAdded : IEvent<ProductId>
    //{
    //    public ProductId Id { get; set; }
    //    public int Version { get; set; }
    //    public DateTime Date { get; set; }
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }
    //    public ProductAdded () {}
    //    public ProductAdded (ProductId productId, string name, decimal price)
    //    {
    //        Id = productId;
    //        Name = name;
    //        Price = price;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as ProductAdded;
    //        if (other == null)
    //        {
    //            return false;
    //        }
    //        return ProductId.Equals(this.Id, other.Id) && string.Equals(this.Name, other.Name) && decimal.Equals(this.Price, other.Price);
    //    }
    //    public override int GetHashCode()
    //    {
    //        var hash = 17;
    //        if (typeof(ProductId).IsValueType || (Id != null)) 
    //            hash = hash * 29 + Id.GetHashCode();
    //        if (typeof(string).IsValueType || (Name != null)) 
    //            hash = hash * 29 + Name.GetHashCode();
    //        if (typeof(decimal).IsValueType || (Price != null)) 
    //            hash = hash * 29 + Price.GetHashCode();
    //        return hash;
    //    }
    //    public static bool operator ==(ProductAdded a, ProductAdded b)
    //    {
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }
    //        return a.Equals(b);
    //    }
    //    public static bool operator !=(ProductAdded a, ProductAdded b)
    //    {
    //        return !(a == b);
    //    }
    //}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RemoveProduct : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public RemoveProduct () {}
		public RemoveProduct (ProductId productId)
		{
			Id = productId;
		}
		public override bool Equals(object obj)
		{
			var other = obj as RemoveProduct;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			return hash;
		}
		public static bool operator ==(RemoveProduct a, RemoveProduct b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(RemoveProduct a, RemoveProduct b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductRemoved : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public ProductRemoved () {}
		public ProductRemoved (ProductId productId)
		{
			Id = productId;
		}
		public override bool Equals(object obj)
		{
			var other = obj as ProductRemoved;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			return hash;
		}
		public static bool operator ==(ProductRemoved a, ProductRemoved b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(ProductRemoved a, ProductRemoved b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ChangeProductPrice : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public decimal NewPrice { get; set; }
		public ChangeProductPrice () {}
		public ChangeProductPrice (ProductId productId, decimal newPrice)
		{
			Id = productId;
			NewPrice = newPrice;
		}
		public override bool Equals(object obj)
		{
			var other = obj as ChangeProductPrice;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id) && decimal.Equals(this.NewPrice, other.NewPrice);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(decimal).IsValueType || (NewPrice != null)) 
				hash = hash * 29 + NewPrice.GetHashCode();
			return hash;
		}
		public static bool operator ==(ChangeProductPrice a, ChangeProductPrice b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(ChangeProductPrice a, ChangeProductPrice b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductPriceChanged : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public decimal NewPrice { get; set; }
		public ProductPriceChanged () {}
		public ProductPriceChanged (ProductId productId, decimal newPrice)
		{
			Id = productId;
			NewPrice = newPrice;
		}
		public override bool Equals(object obj)
		{
			var other = obj as ProductPriceChanged;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id) && decimal.Equals(this.NewPrice, other.NewPrice);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(decimal).IsValueType || (NewPrice != null)) 
				hash = hash * 29 + NewPrice.GetHashCode();
			return hash;
		}
		public static bool operator ==(ProductPriceChanged a, ProductPriceChanged b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(ProductPriceChanged a, ProductPriceChanged b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AddRelatedProduct : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public ProductId TargetProductId { get; set; }
		public AddRelatedProduct () {}
		public AddRelatedProduct (ProductId productId, ProductId targetProductId)
		{
			Id = productId;
			TargetProductId = targetProductId;
		}
		public override bool Equals(object obj)
		{
			var other = obj as AddRelatedProduct;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id) && ProductId.Equals(this.TargetProductId, other.TargetProductId);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(ProductId).IsValueType || (TargetProductId != null)) 
				hash = hash * 29 + TargetProductId.GetHashCode();
			return hash;
		}
		public static bool operator ==(AddRelatedProduct a, AddRelatedProduct b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(AddRelatedProduct a, AddRelatedProduct b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RelatedProductAdded : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public ProductId TargetProductId { get; set; }
		public RelatedProductAdded () {}
		public RelatedProductAdded (ProductId productId, ProductId targetProductId)
		{
			Id = productId;
			TargetProductId = targetProductId;
		}
		public override bool Equals(object obj)
		{
			var other = obj as RelatedProductAdded;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id) && ProductId.Equals(this.TargetProductId, other.TargetProductId);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(ProductId).IsValueType || (TargetProductId != null)) 
				hash = hash * 29 + TargetProductId.GetHashCode();
			return hash;
		}
		public static bool operator ==(RelatedProductAdded a, RelatedProductAdded b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(RelatedProductAdded a, RelatedProductAdded b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CreateCategory : IFunctionalCommand
	{
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public CreateCategory () {}
		public CreateCategory (string name)
		{
			Name = name;
		}
		public override bool Equals(object obj)
		{
			var other = obj as CreateCategory;
			if (other == null)
			{
				return false;
			}
			return string.Equals(this.Name, other.Name);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			return hash;
		}
		public static bool operator ==(CreateCategory a, CreateCategory b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(CreateCategory a, CreateCategory b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CategoryCreated : IEvent<CatalogId>
	{
		public CatalogId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public CategoryCreated () {}
		public CategoryCreated (CatalogId catalogId, string name)
		{
			Id = catalogId;
			Name = name;
		}
		public override bool Equals(object obj)
		{
			var other = obj as CategoryCreated;
			if (other == null)
			{
				return false;
			}
			return CatalogId.Equals(this.Id, other.Id) && string.Equals(this.Name, other.Name);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(CatalogId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			return hash;
		}
		public static bool operator ==(CategoryCreated a, CategoryCreated b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(CategoryCreated a, CategoryCreated b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class MoveCategory : IFunctionalCommand
	{
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public string TargetCategory { get; set; }
		public MoveCategory () {}
		public MoveCategory (string name, string targetCategory)
		{
			Name = name;
			TargetCategory = targetCategory;
		}
		public override bool Equals(object obj)
		{
			var other = obj as MoveCategory;
			if (other == null)
			{
				return false;
			}
			return string.Equals(this.Name, other.Name) && string.Equals(this.TargetCategory, other.TargetCategory);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			if (typeof(string).IsValueType || (TargetCategory != null)) 
				hash = hash * 29 + TargetCategory.GetHashCode();
			return hash;
		}
		public static bool operator ==(MoveCategory a, MoveCategory b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(MoveCategory a, MoveCategory b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CategoryMoved : IEvent<CatalogId>
	{
		public CatalogId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public string TargetCategory { get; set; }
		public CategoryMoved () {}
		public CategoryMoved (CatalogId catalogId, string name, string targetCategory)
		{
			Id = catalogId;
			Name = name;
			TargetCategory = targetCategory;
		}
		public override bool Equals(object obj)
		{
			var other = obj as CategoryMoved;
			if (other == null)
			{
				return false;
			}
			return CatalogId.Equals(this.Id, other.Id) && string.Equals(this.Name, other.Name) && string.Equals(this.TargetCategory, other.TargetCategory);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(CatalogId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			if (typeof(string).IsValueType || (TargetCategory != null)) 
				hash = hash * 29 + TargetCategory.GetHashCode();
			return hash;
		}
		public static bool operator ==(CategoryMoved a, CategoryMoved b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(CategoryMoved a, CategoryMoved b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RemoveCategory : IFunctionalCommand
	{
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public RemoveCategory () {}
		public RemoveCategory (string name)
		{
			Name = name;
		}
		public override bool Equals(object obj)
		{
			var other = obj as RemoveCategory;
			if (other == null)
			{
				return false;
			}
			return string.Equals(this.Name, other.Name);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			return hash;
		}
		public static bool operator ==(RemoveCategory a, RemoveCategory b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(RemoveCategory a, RemoveCategory b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CategoryRemoved : IEvent<CatalogId>
	{
		public CatalogId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public CategoryRemoved () {}
		public CategoryRemoved (CatalogId catalogId, string name)
		{
			Id = catalogId;
			Name = name;
		}
		public override bool Equals(object obj)
		{
			var other = obj as CategoryRemoved;
			if (other == null)
			{
				return false;
			}
			return CatalogId.Equals(this.Id, other.Id) && string.Equals(this.Name, other.Name);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(CatalogId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(string).IsValueType || (Name != null)) 
				hash = hash * 29 + Name.GetHashCode();
			return hash;
		}
		public static bool operator ==(CategoryRemoved a, CategoryRemoved b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(CategoryRemoved a, CategoryRemoved b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AddProductToCategory : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string CategoryName { get; set; }
		public AddProductToCategory () {}
		public AddProductToCategory (ProductId productId, string categoryName)
		{
			Id = productId;
			CategoryName = categoryName;
		}
		public override bool Equals(object obj)
		{
			var other = obj as AddProductToCategory;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id) && string.Equals(this.CategoryName, other.CategoryName);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(string).IsValueType || (CategoryName != null)) 
				hash = hash * 29 + CategoryName.GetHashCode();
			return hash;
		}
		public static bool operator ==(AddProductToCategory a, AddProductToCategory b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(AddProductToCategory a, AddProductToCategory b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductAddedToCategory : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public DateTime Date { get; set; }
		public string CategoryName { get; set; }
		public ProductAddedToCategory () {}
		public ProductAddedToCategory (ProductId productId, string categoryName)
		{
			Id = productId;
			CategoryName = categoryName;
		}
		public override bool Equals(object obj)
		{
			var other = obj as ProductAddedToCategory;
			if (other == null)
			{
				return false;
			}
			return ProductId.Equals(this.Id, other.Id) && string.Equals(this.CategoryName, other.CategoryName);
		}
		public override int GetHashCode()
		{
			var hash = 17;
			if (typeof(ProductId).IsValueType || (Id != null)) 
				hash = hash * 29 + Id.GetHashCode();
			if (typeof(string).IsValueType || (CategoryName != null)) 
				hash = hash * 29 + CategoryName.GetHashCode();
			return hash;
		}
		public static bool operator ==(ProductAddedToCategory a, ProductAddedToCategory b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(ProductAddedToCategory a, ProductAddedToCategory b)
		{
			return !(a == b);
		}
	}

}