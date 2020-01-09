using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using ml_stats_core.Models;

namespace ml_stats_core.Interfaces
{
	public interface IRepository<T> where T : Entity
	{
		Task<T> GetByIdAsync(string id);
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(T entity);
		Task<IEnumerable<T>> GetByExpressionAsync(string sql, SqlParameterCollection parameters);
		Task<T> GetOneByExpressionAsync(string sql, SqlParameterCollection parameters);
	}
}
