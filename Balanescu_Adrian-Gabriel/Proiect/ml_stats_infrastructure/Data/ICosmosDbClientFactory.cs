using System;
using System.Collections.Generic;
using System.Text;

namespace ml_stats_infrastructure.Data
{
	public interface ICosmosDbClientFactory
	{
		ICosmosDbClient GetClient(string collectionName);
	}
}
