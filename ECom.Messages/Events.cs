//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the "EventsGenerator.fsx" script.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace ECom.Messages
{

[GeneratedCodeAttribute("EventsGenerator.fsx", "1.0.0.0")]
[DataContract(Namespace = "http://crowdshop.com/contracts/events/")]
public sealed class UserCreated: IEvent<UserId>, IEquatable<UserCreated>
{
    public UserId Id
    {
        get { return this.userId; }
    }

    [DataMember(Name = "Date")]
    private DateTime date;

    public DateTime Date
    {
        get { return this.date; }
    }

    [DataMember(Name = "Version")]
    private Int32 version;

    public Int32 Version
    {
        get { return this.version; }
    }

    [DataMember(Name = "UserId")]
    private UserId userId;

    public UserId UserId
    {
        get { return this.userId; }
    }

    public UserCreated(DateTime date, Int32 version, UserId userId)
    {
        this.date = date;
        this.version = version;
        this.userId = userId;
    }
        
    public bool Equals(UserCreated other)
    {
        if (this != null)
		{
			return other != null && DateTime.Equals(this.Date, other.Date) && Int32.Equals(this.Version, other.Version) && UserId.Equals(this.UserId, other.UserId);
		}
		return other == null;
    }

    public override bool Equals(object obj)
    {
        var other = obj as UserCreated;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        
        hash = hash * 29 + this.Date.GetHashCode();
        hash = hash * 29 + this.Version.GetHashCode();
        if (this.UserId != null)
            hash = hash * 29 + this.UserId.GetHashCode();
        
        return hash;
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

[GeneratedCodeAttribute("EventsGenerator.fsx", "1.0.0.0")]
[DataContract(Namespace = "http://crowdshop.com/contracts/events/")]
public sealed class UserLoggedInReported: IEvent<UserId>, IEquatable<UserLoggedInReported>
{
    public UserId Id
    {
        get { return this.userId; }
    }

    [DataMember(Name = "Date")]
    private DateTime date;

    public DateTime Date
    {
        get { return this.date; }
    }

    [DataMember(Name = "Version")]
    private Int32 version;

    public Int32 Version
    {
        get { return this.version; }
    }

    [DataMember(Name = "UserId")]
    private UserId userId;

    public UserId UserId
    {
        get { return this.userId; }
    }

    [DataMember(Name = "UserName")]
    private String userName;

    public String UserName
    {
        get { return this.userName; }
    }

    [DataMember(Name = "PhotoUrl")]
    private String photoUrl;

    public String PhotoUrl
    {
        get { return this.photoUrl; }
    }

    public UserLoggedInReported(DateTime date, Int32 version, UserId userId, String userName, String photoUrl)
    {
        this.date = date;
        this.version = version;
        this.userId = userId;
        this.userName = userName;
        this.photoUrl = photoUrl;
    }
        
    public bool Equals(UserLoggedInReported other)
    {
        if (this != null)
		{
			return other != null && DateTime.Equals(this.Date, other.Date) && Int32.Equals(this.Version, other.Version) && UserId.Equals(this.UserId, other.UserId) && String.Equals(this.UserName, other.UserName) && String.Equals(this.PhotoUrl, other.PhotoUrl);
		}
		return other == null;
    }

    public override bool Equals(object obj)
    {
        var other = obj as UserLoggedInReported;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        
        hash = hash * 29 + this.Date.GetHashCode();
        hash = hash * 29 + this.Version.GetHashCode();
        if (this.UserId != null)
            hash = hash * 29 + this.UserId.GetHashCode();
        if (this.UserName != null)
            hash = hash * 29 + this.UserName.GetHashCode();
        if (this.PhotoUrl != null)
            hash = hash * 29 + this.PhotoUrl.GetHashCode();
        
        return hash;
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

[GeneratedCodeAttribute("EventsGenerator.fsx", "1.0.0.0")]
[DataContract(Namespace = "http://crowdshop.com/contracts/events/")]
public sealed class UserEmailSet: IEvent<UserId>, IEquatable<UserEmailSet>
{
    public UserId Id
    {
        get { return this.userId; }
    }

    [DataMember(Name = "Date")]
    private DateTime date;

    public DateTime Date
    {
        get { return this.date; }
    }

    [DataMember(Name = "Version")]
    private Int32 version;

    public Int32 Version
    {
        get { return this.version; }
    }

    [DataMember(Name = "UserId")]
    private UserId userId;

    public UserId UserId
    {
        get { return this.userId; }
    }

    [DataMember(Name = "Email")]
    private EmailAddress email;

    public EmailAddress Email
    {
        get { return this.email; }
    }

    public UserEmailSet(DateTime date, Int32 version, UserId userId, EmailAddress email)
    {
        this.date = date;
        this.version = version;
        this.userId = userId;
        this.email = email;
    }
        
    public bool Equals(UserEmailSet other)
    {
        if (this != null)
		{
			return other != null && DateTime.Equals(this.Date, other.Date) && Int32.Equals(this.Version, other.Version) && UserId.Equals(this.UserId, other.UserId) && EmailAddress.Equals(this.Email, other.Email);
		}
		return other == null;
    }

    public override bool Equals(object obj)
    {
        var other = obj as UserEmailSet;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        
        hash = hash * 29 + this.Date.GetHashCode();
        hash = hash * 29 + this.Version.GetHashCode();
        if (this.UserId != null)
            hash = hash * 29 + this.UserId.GetHashCode();
        if (this.Email != null)
            hash = hash * 29 + this.Email.GetHashCode();
        
        return hash;
    }

    public static bool operator ==(UserEmailSet a, UserEmailSet b)
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

	public static bool operator !=(UserEmailSet a, UserEmailSet b)
	{
		return !(a == b);
	}
}

[GeneratedCodeAttribute("EventsGenerator.fsx", "1.0.0.0")]
[DataContract(Namespace = "http://crowdshop.com/contracts/events/")]
public sealed class NewOrderCreated: IEvent<OrderId>, IEquatable<NewOrderCreated>
{
    public OrderId Id
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "Date")]
    private DateTime date;

    public DateTime Date
    {
        get { return this.date; }
    }

    [DataMember(Name = "Version")]
    private Int32 version;

    public Int32 Version
    {
        get { return this.version; }
    }

    [DataMember(Name = "OrderId")]
    private OrderId orderId;

    public OrderId OrderId
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "UserId")]
    private UserId userId;

    public UserId UserId
    {
        get { return this.userId; }
    }

    public NewOrderCreated(DateTime date, Int32 version, OrderId orderId, UserId userId)
    {
        this.date = date;
        this.version = version;
        this.orderId = orderId;
        this.userId = userId;
    }
        
    public bool Equals(NewOrderCreated other)
    {
        if (this != null)
		{
			return other != null && DateTime.Equals(this.Date, other.Date) && Int32.Equals(this.Version, other.Version) && OrderId.Equals(this.OrderId, other.OrderId) && UserId.Equals(this.UserId, other.UserId);
		}
		return other == null;
    }

    public override bool Equals(object obj)
    {
        var other = obj as NewOrderCreated;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        
        hash = hash * 29 + this.Date.GetHashCode();
        hash = hash * 29 + this.Version.GetHashCode();
        if (this.OrderId != null)
            hash = hash * 29 + this.OrderId.GetHashCode();
        if (this.UserId != null)
            hash = hash * 29 + this.UserId.GetHashCode();
        
        return hash;
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

[GeneratedCodeAttribute("EventsGenerator.fsx", "1.0.0.0")]
[DataContract(Namespace = "http://crowdshop.com/contracts/events/")]
public sealed class ProductAddedToOrder: IEvent<OrderId>, IEquatable<ProductAddedToOrder>
{
    public OrderId Id
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "Date")]
    private DateTime date;

    public DateTime Date
    {
        get { return this.date; }
    }

    [DataMember(Name = "Version")]
    private Int32 version;

    public Int32 Version
    {
        get { return this.version; }
    }

    [DataMember(Name = "OrderId")]
    private OrderId orderId;

    public OrderId OrderId
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "OrderItemId")]
    private OrderItemId orderItemId;

    public OrderItemId OrderItemId
    {
        get { return this.orderItemId; }
    }

    [DataMember(Name = "ProductUri")]
    private Uri productUri;

    public Uri ProductUri
    {
        get { return this.productUri; }
    }

    [DataMember(Name = "Name")]
    private String name;

    public String Name
    {
        get { return this.name; }
    }

    [DataMember(Name = "Description")]
    private String description;

    public String Description
    {
        get { return this.description; }
    }

    [DataMember(Name = "Price")]
    private Decimal price;

    public Decimal Price
    {
        get { return this.price; }
    }

    [DataMember(Name = "Quantity")]
    private Int32 quantity;

    public Int32 Quantity
    {
        get { return this.quantity; }
    }

    [DataMember(Name = "Size")]
    private String size;

    public String Size
    {
        get { return this.size; }
    }

    [DataMember(Name = "Color")]
    private String color;

    public String Color
    {
        get { return this.color; }
    }

    [DataMember(Name = "ImageUri")]
    private Uri imageUri;

    public Uri ImageUri
    {
        get { return this.imageUri; }
    }

    public ProductAddedToOrder(DateTime date, Int32 version, OrderId orderId, OrderItemId orderItemId, Uri productUri, String name, String description, Decimal price, Int32 quantity, String size, String color, Uri imageUri)
    {
        this.date = date;
        this.version = version;
        this.orderId = orderId;
        this.orderItemId = orderItemId;
        this.productUri = productUri;
        this.name = name;
        this.description = description;
        this.price = price;
        this.quantity = quantity;
        this.size = size;
        this.color = color;
        this.imageUri = imageUri;
    }
        
    public bool Equals(ProductAddedToOrder other)
    {
        if (this != null)
		{
			return other != null && DateTime.Equals(this.Date, other.Date) && Int32.Equals(this.Version, other.Version) && OrderId.Equals(this.OrderId, other.OrderId) && OrderItemId.Equals(this.OrderItemId, other.OrderItemId) && Uri.Equals(this.ProductUri, other.ProductUri) && String.Equals(this.Name, other.Name) && String.Equals(this.Description, other.Description) && Decimal.Equals(this.Price, other.Price) && Int32.Equals(this.Quantity, other.Quantity) && String.Equals(this.Size, other.Size) && String.Equals(this.Color, other.Color) && Uri.Equals(this.ImageUri, other.ImageUri);
		}
		return other == null;
    }

    public override bool Equals(object obj)
    {
        var other = obj as ProductAddedToOrder;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        
        hash = hash * 29 + this.Date.GetHashCode();
        hash = hash * 29 + this.Version.GetHashCode();
        if (this.OrderId != null)
            hash = hash * 29 + this.OrderId.GetHashCode();
        if (this.OrderItemId != null)
            hash = hash * 29 + this.OrderItemId.GetHashCode();
        if (this.ProductUri != null)
            hash = hash * 29 + this.ProductUri.GetHashCode();
        if (this.Name != null)
            hash = hash * 29 + this.Name.GetHashCode();
        if (this.Description != null)
            hash = hash * 29 + this.Description.GetHashCode();
        hash = hash * 29 + this.Price.GetHashCode();
        hash = hash * 29 + this.Quantity.GetHashCode();
        if (this.Size != null)
            hash = hash * 29 + this.Size.GetHashCode();
        if (this.Color != null)
            hash = hash * 29 + this.Color.GetHashCode();
        if (this.ImageUri != null)
            hash = hash * 29 + this.ImageUri.GetHashCode();
        
        return hash;
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

[GeneratedCodeAttribute("EventsGenerator.fsx", "1.0.0.0")]
[DataContract(Namespace = "http://crowdshop.com/contracts/events/")]
public sealed class ItemRemovedFromOrder: IEvent<OrderId>, IEquatable<ItemRemovedFromOrder>
{
    public OrderId Id
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "Date")]
    private DateTime date;

    public DateTime Date
    {
        get { return this.date; }
    }

    [DataMember(Name = "Version")]
    private Int32 version;

    public Int32 Version
    {
        get { return this.version; }
    }

    [DataMember(Name = "OrderId")]
    private OrderId orderId;

    public OrderId OrderId
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "OrderItemId")]
    private OrderItemId orderItemId;

    public OrderItemId OrderItemId
    {
        get { return this.orderItemId; }
    }

    public ItemRemovedFromOrder(DateTime date, Int32 version, OrderId orderId, OrderItemId orderItemId)
    {
        this.date = date;
        this.version = version;
        this.orderId = orderId;
        this.orderItemId = orderItemId;
    }
        
    public bool Equals(ItemRemovedFromOrder other)
    {
        if (this != null)
		{
			return other != null && DateTime.Equals(this.Date, other.Date) && Int32.Equals(this.Version, other.Version) && OrderId.Equals(this.OrderId, other.OrderId) && OrderItemId.Equals(this.OrderItemId, other.OrderItemId);
		}
		return other == null;
    }

    public override bool Equals(object obj)
    {
        var other = obj as ItemRemovedFromOrder;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        
        hash = hash * 29 + this.Date.GetHashCode();
        hash = hash * 29 + this.Version.GetHashCode();
        if (this.OrderId != null)
            hash = hash * 29 + this.OrderId.GetHashCode();
        if (this.OrderItemId != null)
            hash = hash * 29 + this.OrderItemId.GetHashCode();
        
        return hash;
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

[GeneratedCodeAttribute("EventsGenerator.fsx", "1.0.0.0")]
[DataContract(Namespace = "http://crowdshop.com/contracts/events/")]
public sealed class OrderSubmited: IEvent<OrderId>, IEquatable<OrderSubmited>
{
    public OrderId Id
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "Date")]
    private DateTime date;

    public DateTime Date
    {
        get { return this.date; }
    }

    [DataMember(Name = "Version")]
    private Int32 version;

    public Int32 Version
    {
        get { return this.version; }
    }

    [DataMember(Name = "OrderId")]
    private OrderId orderId;

    public OrderId OrderId
    {
        get { return this.orderId; }
    }

    [DataMember(Name = "UserId")]
    private UserId userId;

    public UserId UserId
    {
        get { return this.userId; }
    }

    [DataMember(Name = "ItemsCount")]
    private Int32 itemsCount;

    public Int32 ItemsCount
    {
        get { return this.itemsCount; }
    }

    [DataMember(Name = "Total")]
    private Decimal total;

    public Decimal Total
    {
        get { return this.total; }
    }

    public OrderSubmited(DateTime date, Int32 version, OrderId orderId, UserId userId, Int32 itemsCount, Decimal total)
    {
        this.date = date;
        this.version = version;
        this.orderId = orderId;
        this.userId = userId;
        this.itemsCount = itemsCount;
        this.total = total;
    }
        
    public bool Equals(OrderSubmited other)
    {
        if (this != null)
		{
			return other != null && DateTime.Equals(this.Date, other.Date) && Int32.Equals(this.Version, other.Version) && OrderId.Equals(this.OrderId, other.OrderId) && UserId.Equals(this.UserId, other.UserId) && Int32.Equals(this.ItemsCount, other.ItemsCount) && Decimal.Equals(this.Total, other.Total);
		}
		return other == null;
    }

    public override bool Equals(object obj)
    {
        var other = obj as OrderSubmited;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        
        hash = hash * 29 + this.Date.GetHashCode();
        hash = hash * 29 + this.Version.GetHashCode();
        if (this.OrderId != null)
            hash = hash * 29 + this.OrderId.GetHashCode();
        if (this.UserId != null)
            hash = hash * 29 + this.UserId.GetHashCode();
        hash = hash * 29 + this.ItemsCount.GetHashCode();
        hash = hash * 29 + this.Total.GetHashCode();
        
        return hash;
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

}