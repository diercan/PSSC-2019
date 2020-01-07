using System;
using System.Collections.Generic;
using System.Text;

namespace ml_stats_core.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException()
		{

		}

		public EntityNotFoundException(string message) : base(message)
		{

		}
	}
}
