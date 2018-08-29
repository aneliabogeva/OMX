using AutoMapper;
using OMX.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Test.Mocks
{
    public static class MockAutoMapper
    {
        static MockAutoMapper()
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperConfiguration>());
            
        }

        public static IMapper GetAutoMapper() => Mapper.Instance;
    }
}
