﻿


using System;
using System.CodeDom.Compiler;

namespace ECom.Messages 
{
	
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserCreated : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private UserCreated () {}
		public UserCreated (UserId userId)
		{
			Id = userId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserCreated;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		public static bool operator ==(UserCreated a, UserCreated b)
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
		public static bool operator !=(UserCreated a, UserCreated b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ReportUserLoggedIn : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private ReportUserLoggedIn () {}
		public ReportUserLoggedIn (UserId userId)
		{
			Id = userId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ReportUserLoggedIn;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
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
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserLoggedInReported : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private UserLoggedInReported () {}
		public UserLoggedInReported (UserId userId)
		{
			Id = userId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserLoggedInReported;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		public static bool operator ==(UserLoggedInReported a, UserLoggedInReported b)
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
		public static bool operator !=(UserLoggedInReported a, UserLoggedInReported b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class NewOrderCreated : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		private NewOrderCreated () {}
		public NewOrderCreated (OrderId orderId)
		{
			Id = orderId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as NewOrderCreated;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		public static bool operator ==(NewOrderCreated a, NewOrderCreated b)
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
		public static bool operator !=(NewOrderCreated a, NewOrderCreated b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AddProductToOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public Uri ProductUri { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public Uri ImageUri { get; set; }
		private AddProductToOrder () {}
		public AddProductToOrder (OrderId orderId, Uri productUri, string name, string description, decimal price, int quantity, string size, string color, Uri imageUri)
		{
			Id = orderId;
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
			if (obj == null)
			{
				return false;
			}
			var target = obj as AddProductToOrder;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && ProductUri.Equals(target.ProductUri) && Name.Equals(target.Name) && Description.Equals(target.Description) && Price.Equals(target.Price) && Quantity.Equals(target.Quantity) && Size.Equals(target.Size) && Color.Equals(target.Color) && ImageUri.Equals(target.ImageUri);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ ProductUri.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode() ^ Price.GetHashCode() ^ Quantity.GetHashCode() ^ Size.GetHashCode() ^ Color.GetHashCode() ^ ImageUri.GetHashCode();
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
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductAddedToOrder : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public OrderItemId OrderItemId { get; set; }
		public Uri ProductUri { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public Uri ImageUri { get; set; }
		private ProductAddedToOrder () {}
		public ProductAddedToOrder (OrderId orderId, OrderItemId orderItemId, Uri productUri, string name, string description, decimal price, int quantity, string size, string color, Uri imageUri)
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
			if (obj == null)
			{
				return false;
			}
			var target = obj as ProductAddedToOrder;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && OrderItemId.Equals(target.OrderItemId) && ProductUri.Equals(target.ProductUri) && Name.Equals(target.Name) && Description.Equals(target.Description) && Price.Equals(target.Price) && Quantity.Equals(target.Quantity) && Size.Equals(target.Size) && Color.Equals(target.Color) && ImageUri.Equals(target.ImageUri);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ OrderItemId.GetHashCode() ^ ProductUri.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode() ^ Price.GetHashCode() ^ Quantity.GetHashCode() ^ Size.GetHashCode() ^ Color.GetHashCode() ^ ImageUri.GetHashCode();
		}
		public static bool operator ==(ProductAddedToOrder a, ProductAddedToOrder b)
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
		public static bool operator !=(ProductAddedToOrder a, ProductAddedToOrder b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RemoveItemFromOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public OrderItemId OrderItemId { get; set; }
		private RemoveItemFromOrder () {}
		public RemoveItemFromOrder (OrderId orderId, OrderItemId orderItemId)
		{
			Id = orderId;
			OrderItemId = orderItemId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as RemoveItemFromOrder;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && OrderItemId.Equals(target.OrderItemId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ OrderItemId.GetHashCode();
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
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ItemRemovedFromOrder : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public OrderItemId OrderItemId { get; set; }
		private ItemRemovedFromOrder () {}
		public ItemRemovedFromOrder (OrderId orderId, OrderItemId orderItemId)
		{
			Id = orderId;
			OrderItemId = orderItemId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ItemRemovedFromOrder;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && OrderItemId.Equals(target.OrderItemId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ OrderItemId.GetHashCode();
		}
		public static bool operator ==(ItemRemovedFromOrder a, ItemRemovedFromOrder b)
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
		public static bool operator !=(ItemRemovedFromOrder a, ItemRemovedFromOrder b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class SubmitOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		private SubmitOrder () {}
		public SubmitOrder (OrderId orderId)
		{
			Id = orderId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as SubmitOrder;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
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
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class OrderSubmited : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		private OrderSubmited () {}
		public OrderSubmited (OrderId orderId)
		{
			Id = orderId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as OrderSubmited;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		public static bool operator ==(OrderSubmited a, OrderSubmited b)
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
		public static bool operator !=(OrderSubmited a, OrderSubmited b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AddProduct : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		private AddProduct () {}
		public AddProduct (ProductId productId, string name, decimal price)
		{
			Id = productId;
			Name = name;
			Price = price;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as AddProduct;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name) && Price.Equals(target.Price);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode() ^ Price.GetHashCode();
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
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductAdded : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		private ProductAdded () {}
		public ProductAdded (ProductId productId, string name, decimal price)
		{
			Id = productId;
			Name = name;
			Price = price;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ProductAdded;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name) && Price.Equals(target.Price);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode() ^ Price.GetHashCode();
		}
		public static bool operator ==(ProductAdded a, ProductAdded b)
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
		public static bool operator !=(ProductAdded a, ProductAdded b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RemoveProduct : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		private RemoveProduct () {}
		public RemoveProduct (ProductId productId)
		{
			Id = productId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as RemoveProduct;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
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
		private ProductRemoved () {}
		public ProductRemoved (ProductId productId)
		{
			Id = productId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ProductRemoved;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
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
		public decimal NewPrice { get; set; }
		private ChangeProductPrice () {}
		public ChangeProductPrice (ProductId productId, decimal newPrice)
		{
			Id = productId;
			NewPrice = newPrice;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ChangeProductPrice;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && NewPrice.Equals(target.NewPrice);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ NewPrice.GetHashCode();
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
		public decimal NewPrice { get; set; }
		private ProductPriceChanged () {}
		public ProductPriceChanged (ProductId productId, decimal newPrice)
		{
			Id = productId;
			NewPrice = newPrice;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ProductPriceChanged;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && NewPrice.Equals(target.NewPrice);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ NewPrice.GetHashCode();
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
		public ProductId TargetProductId { get; set; }
		private AddRelatedProduct () {}
		public AddRelatedProduct (ProductId productId, ProductId targetProductId)
		{
			Id = productId;
			TargetProductId = targetProductId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as AddRelatedProduct;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && TargetProductId.Equals(target.TargetProductId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ TargetProductId.GetHashCode();
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
		public ProductId TargetProductId { get; set; }
		private RelatedProductAdded () {}
		public RelatedProductAdded (ProductId productId, ProductId targetProductId)
		{
			Id = productId;
			TargetProductId = targetProductId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as RelatedProductAdded;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && TargetProductId.Equals(target.TargetProductId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ TargetProductId.GetHashCode();
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
		public string Name { get; set; }
		private CreateCategory () {}
		public CreateCategory (string name)
		{
			Name = name;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as CreateCategory;
			if (target == null)
			{
				return false;
			}
			return Name.Equals(target.Name);
		}
		public override int GetHashCode()
		{
			return Name.GetHashCode();
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
		public string Name { get; set; }
		private CategoryCreated () {}
		public CategoryCreated (CatalogId catalogId, string name)
		{
			Id = catalogId;
			Name = name;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as CategoryCreated;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode();
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
		public string Name { get; set; }
		public string TargetCategory { get; set; }
		private MoveCategory () {}
		public MoveCategory (string name, string targetCategory)
		{
			Name = name;
			TargetCategory = targetCategory;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as MoveCategory;
			if (target == null)
			{
				return false;
			}
			return Name.Equals(target.Name) && TargetCategory.Equals(target.TargetCategory);
		}
		public override int GetHashCode()
		{
			return Name.GetHashCode() ^ TargetCategory.GetHashCode();
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
		public string Name { get; set; }
		public string TargetCategory { get; set; }
		private CategoryMoved () {}
		public CategoryMoved (CatalogId catalogId, string name, string targetCategory)
		{
			Id = catalogId;
			Name = name;
			TargetCategory = targetCategory;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as CategoryMoved;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name) && TargetCategory.Equals(target.TargetCategory);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode() ^ TargetCategory.GetHashCode();
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
		public string Name { get; set; }
		private RemoveCategory () {}
		public RemoveCategory (string name)
		{
			Name = name;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as RemoveCategory;
			if (target == null)
			{
				return false;
			}
			return Name.Equals(target.Name);
		}
		public override int GetHashCode()
		{
			return Name.GetHashCode();
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
		public string Name { get; set; }
		private CategoryRemoved () {}
		public CategoryRemoved (CatalogId catalogId, string name)
		{
			Id = catalogId;
			Name = name;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as CategoryRemoved;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode();
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
		public string CategoryName { get; set; }
		private AddProductToCategory () {}
		public AddProductToCategory (ProductId productId, string categoryName)
		{
			Id = productId;
			CategoryName = categoryName;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as AddProductToCategory;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && CategoryName.Equals(target.CategoryName);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ CategoryName.GetHashCode();
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
		public string CategoryName { get; set; }
		private ProductAddedToCategory () {}
		public ProductAddedToCategory (ProductId productId, string categoryName)
		{
			Id = productId;
			CategoryName = categoryName;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ProductAddedToCategory;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && CategoryName.Equals(target.CategoryName);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ CategoryName.GetHashCode();
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