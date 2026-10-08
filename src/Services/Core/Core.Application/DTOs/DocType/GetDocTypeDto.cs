using System.ComponentModel.DataAnnotations;
using Core.Application.Common.Mappings;
using AutoMapper;
using Core.Domain.Entities;

namespace Core.Application.DTOs.DocType
{
    public class GetDocTypeDto : IMapFrom<Domain.Entities.DocType>, IMapFrom<Domain.Entities.FilePaths>
    {
        [Required]
        public short DocTypeSerialID { get; set; }
        [Required]
        public int ModSerialID { get; set; }
        [Required]
        public string? DocumentType { get; set; }
        [StringLength(80, ErrorMessage = "The file path cannot exceed 80 characters. ")]
        public string? FilePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.DocType, GetDocTypeDto>();
            profile.CreateMap<FilePaths, GetDocTypeDto>()
                       .ForMember(dest => dest.DocTypeSerialID, opt => opt.MapFrom(src => src.DocType.DocTypeSerialID))
                       .ForMember(dest => dest.ModSerialID, opt => opt.MapFrom(src => src.DocType.ModSerialID))
                       .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocType.DocumentType))
                       .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath)); // adjust if property is not 'Path'
        }
    }
}
