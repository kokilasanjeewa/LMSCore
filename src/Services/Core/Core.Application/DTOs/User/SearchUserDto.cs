using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.DTOs.Company;
using Core.Application.Helper;
using Core.Domain.Entities;

namespace Core.Application.DTOs.User
{
    public class SearchUserDto : IMapFrom<Core.Domain.Entities.User>
    {
        public int UserSerialID { get; set; }
        public long? EESerialID { get; set; }
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public PermissionType PermissionType { get; set; }
        public int PermissionInt { get; set; }
        public string? Status { get; set; }
        public int? GrpSerialID { get; set; }
        public string? PassWd { get; set; }
        public bool Active { get; set; }
        public List<SearchCompanyDto>? Companies { get; set; } = new List<SearchCompanyDto>();

        private List<SearchMenuDto>? _menuPermissions = new List<SearchMenuDto>();

        public List<SearchMenuDto>? MenuPermissions
        {
            get
            {
                // Return the sorted list
                return _menuPermissions?.OrderBy(mp => mp.MnuID).ToList();
            }
            set
            {
                _menuPermissions = value;
            }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Domain.Entities.User, SearchUserDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active ? "Active" : "Inactive"))
                .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.Companies))
               .ForMember(dest => dest.PermissionInt, opt => opt.MapFrom(src => EnumConverter.EnumToNumber(src.PermissionType)));

            profile.CreateMap<UserCompany, SearchCompanyDto>()
                .ForMember(dest => dest.ComSerialID, opt => opt.MapFrom(src => src.ComSerialID));

            profile.CreateMap<UserMenuPermission, SearchMenuDto>()
                .ForMember(dest => dest.UserMnuPermsSerialID, opt => opt.MapFrom(src => src.UserMnuPermsSerialID))
                .ForMember(dest => dest.MnuMTBR, opt => opt.MapFrom(src => src.Menu.MnuMTBR))
                .ForMember(dest => dest.IsBtnInc, opt => opt.MapFrom(src => src.Menu.IsBtnInc))
                .ForMember(dest => dest.IsRptInc, opt => opt.MapFrom(src => src.Menu.IsRptInc));

        }
    }
}

#region

//  [NotMapped]
// public List<SearchMenuDto>? GroupMenuPermissions { get; set; } = new List<SearchMenuDto>();


/* .ForMember(dest => dest.GroupMenuPermissions, opt => opt.MapFrom(src =>
         src.Group.GroupMenus.Select(x => new SearchMenuDto
         {
             UserSerialID = src.UserSerialID,
             MnuSerialID = x.Menu.MnuSerialID,
             MnuMTBR = x.Menu.MnuMTBR,
             IsBtnInc = x.Menu.IsBtnInc,
             IsRptInc = x.Menu.IsRptInc
         })))*/

#endregion