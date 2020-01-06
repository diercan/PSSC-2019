using System;
using System.Collections.Generic;
using System.Text;

namespace ml_stats_core.Exceptions
{
	public class EntityAlreadyExistsException : Exception
	{
		public EntityAlreadyExistsException()
		{

		}
		public EntityAlreadyExistsException(string message) : base(message) { }
	}
}
