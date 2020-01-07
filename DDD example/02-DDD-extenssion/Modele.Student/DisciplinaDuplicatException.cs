using System;
using System.Runtime.Serialization;

namespace Modele.Student
{
    [Serializable]
    internal class DisciplinaDuplicatException : Exception
    {
        public DisciplinaDuplicatException()
        {
        }

        public DisciplinaDuplicatException(string message) : base(message)
        {
        }

        public DisciplinaDuplicatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DisciplinaDuplicatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}