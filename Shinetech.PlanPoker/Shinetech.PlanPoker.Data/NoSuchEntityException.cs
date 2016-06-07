using System;

namespace Shinetech.PlanPoker.Data
{
    public class NoSuchEntityException : Exception
    {
        public NoSuchEntityException(object id, Type type) : base($"Could not find an entity with id: {id} and type: {type.FullName}") { }
    }
}