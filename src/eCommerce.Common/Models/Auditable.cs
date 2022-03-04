using System;
using eCommerce.Common.Models.Interfaces;

namespace eCommerce.Common.Models
{
    public class Auditable : Auditable<int>
    {
    }


    public class Auditable<TForeignKey>
        : BaseEntity, IAuditable<TForeignKey>
    {
        public bool IsActive { get; set; } // IsActive
        public TForeignKey CreatorId { get; set; } // CreatorId
        public DateTime CreationDate { get; set; } // CreationDate
        public TForeignKey ModifierId { get; set; } // ModifierId
        public DateTime LastModifiedDate { get; set; } // LastModifiedDate
        public bool IsDeleted { get; set; } // IsDeleted
    }
}