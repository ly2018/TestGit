using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Data
{
    public class SqlDbHelper
    {
        private readonly IServiceProvider _serviceProvider;
        public SqlDbHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///执行sql语句，返回操作结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            using (var context = new TradeDbContext(_serviceProvider.GetRequiredService<DbContextOptions<TradeDbContext>>()))
            {
                return context.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
