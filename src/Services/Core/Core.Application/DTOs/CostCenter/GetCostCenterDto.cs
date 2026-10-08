using System.ComponentModel.DataAnnotations;
using Core.Application.Common.Mappings;

namespace Core.Application.DTOS.CostCenter
{
    public class GetCostCenterDto : IMapFrom<Domain.Entities.CostCenter>
    {
        [Required]
        public byte CostCenterSerialID { get; set; }

        [Required]
        public string CostCenterName { get; set; }= string.Empty;
    }
}
