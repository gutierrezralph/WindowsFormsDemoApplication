using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Windows.Forms.Mapper.MapProfile;

namespace Task.Windows.Forms.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((config) =>
            {
                config.AddProfile<RequestDataMapProfile>();
            });
        }
    }
}
