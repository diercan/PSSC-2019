using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents;
using ml_stats_core.Models;

namespace ml_stats_infrastructure.Data
{
	public interface IDocumentCollectionContext<in T> where T : Entity
	{
		string CollectionName { get; }
		string GenerateId(T entity);
		PartitionKey ResolvePartitionKey(string entityId);
	}
}
