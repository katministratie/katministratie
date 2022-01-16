using System;

namespace Superkatten.Katministratie.Infrastructure.Exceptions
{
    internal class DatabaseException : Exception
    {
        public DatabaseException(string? message) : base(message)
        {

        }
    }
}
