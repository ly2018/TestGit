using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.Entities;

namespace Vic.Core.Application.IServices
{
    public interface IUserAppService
    {
        #region customer define interface by master
        UserDto CheckUser(string userName, string password);
        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        UserDto InsertOrUpdate(UserDto dto);
        /// <summary>
        /// create signal record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Create(UserDto dto);
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
        bool Edit(UserDto dto);
        /// <summary>
        /// query by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDto Get(Guid id);
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        List<UserDto> List();
        /// <summary>
        /// query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<UserDto> QueryList(Expression<Func<User, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<UserDto> PageList(Expression<Func<User, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        UserDto Get(Expression<Func<User, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<User, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression
        /// </summary>
        IQueryable<object> QueryProperty(Expression<Func<User, bool>> predicate, Expression<Func<User, object>> selector);
        #endregion

		#region customer define interface by slaves
		/// <summary>
        /// query by id ByReadOnlyDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDto GetByReadOnlyDB(Guid id);
        /// <summary>
        /// query all form table ByReadOnlyDB
        /// </summary>
        /// <returns></returns>
        List<UserDto> ListByReadOnlyDB();
        /// <summary>
        /// query list by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<UserDto> QueryListByReadOnlyDB(Expression<Func<User, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<UserDto> PageListByReadOnlyDB(Expression<Func<User, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        UserDto GetByReadOnlyDB(Expression<Func<User, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int CountByReadOnlyDB(Expression<Func<User, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression ByReadOnlyDB
        /// </summary>
        IQueryable<object> QueryPropertyByReadOnlyDB(Expression<Func<User, bool>> predicate, Expression<Func<User, object>> selector);
		#endregion
    }
}
