using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DocType
{
    public class GetDocTypePaginationDto
    {
        public int Id { get; set; } // ROW_NUMBER()

        public int ModuleSerialID { get; set; }
        public string? ModName { get; set; }

        public int DocTypeSerialID { get; set; }
        public string? DocumentType { get; set; }

        public int? FilePathSerialID { get; set; }
        public string? FilePath { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
    }
}
