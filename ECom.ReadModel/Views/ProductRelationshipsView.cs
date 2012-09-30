using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	[Serializable]
	public class ProductRelationship : Dto
	{
		public string ID { get; set; }
		public string ParentProductId { get; set; }
		public string TargetProductId { get; set; }
		public string TargetProductName { get; set; }

		public ProductRelationship()
		{
		}

		public ProductRelationship(string parentProductId , string targetProductId, string targetProductName)
		{
			ID = Guid.NewGuid().ToString();
			ParentProductId = parentProductId;
			TargetProductId = targetProductId;
			TargetProductName = targetProductName;
		}
	}

	public class ProductRelationshipsView : ReadModelView,
			IHandle<RelatedProductAdded>
	{
		public ProductRelationshipsView(IDtoManager manager, IReadModelFacade readModel)
			: base(manager, readModel)
		{
		}

		public void Handle(RelatedProductAdded message)
		{
			var rargetProductId = message.TargetProductId.GetId();
			var targetProduct = _manager.Get<ProductDetails>(p => p.ID == rargetProductId);
			_manager.Add<ProductRelationship>(new ProductRelationship(message.Id.GetId(), rargetProductId, targetProduct.Name));
		}
	}
}
