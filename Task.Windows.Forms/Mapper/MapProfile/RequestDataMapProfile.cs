using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DTO.Request;
using Task.DTO.Response;

namespace Task.Windows.Forms.Mapper.MapProfile
{
    class RequestDataMapProfile : Profile
    {
        public RequestDataMapProfile()
        {
            CreateMap<EmployeeRequest,ResponseData>()
                 .ForMember(d => d.Id, m => m.Ignore())
                .ForMember(d => d.DateCreated, m => m.Ignore());
        }
    }
}
