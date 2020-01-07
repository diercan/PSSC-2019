using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ml_stats_core.Models;

namespace ml_stats_core.Interfaces
{
	public interface IRepository<T> where T : Entity
	{
		Task<T> GetByIdAsync(string id);
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(T entity);
	}
}
