using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.DTOs.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Company
{
    public class CompanyDto : IMapFrom<Core.Domain.Entities.Company>
    {
        [Required]
        public int ComSerialID { get; set; }
        [Required]
        public string? ComName { get; set; }
        [Required]
        public string? ComCode { get; set; }
        public byte? SOPayrollPeriodStartDay { get; set; }

        public byte? SOPayrollPeriodEndDay { get; set; }

        public byte? WBPayrollPeriodStartDay { get; set; }

        public byte? WBPayrollPeriodEndDay { get; set; }

    }
}
