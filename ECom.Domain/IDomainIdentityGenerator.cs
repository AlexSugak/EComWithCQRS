using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Domain
{
	/// <summary>
	/// Used to generate new domain identities
	/// </summary>
	public interface IDomainIdentityGenerator
	{
		int GenerateNewId();
	}
}
