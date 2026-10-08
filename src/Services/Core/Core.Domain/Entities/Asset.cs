using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities
{
    public class Asset: BaseAuditableEntity
    {
        [Key]
        public long AssetId { get; set; }
        [NotMapped]
        public long Id { get; set; }

        public string? Barcode { get; set; }

        public long? AssetMcatId { get; set; }

        public long AssetscatId { get; set; }

        public long? AssetMtypeId { get; set; }

        public long? AssetstypeId { get; set; }

        public string? AssetName { get; set; }

        public long? BrandId { get; set; }

        public long? ModelId { get; set; }

        public string? ItemCode { get; set; }

        public string? SerialNoOther { get; set; }

        public DateTime? PhotoUploadedDateTime { get; set; }

        public long? OwnershipTypeId { get; set; }

        public long? CompId { get; set; }

        public string? SupId { get; set; }

        public DateTime? DateActivated { get; set; }

        public string? Remark { get; set; }

        public string? PurchaseInfo { get; set; }

        public long LocationId { get; set; }

        public long OpConditionId { get; set; }

        public string? OpConditionByStaffId { get; set; }

        public DateTime? OpConditionDateTime { get; set; }

        public long Assetstatus { get; set; }

        public string? AssetstatusByStaffId { get; set; }

        public DateTime? AssetstatusDateTime { get; set; }

        public long AssetAvailableStatus { get; set; }

        public string? AssetAvailableByStaffId { get; set; }

        public DateTime? AssetAvailableDateTime { get; set; }

        public long AppStatus { get; set; }

        public string? AppByStaffId { get; set; }

        public DateTime? AppDateTime { get; set; }

        public int DispatchedStatus { get; set; }

        public DateTime? DateDispatched { get; set; }

        public string? DispatchedBy { get; set; }

        public DateTime? ReturnedDateTime { get; set; }

        public string? ReturnedBy { get; set; }

        public long? Aodhid { get; set; }

        public long? Aoddid { get; set; }

        public long RecStatus { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string? CreatedBy { get; set; }

        public string? CreatedMachine { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public string? ModifiedBy { get; set; }

        public string? ModifiedMachine { get; set; }

        public string? DevRemark { get; set; }

    }
}
