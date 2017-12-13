using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.Entities;
using Vic.Core.Utils.Models;

namespace Vic.Core.Application.IServices
{
    public interface IRoleAppService
    {
        #region customer define interface by master
        List<Guid> GetAllMenuListByRole(Guid roleId);
        List<RoleDto> GetAllPageList(int startPage, int pageSize, out int rowCount);
        /// <summary>
        /// 通过角色编号获取对应
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<Right_Privilege> GetMenusByRoleID(string roleId);
        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        bool UpdateRoleMenu(Guid roleId, List<RoleMenuDto> roleMenus);
        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        bool InsertOrUpdate(RoleDto dto);
        /// <summary>
        /// create signal record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Create(RoleDto dto);
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
        bool Edit(RoleDto dto);
        /// <summary>
        /// query by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDto Get(Guid id);
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        List<RoleDto> List();
        /// <summary>
        /// query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<RoleDto> QueryList(Expression<Func<Role, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<RoleDto> PageList(Expression<Func<Role, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        RoleDto Get(Expression<Func<Role, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<Role, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression
        /// </summary>
        IQueryable<object> QueryProperty(Expression<Func<Role, bool>> predicate, Expression<Func<Role, object>> selector);
        #endregion

		#region customer define interface by slaves
		/// <summary>
        /// query by id ByReadOnlyDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDto GetByReadOnlyDB(Guid id);
        /// <summary>
        /// query all form table ByReadOnlyDB
        /// </summary>
        /// <returns></returns>
        List<RoleDto> ListByReadOnlyDB();
        /// <summary>
        /// query list by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<RoleDto> QueryListByReadOnlyDB(Expression<Func<Role, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<RoleDto> PageListByReadOnlyDB(Expression<Func<Role, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        RoleDto GetByReadOnlyDB(Expression<Func<Role, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int CountByReadOnlyDB(Expression<Func<Role, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression ByReadOnlyDB
        /// </summary>
        IQueryable<object> QueryPropertyByReadOnlyDB(Expression<Func<Role, bool>> predicate, Expression<Func<Role, object>> selector);
		#endregion
    }
}
