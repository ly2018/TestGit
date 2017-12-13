using System;
using System.Collections.Generic;
using System.Text;
using Vic.Core.Domain.Entities;
using Vic.Core.Domain.IRepositories;

namespace Vic.Core.Data.Repositories
{
    public class MailConfigRepository : RepositoryBase<MailConfig>, IMailConfigRepository
    {
        public MailConfigRepository(TradeDbContext dbcontext, ReadOnlyTradeDbContext dbReadContext) : base(dbcontext, dbReadContext)
        {

        }
    }
}

