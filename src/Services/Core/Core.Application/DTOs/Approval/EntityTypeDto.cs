
namespace Core.Application.DTOs.Approval
{
    public class EntityTypeDto
    {
        public int EntityTypeID { get; set; }
        public string? EntityCode { get; set; }
        public string? EntityName { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
    }
}
