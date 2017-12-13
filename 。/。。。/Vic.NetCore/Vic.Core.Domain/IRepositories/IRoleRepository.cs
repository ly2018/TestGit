using Vic.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Vic.Core.Domain.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        List<Guid> GetAllMenuListByRole(Guid roleId);

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        bool UpdateRoleMenu(Guid roleId, List<RoleMenu> roleMenus);
        /// <summary>
        /// 删除角色，清理菜单
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool DeleteRole(Expression<Func<Role, bool>> predicate);
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="roleMenus"></param>
        /// <returns></returns>
        bool CreateRoleMenu(Role role, List<RoleMenu> roleMenus);
    }
}
