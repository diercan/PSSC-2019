using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ml_stats_infrastructure.Data
{
	public interface ICosmosDbClient
	{
		Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null, 
			CancellationToken cancellationToken = default(CancellationToken));
		Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
			bool disableAutomaticIdGeneration = false,
			CancellationToken cancellationToken = default(CancellationToken));
		Task<Document> ReplaceDocumentAsync(string documentId, object document, RequestOptions options = null,
						CancellationToken cancellationToken = default(CancellationToken));
		Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
				CancellationToken cancellationToken = default(CancellationToken));
	}
}
