using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.Entities;

namespace Vic.Core.Application.IServices
{
    public interface IMenuAppService
    {
        #region customer define interface by master
        List<MenuDto> GetMenusByParent(Guid parentId, int startPage, int pageSize, out int rowCount);
        /// <summary>
        /// 根据用户获取功能菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="IsTopMenu">是否顶部菜单，默认false</param>
        /// <returns></returns>
        List<MenuDto> GetMenusByUser(Guid userId, bool IsTopMenu = false);
        /// <summary>
        /// create signal record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Create(MenuDto dto);
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
        bool Edit(MenuDto dto);
        /// <summary>
        /// query by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MenuDto Get(Guid id);
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        List<MenuDto> List();
        /// <summary>
        /// query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<MenuDto> QueryList(Expression<Func<Menu, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<MenuDto> PageList(Expression<Func<Menu, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        MenuDto Get(Expression<Func<Menu, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<Menu, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression
        /// </summary>
        IQueryable<object> QueryProperty(Expression<Func<Menu, bool>> predicate, Expression<Func<Menu, object>> selector);
        #endregion

		#region customer define interface by slaves
		/// <summary>
        /// query by id ByReadOnlyDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MenuDto GetByReadOnlyDB(Guid id);
        /// <summary>
        /// query all form table ByReadOnlyDB
        /// </summary>
        /// <returns></returns>
        List<MenuDto> ListByReadOnlyDB();
        /// <summary>
        /// query list by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<MenuDto> QueryListByReadOnlyDB(Expression<Func<Menu, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<MenuDto> PageListByReadOnlyDB(Expression<Func<Menu, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        MenuDto GetByReadOnlyDB(Expression<Func<Menu, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int CountByReadOnlyDB(Expression<Func<Menu, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression ByReadOnlyDB
        /// </summary>
        IQueryable<object> QueryPropertyByReadOnlyDB(Expression<Func<Menu, bool>> predicate, Expression<Func<Menu, object>> selector);
		#endregion
    }
}
