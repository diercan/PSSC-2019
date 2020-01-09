﻿using System;
 using System.Threading.Tasks;
 using Microsoft.Azure.Documents;
 using ml_stats_core.Interfaces;
 using ml_stats_core.Models;

 namespace ml_stats_infrastructure.Data
{
  public class ExperimentItemRepository : CosmosDbRepository<Experiment>, IExperimentItemRepository
  {
    public ExperimentItemRepository(ICosmosDbClientFactory cosmosDbClientFactory) : base(cosmosDbClientFactory)
    {
    }
    public override string CollectionName { get; } = "experiments";
    public override string GenerateId(Experiment entity) => $"{Guid.NewGuid()}";
    public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);
  }
}