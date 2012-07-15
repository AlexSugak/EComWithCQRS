using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;

namespace ECom.ReadModel
{
	public interface IDtoManager
	{
		void Add<T>(T dto) where T : Dto, new();
		void Delete<T>(IIdentity id) where T : Dto, new();
		void Delete<T>(string id) where T : Dto, new();
		void Update<T>(IIdentity id, Action<T> updateAction) where T : Dto, new();
		void Update<T>(string id, Action<T> updateAction) where T : Dto, new();
	}
}
