﻿using System;
 using System.Threading.Tasks;
 using Microsoft.Azure.Documents;
 using ml_stats_core.Interfaces;
 using ml_stats_core.Models;
 using User = ml_stats_core.Models.User;

 namespace ml_stats_infrastructure.Data
{
  public class PlotItemRepository : CosmosDbRepository<Plot>, IPlotItemRepository
  {
    public PlotItemRepository(ICosmosDbClientFactory cosmosDbClientFactory) : base(cosmosDbClientFactory)
    {
    }
    public override string CollectionName { get; } = "plots";
    public override string GenerateId(Plot entity) => $"{Guid.NewGuid()}";
    public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);
  }
}