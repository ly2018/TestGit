﻿using System;
using System.Collections.Generic;
using System.Text;
using Vic.Core.Domain.Entities;
using Vic.Core.Domain.IRepositories;

namespace Vic.Core.Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(TradeDbContext dbcontext, ReadOnlyTradeDbContext dbReadContext) : base(dbcontext, dbReadContext)
        {

        }
    }
}

