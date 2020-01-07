﻿using System;
 using Microsoft.Azure.Documents;
 using ml_stats_core.Interfaces;
 using User = ml_stats_core.Models.User;

 namespace ml_stats_infrastructure.Data
{
  public class UserItemRepository : CosmosDbRepository<User>, IUserItemRepository
  {
    public UserItemRepository(ICosmosDbClientFactory cosmosDbClientFactory) : base(cosmosDbClientFactory)
    {
    }
    public override string CollectionName { get; } = "users";
    public override string GenerateId(User entity) => $"{entity.Id}:{Guid.NewGuid()}";
    public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);
  }
}