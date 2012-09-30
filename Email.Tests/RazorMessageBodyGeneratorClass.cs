using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Email.Tests
{
	[TestClass]
	public class RazorMessageBodyGeneratorClass
	{
		[TestClass]
		public class GenerateMethod
		{
			[TestMethod]
			public void must_generate_plain_text_body()
			{
				var data = new SimpleEmaiDataModel() { Name = "Alex" };

				var generator = new RazorMessageBodyGenerator();
				var template = "Hello @Model.Name!";

				var result = generator.Generate(template, data);

				Assert.AreEqual("Hello Alex!", result);
			}

			[TestMethod]
			public void must_generate_html_body()
			{
				var data = new SimpleEmaiDataModel() { Name = "Alex" };

				var generator = new RazorMessageBodyGenerator();
				var template = "<html><head><title>Hello @Model.Name</title></head><body>Sample body</body></html>";

				var result = generator.Generate(template, data);

				Assert.AreEqual("<html><head><title>Hello Alex</title></head><body>Sample body</body></html>", result);
			}

			[TestMethod]
			public void must_generate_html_body_with_links()
			{
				var data = new SimpleEmaiDataModel() { Name = "Alex", Param = "123" };

				var generator = new RazorMessageBodyGenerator();
				var template = "<html><head><title>Hello @Model.Name</title></head><body>Click <a href='http://domain.com/@Model.Param/234'>here</a></body></html>";

				var result = generator.Generate(template, data);

				Assert.AreEqual("<html><head><title>Hello Alex</title></head><body>Click <a href='http://domain.com/123/234'>here</a></body></html>", result);
			}
		}
	}

	public class SimpleEmaiDataModel
	{
		public string Name;
		public string Param;
	}
}
