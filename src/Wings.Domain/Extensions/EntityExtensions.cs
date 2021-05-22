using System;

namespace Wings.Domain.Entities
{
    public static class EntityExtensions
    {
        public static bool IsEntity(this Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }
    }
}
