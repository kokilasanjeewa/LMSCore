using Core.Application.DTOs.City;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common.Dtos
{
    public static class FormOptionHelper
    {
        public static List<FormOptionDto> FromEnum<T>()
            where T : struct, Enum
        {
            return Enum.GetValues<T>()
                .Select(x => new FormOptionDto
                {
                    Id = Convert.ToInt32(x),
                    Label = GetDisplayName(x),
                    Value = Convert.ToInt32(x)
                })
                .ToList();
        }

        private static string GetDisplayName<T>(T value)
            where T : struct, Enum
        {
            var member = typeof(T)
                .GetMember(value.ToString())
                .FirstOrDefault();

            var attribute = member?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return attribute?.Name ?? value.ToString();
        }
    }
}
