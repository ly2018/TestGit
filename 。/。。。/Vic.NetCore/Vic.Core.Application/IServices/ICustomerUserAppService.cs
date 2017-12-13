using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.Entities;

namespace Vic.Core.Application.IServices
{
    public interface ICustomerUserAppService
    {
        #region customer define interface by master
        /// <summary>
        /// create signal record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Create(CustomerUserDto dto);
        /// <summary>
        /// delete by ids
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        bool DeleteBatch(List<Guid> ids);
        /// <summary>
        /// delete by ids
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        bool Delete(Guid id);
        /// <summary>
        /// edit signal dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Edit(CustomerUserDto dto);
        /// <summary>
        /// query by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CustomerUserDto Get(Guid id);
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        List<CustomerUserDto> List();
        /// <summary>
        /// query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<CustomerUserDto> QueryList(Expression<Func<CustomerUser, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<CustomerUserDto> PageList(Expression<Func<CustomerUser, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        CustomerUserDto Get(Expression<Func<CustomerUser, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<CustomerUser, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression
        /// </summary>
        IQueryable<object> QueryProperty(Expression<Func<CustomerUser, bool>> predicate, Expression<Func<CustomerUser, object>> selector);
        #endregion

		#region customer define interface by slaves
		/// <summary>
        /// query by id ByReadOnlyDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CustomerUserDto GetByReadOnlyDB(Guid id);
        /// <summary>
        /// query all form table ByReadOnlyDB
        /// </summary>
        /// <returns></returns>
        List<CustomerUserDto> ListByReadOnlyDB();
        /// <summary>
        /// query list by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<CustomerUserDto> QueryListByReadOnlyDB(Expression<Func<CustomerUser, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<CustomerUserDto> PageListByReadOnlyDB(Expression<Func<CustomerUser, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        CustomerUserDto GetByReadOnlyDB(Expression<Func<CustomerUser, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int CountByReadOnlyDB(Expression<Func<CustomerUser, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression ByReadOnlyDB
        /// </summary>
        IQueryable<object> QueryPropertyByReadOnlyDB(Expression<Func<CustomerUser, bool>> predicate, Expression<Func<CustomerUser, object>> selector);
		#endregion
    }
}
