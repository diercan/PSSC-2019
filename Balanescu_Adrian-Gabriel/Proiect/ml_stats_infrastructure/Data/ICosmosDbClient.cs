using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;

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
		IDocumentQuery<T> ReadDocumentsByQuery<T>(string sql, SqlParameterCollection parameters, RequestOptions options = null, 
			CancellationToken cancellationToken = default(CancellationToken));
	}
}
