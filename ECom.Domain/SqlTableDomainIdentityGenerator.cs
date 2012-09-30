using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using ECom.Utility;

namespace ECom.Domain
{
	/// <summary>
	/// Uses SQL table with single identity column to generate sequential domain ids
	/// </summary>
	public class SqlTableDomainIdentityGenerator : IDomainIdentityGenerator
	{
		private readonly string _connectionString;

		public SqlTableDomainIdentityGenerator(string identitiesDbConnString)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => identitiesDbConnString);

			_connectionString = identitiesDbConnString;
		}

		public int GenerateNewId()
		{
			return SelectNewId();
		}

		private int SelectNewId()
		{
			var result = -1;
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				try
				{
					SqlCommand cmd = connection.CreateCommand();

					cmd.CommandText =
@"
BEGIN
	INSERT INTO [dbo].[Identities] DEFAULT VALUES
	SELECT @@IDENTITY
END";

					result = Convert.ToInt32((decimal)cmd.ExecuteScalar());
				}
				finally
				{
					connection.Close();
				}
			}

			return result;
		}
	}
}
