using AutoMapper;
using OMX.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Services
{
    public abstract class BaseService
    {
        protected BaseService(OmxDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        protected OmxDbContext DbContext { get; private set; }
        protected IMapper Mapper { get; private set; }
    }
}
