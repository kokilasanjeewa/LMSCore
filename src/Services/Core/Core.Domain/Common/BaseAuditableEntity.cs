using Core.Domain.Common.interfaces;

namespace Core.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }

        // will update later
        //public int? DeletedBy { get; set; }
        //public DateTime? DeletedDate { get; set; }
    }
}
