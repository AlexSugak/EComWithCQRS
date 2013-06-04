using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Infrastructure
{
	/// <summary>
	/// Used to generate new domain identities
	/// </summary>
	public interface IDomainIdentityGenerator
	{
		long GenerateNewId();
	}
}
