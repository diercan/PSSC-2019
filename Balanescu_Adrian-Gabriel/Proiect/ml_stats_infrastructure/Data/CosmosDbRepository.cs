using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using ml_stats_core.Exceptions;
using ml_stats_core.Interfaces;
using ml_stats_core.Models;

namespace ml_stats_infrastructure.Data
{
	public abstract class CosmosDbRepository<T> : IRepository<T>, IDocumentCollectionContext<T> where T : Entity
	{
		private readonly ICosmosDbClientFactory _cosmosDbClientFactory;

		public CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
		{
			_cosmosDbClientFactory = cosmosDbClientFactory;
		}

		public async Task<T> GetByIdAsync(string id)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await cosmosDbClient.ReadDocumentAsync(id, new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id)
				});

				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		public async Task<T> AddAsync(T entity)
		{
			try
			{
				entity.Id = GenerateId(entity);
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await cosmosDbClient.CreateDocumentAsync(entity);
				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.Conflict)
				{
					throw new EntityAlreadyExistsException();
				}

				throw;
			}
		}

		public async Task<T> DeleteAsync(T entity)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await cosmosDbClient.DeleteDocumentAsync(entity.Id, new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(entity.Id)
				});
				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		public async Task<T> UpdateAsync(T entity)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				var document  = await cosmosDbClient.ReplaceDocumentAsync(entity.Id, entity);
				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		public abstract string CollectionName { get; }
		public virtual string GenerateId(T entity) => Guid.NewGuid().ToString();
		public virtual PartitionKey ResolvePartitionKey(string entityId) => null;
	}
}
