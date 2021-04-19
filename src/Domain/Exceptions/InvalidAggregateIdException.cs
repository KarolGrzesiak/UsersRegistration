using System;

namespace Domain.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate id: {id}")
        {
        }
    }
}