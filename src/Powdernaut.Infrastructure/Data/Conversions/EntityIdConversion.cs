using Powdernaut.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Powdernaut.Infrastructure.Data.Conversions;

public class EntityIdConversion : ValueConverter<EntityId, Guid>
{
    public EntityIdConversion() : base(c => c.Value, c => EntityId.FromGuid(c))
    {

    }
}