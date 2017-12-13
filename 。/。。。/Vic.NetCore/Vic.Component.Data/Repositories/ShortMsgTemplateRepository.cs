using System;
using System.Collections.Generic;
using System.Text;
using Vic.Core.Domain.Entities;
using Vic.Core.Domain.IRepositories;

namespace Vic.Core.Data.Repositories
{
    public class ShortMsgTemplateRepository : RepositoryBase<ShortMsgTemplate>, IShortMsgTemplateRepository
    {
        public ShortMsgTemplateRepository(TradeDbContext dbcontext, ReadOnlyTradeDbContext dbReadContext) : base(dbcontext, dbReadContext)
        {

        }
    }
}

