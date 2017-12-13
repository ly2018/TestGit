using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


using Vic.Core.Data;
using Vic.Core.Domain.Entities;
using Vic.Core.Domain.IRepositories;

namespace Vic.Core.Data.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(TradeDbContext dbcontext, ReadOnlyTradeDbContext dbReadContext) : base(dbcontext, dbReadContext)
        {
            
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="roleMenus"></param>
        /// <returns></returns>
        public bool CreateRoleMenu(Role role, List<RoleMenu> roleMenus)
        {
            var oldDatas = _dbContext.Set<Role>().Add(role);
            _dbContext.SaveChanges();
            if (roleMenus != null && roleMenus.Any())
            {
                _dbContext.Set<RoleMenu>().AddRange(roleMenus);
                _dbContext.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool DeleteRole(Expression<Func<Role, bool>> predicate)
        {
            var roleList = _dbContext.Set<Role>().Where(predicate).ToList();
            if (roleList.Any())
            {
                var oldDatas = _dbContext.Set<RoleMenu>().Where(t => roleList.Any(a => a.ID == t.RoleId)).ToList();
                oldDatas.ForEach(it => _dbContext.Set<RoleMenu>().Remove(it));
                _dbContext.SaveChanges();
                roleList.ForEach(it => _dbContext.Set<Role>().Remove(it));
                _dbContext.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetAllMenuListByRole(Guid roleId)
        {
            var roleMenus = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId);
            var menuIds = from t in roleMenus select t.MenuId;
            return menuIds.ToList();
        }

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        public bool UpdateRoleMenu(Guid roleId, List<RoleMenu> roleMenus)
        {
            var oldDatas = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId).ToList();
            oldDatas.ForEach(it => _dbContext.Set<RoleMenu>().Remove(it));
            _dbContext.SaveChanges();
            if (roleMenus != null && roleMenus.Any())
            {
                _dbContext.Set<RoleMenu>().AddRange(roleMenus);
                _dbContext.SaveChanges();
            }
            return true;
        }
    }
}

