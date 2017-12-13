using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Vic.Core.Domain.Entities;

namespace Vic.Core.Domain.IRepositories
{
    public interface ICustomerUserRepository : IRepository<CustomerUser, Guid>
    {
    }
}
