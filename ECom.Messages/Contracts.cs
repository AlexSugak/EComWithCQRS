


using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECom.Messages 
{
	
	
	[Serializable]
	public sealed class AddProduct : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public AddProduct () {}
		public AddProduct (ProductId productId, string name, decimal price)
		{
			Id = productId;
			Name = name;
			Price = price;
		}
	}
	
	[Serializable]
	public sealed class ProductAdded : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public ProductAdded () {}
		public ProductAdded (ProductId productId, string name, decimal price)
		{
			Id = productId;
			Name = name;
			Price = price;
		}
	}
	
	[Serializable]
	public sealed class RemoveProduct : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public RemoveProduct () {}
		public RemoveProduct (ProductId productId)
		{
			Id = productId;
		}
	}
	
	[Serializable]
	public sealed class ProductRemoved : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public ProductRemoved () {}
		public ProductRemoved (ProductId productId)
		{
			Id = productId;
		}
	}
	
	[Serializable]
	public sealed class ChangeProductPrice : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public decimal NewPrice { get; set; }
		public ChangeProductPrice () {}
		public ChangeProductPrice (ProductId productId, decimal newPrice)
		{
			Id = productId;
			NewPrice = newPrice;
		}
	}
	
	[Serializable]
	public sealed class ProductPriceChanged : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public decimal NewPrice { get; set; }
		public ProductPriceChanged () {}
		public ProductPriceChanged (ProductId productId, decimal newPrice)
		{
			Id = productId;
			NewPrice = newPrice;
		}
	}
	
	[Serializable]
	public sealed class CreateCategory : IFunctionalCommand
	{
		public int Version { get; set; }
		public string Name { get; set; }
		public CreateCategory () {}
		public CreateCategory (string name)
		{
			Name = name;
		}
	}
	
	[Serializable]
	public sealed class CategoryCreated : IEvent<CatalogId>
	{
		public CatalogId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public CategoryCreated () {}
		public CategoryCreated (CatalogId catalogId, string name)
		{
			Id = catalogId;
			Name = name;
		}
	}
	
	[Serializable]
	public sealed class MoveCategory : IFunctionalCommand
	{
		public int Version { get; set; }
		public string Name { get; set; }
		public string TargetCategory { get; set; }
		public MoveCategory () {}
		public MoveCategory (string name, string targetCategory)
		{
			Name = name;
			TargetCategory = targetCategory;
		}
	}
	
	[Serializable]
	public sealed class CategoryMoved : IEvent<CatalogId>
	{
		public CatalogId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public string TargetCategory { get; set; }
		public CategoryMoved () {}
		public CategoryMoved (CatalogId catalogId, string name, string targetCategory)
		{
			Id = catalogId;
			Name = name;
			TargetCategory = targetCategory;
		}
	}
	
	[Serializable]
	public sealed class AddProductToCategory : ICommand<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public string CategoryName { get; set; }
		public AddProductToCategory () {}
		public AddProductToCategory (ProductId productId, string categoryName)
		{
			Id = productId;
			CategoryName = categoryName;
		}
	}
	
	[Serializable]
	public sealed class ProductAddedToCategory : IEvent<ProductId>
	{
		public ProductId Id { get; set; }
		public int Version { get; set; }
		public string CategoryName { get; set; }
		public ProductAddedToCategory () {}
		public ProductAddedToCategory (ProductId productId, string categoryName)
		{
			Id = productId;
			CategoryName = categoryName;
		}
	}
	
	[Serializable]
	public sealed class CreateNewOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public CreateNewOrder () {}
		public CreateNewOrder (OrderId orderId)
		{
			Id = orderId;
		}
	}
	
	[Serializable]
	public sealed class NewOrderCreated : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public NewOrderCreated () {}
		public NewOrderCreated (OrderId orderId)
		{
			Id = orderId;
		}
	}
	
	[Serializable]
	public sealed class AddProductToOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public ProductId ProductId { get; set; }
		public int Quantity { get; set; }
		public AddProductToOrder () {}
		public AddProductToOrder (OrderId orderId, ProductId productId, int quantity)
		{
			Id = orderId;
			ProductId = productId;
			Quantity = quantity;
		}
	}
	
	[Serializable]
	public sealed class ProductAddedToOrder : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public ProductId ProductId { get; set; }
		public int Quantity { get; set; }
		public ProductAddedToOrder () {}
		public ProductAddedToOrder (OrderId orderId, ProductId productId, int quantity)
		{
			Id = orderId;
			ProductId = productId;
			Quantity = quantity;
		}
	}
	
	[Serializable]
	public sealed class RemoveProductFromOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public ProductId ProductId { get; set; }
		public RemoveProductFromOrder () {}
		public RemoveProductFromOrder (OrderId orderId, ProductId productId)
		{
			Id = orderId;
			ProductId = productId;
		}
	}
	
	[Serializable]
	public sealed class ProductRemovedFromOrder : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public ProductId ProductId { get; set; }
		public ProductRemovedFromOrder () {}
		public ProductRemovedFromOrder (OrderId orderId, ProductId productId)
		{
			Id = orderId;
			ProductId = productId;
		}
	}
	
	[Serializable]
	public sealed class SubmitOrder : ICommand<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public SubmitOrder () {}
		public SubmitOrder (OrderId orderId)
		{
			Id = orderId;
		}
	}
	
	[Serializable]
	public sealed class OrderSubmited : IEvent<OrderId>
	{
		public OrderId Id { get; set; }
		public int Version { get; set; }
		public OrderSubmited () {}
		public OrderSubmited (OrderId orderId)
		{
			Id = orderId;
		}
	}
	
	[Serializable]
	public sealed class CreateDiscount : ICommand<DiscountId>
	{
		public DiscountId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public decimal Value { get; set; }
		public CreateDiscount () {}
		public CreateDiscount (DiscountId discountId, string name, decimal value)
		{
			Id = discountId;
			Name = name;
			Value = value;
		}
	}
	
	[Serializable]
	public sealed class DiscountCreated : IEvent<DiscountId>
	{
		public DiscountId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public decimal Value { get; set; }
		public DiscountCreated () {}
		public DiscountCreated (DiscountId discountId, string name, decimal value)
		{
			Id = discountId;
			Name = name;
			Value = value;
		}
	}
	
	[Serializable]
	public sealed class AssignDiscountToProduct : ICommand<DiscountId>
	{
		public DiscountId Id { get; set; }
		public int Version { get; set; }
		public ProductId ProductId { get; set; }
		public AssignDiscountToProduct () {}
		public AssignDiscountToProduct (DiscountId discountId, ProductId productId)
		{
			Id = discountId;
			ProductId = productId;
		}
	}
	
	[Serializable]
	public sealed class DiscountAassignedToProduct : IEvent<DiscountId>
	{
		public DiscountId Id { get; set; }
		public int Version { get; set; }
		public ProductId ProductId { get; set; }
		public DiscountAassignedToProduct () {}
		public DiscountAassignedToProduct (DiscountId discountId, ProductId productId)
		{
			Id = discountId;
			ProductId = productId;
		}
	}

}