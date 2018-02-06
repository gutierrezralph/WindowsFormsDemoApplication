using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task.Core.Domain;
using Task.DTO.Request;

namespace Task.Web.Api.Mapper.MapProfile
{
    public class EmployeeMapProfile : Profile
    {
        public EmployeeMapProfile()
        {
            CreateMap<EmployeeRequest, Employee>()
                .ForMember(d => d.Id, m => m.Ignore())
                .ForMember(d => d.DateCreated, m => m.Ignore());
        }

    }
}