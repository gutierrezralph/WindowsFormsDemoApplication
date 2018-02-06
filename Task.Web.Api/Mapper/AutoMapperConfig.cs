using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Task.Core.Domain;
using Task.Web.Api.Mapper.MapProfile;

namespace Task.Web.Api.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((config) =>
            {
                config.AddProfile<EmployeeMapProfile>();
            });
        }
    }
}