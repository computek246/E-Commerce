using System;

namespace eCommerce.Common.Models.Interfaces
{
    public interface IAuditable<TForeignKey> : IActiveable, ISoftDeletable
    {
        TForeignKey CreatorId { get; set; } // CreatorId
        DateTime CreationDate { get; set; } // CreationDate
        TForeignKey ModifierId { get; set; } // ModifierId
        DateTime LastModifiedDate { get; set; } // LastModifiedDate
    }
}