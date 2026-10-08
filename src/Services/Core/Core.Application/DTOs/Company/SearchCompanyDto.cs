using Core.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Company
{
    public class SearchCompanyDto : IMapFrom<Core.Domain.Entities.Company>
    {
        [Required]
        public int ComSerialID { get; set; }
    }
}
