using System.ComponentModel.DataAnnotations;
using Core.Application.Common.Mappings;
using AutoMapper;

namespace Core.Application.DTOs.DocType
{
    public class CreateDocTypeDto : IMapFrom<Core.Domain.Entities.DocType>
    {
        [Required]
        public int ModSerialID { get; set; }
        [Required]
        public string? DocumentType { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "The file path cannot exceed 80 characters. ")]
        public string? FilePath { get; set; }
        public bool Active { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateDocTypeDto, Core.Domain.Entities.DocType>();
        }
    }
}
