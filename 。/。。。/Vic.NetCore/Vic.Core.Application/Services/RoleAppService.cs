
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Vic.Core.Domain.Entities;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.IRepositories;
using Vic.Core.Application.IServices;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Vic.Core.Utils;
using Vic.Core.Utils.Models;

namespace Vic.Core.Application.Services
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IRoleRepository _repository;
        private readonly IMenuAppService _menuAppService;
        private static ILogger<RoleAppService> _log;
        /// <summary>
        /// interface inject
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="repository">repository</param>
        public RoleAppService(ILogger<RoleAppService> log, IRoleRepository repository, IMenuAppService menuAppService)
        {
            _log = log;
            _menuAppService = menuAppService;
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<RoleDto> GetAllList()
        {
            return Mapper.Map<List<RoleDto>>(_repository.GetAllList().OrderBy(it => it.Code));
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="rowCount">数据总数</param>
        /// <returns></returns>
        public List<RoleDto> GetAllPageList(int startPage, int pageSize, out int rowCount)
        {
            return Mapper.Map<List<RoleDto>>(_repository.LoadPageList(startPage, pageSize, out rowCount, null, it => it.Code));
        }

        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public bool InsertOrUpdate(RoleDto dto)
        {
            var menu = _repository.InsertOrUpdate(Mapper.Map<Role>(dto));
            return menu == null ? false : true;
        }


        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetAllMenuListByRole(Guid roleId)
        {
            return _repository.GetAllMenuListByRole(roleId);
        }

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        public bool UpdateRoleMenu(Guid roleId, List<RoleMenuDto> roleMenus)
        {
            return _repository.UpdateRoleMenu(roleId, Mapper.Map<List<RoleMenu>>(roleMenus));
        }


        #region interface Implemente
        /// <summary>
        /// create new record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Create(RoleDto dto)
        {
            try
            {
                List<RoleMenu> roleMenus = new List<RoleMenu>();
                if (!string.IsNullOrEmpty(dto.RoleValue))
                {
                    string[] menuIds = dto.RoleValue.Split(',');
                    Guid temp = Guid.Empty;
                    foreach (var item in menuIds)
                    {
                        if (!string.IsNullOrEmpty(item) && Guid.TryParse(item, out temp))
                            roleMenus.Add(new RoleMenu() { RoleId = dto.ID, MenuId = temp });
                    }
                }
                return _repository.CreateRoleMenu(Mapper.Map<Role>(dto), roleMenus);
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService Create error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Delete Batch by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteBatch(List<Guid> ids)
        {
            try
            {
                return _repository.DeleteRole(it => ids.Contains(it.ID) && it.ID != ConstDefine.SuperRoleID);
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService DeleteBatch error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            try
            {
                if (id == ConstDefine.SuperRoleID)
                    return false;
                return _repository.DeleteRole(it => it.ID == id && it.ID != ConstDefine.SuperRoleID);
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService Delete error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Edit(RoleDto dto)
        {
            try
            {
                List<RoleMenu> roleMenus = new List<RoleMenu>();
                if (!string.IsNullOrEmpty(dto.RoleValue))
                {
                    string[] menuIds = dto.RoleValue.Split(',');
                    Guid temp = Guid.Empty;
                    foreach (var item in menuIds)
                    {
                        if (!roleMenus.Any(a => a.MenuId == temp) && Guid.TryParse(item, out temp))
                            roleMenus.Add(new RoleMenu() { RoleId = dto.ID, MenuId = temp });
                    }
                }
                return _repository.UpdateRoleMenu(dto.ID, roleMenus);
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService Edit error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDto Get(Guid id)
        {
            try
            {
                return Mapper.Map<RoleDto>(_repository.Get(id));
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService Get error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        public List<RoleDto> List()
        {
            try
            {
                return Mapper.Map<List<RoleDto>>(_repository.GetAllList(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService List error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<RoleDto> QueryList(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<RoleDto>>(_repository.QueryList(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService List error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query pager by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<RoleDto> PageList(Expression<Func<Role, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<RoleDto>>(_repository.LoadPageList(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("RoleAppService PageList error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RoleDto Get(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                return Mapper.Map<RoleDto>(_repository.FirstOrDefault(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("RoleAppService Get lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                return _repository.Count(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("RoleAppService Count error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// query properties by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryProperty(Expression<Func<Role, bool>> predicate, Expression<Func<Role, object>> selector)
        {
            try
            {
                return _repository.QueryProperty(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("RoleAppService QueryProperty error occured:" + ex.Message);
                return null;
            }
        }
        #endregion

        #region 权限
        /// <summary>
        /// 通过角色编号获取对应
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<Right_Privilege> GetMenusByRoleID(string roleId)
        {
            var list = _menuAppService.QueryList(a => a.ID != Guid.Empty);
            List<Right_Privilege> Dtopilist = new List<Right_Privilege>();
            var parentList = list.Where(a => a.ParentId == Guid.Empty && a.Type == (int)Utils.Enum.AM_Enum.MenuTypeEnum.ParentMenu).OrderBy(a => a.SerialNumber);
            Guid currRoleID = Guid.Empty;
            Role currRole = null;
            if (Guid.TryParse(roleId, out currRoleID))
            {
                currRole = _repository.Get(currRoleID);
            }
            if (!string.IsNullOrEmpty(roleId))
            {

            }
            foreach (var item in parentList)
            {
                Right_Privilege dtopil = new Right_Privilege()
                {
                    cateName = item.Name,
                    cateId = item.ID
                };
                dtopil.cateQuanxian = new List<Right_Control>() { new Right_Control()
                {
                    quanxian = "查看",
                    quanxianid = item.ID,
                    state = 0
                }};
                if (currRole != null && currRole.RoleMenus.Any(a => a.MenuId.Equals(item.ID)))
                {
                    dtopil.cateQuanxian[0].state = 1;
                }
                dtopil.childCateList = new List<Right_Privilege>();
                dtopil.childCateList.AddRange(GetChildPermissions(list, item, currRole));
                if (item.ParentId != Guid.Empty)
                    dtopil.cateQuanxian.AddRange(GetChildMenus(list, item, currRole));
                Dtopilist.Add(dtopil);
            }
            return Dtopilist;
        }

        private IEnumerable<Right_Control> GetChildMenus(List<MenuDto> list, MenuDto item, Role currRole)
        {
            List<Right_Control> permissions = new List<Right_Control>();
            var query = list.Where(a => a.ParentId == item.ID).OrderBy(a => a.SerialNumber);
            foreach (var child in query)
            {
                var dtoControl = new Right_Control() { quanxianid = child.ID, quanxian = child.Name, state = 0 };
                if (currRole != null && currRole.RoleMenus.Any(a => a.MenuId.Equals(child.ID)))
                {
                    dtoControl.state = 1;
                }
                permissions.Add(dtoControl);
            }
            return permissions;
        }

        private IEnumerable<Right_Privilege> GetChildPermissions(List<MenuDto> list, MenuDto item, Role currRole)
        {
            List<Right_Privilege> privileges = new List<Right_Privilege>();
            var query = list.Where(a => a.ParentId == item.ID).OrderBy(a => a.SerialNumber);
            foreach (var child in query)
            {
                Right_Privilege dtopil = new Right_Privilege()
                {
                    cateName = child.Name,
                    cateId = child.ID,
                    catePid = child.ID

                };
                dtopil.cateQuanxian = new List<Right_Control>() { new Right_Control()
                {
                    quanxian = "查看",
                    quanxianid = child.ID,
                    state = 0
                }};
                if (currRole != null && currRole.RoleMenus.Any(a => a.MenuId.Equals(child.ID)))
                {
                    dtopil.cateQuanxian[0].state = 1;
                }
                dtopil.childCateList = new List<Right_Privilege>();
                if (child.Type == 1)
                    dtopil.childCateList.AddRange(GetChildPermissions(list, child, currRole));
                dtopil.cateQuanxian.AddRange(GetChildMenus(list, child, currRole));
                privileges.Add(dtopil);
            }
            return privileges;
        }
        #endregion


        #region  implement interface by slaves
        /// <summary>
        /// get by id ByReadOnlyDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDto GetByReadOnlyDB(Guid id)
        {
            try
            {
                return Mapper.Map<RoleDto>(_repository.GetByReadOnlyDB(id));
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService GetByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query all form table ByReadOnlyDB
        /// </summary>
        /// <returns></returns>
        public List<RoleDto> ListByReadOnlyDB()
        {
            try
            {
                return Mapper.Map<List<RoleDto>>(_repository.GetAllListByReadOnlyDB(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService ListByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<RoleDto> QueryListByReadOnlyDB(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<RoleDto>>(_repository.QueryListByReadOnlyDB(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("RoleAppService ListByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query pager by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<RoleDto> PageListByReadOnlyDB(Expression<Func<Role, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<RoleDto>>(_repository.LoadPageListByReadOnlyDB(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("RoleAppService PageListByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RoleDto GetByReadOnlyDB(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                return Mapper.Map<RoleDto>(_repository.FirstOrDefaultByReadOnlyDB(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("RoleAppService GetByReadOnlyDB lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int CountByReadOnlyDB(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                return _repository.CountByReadOnlyDB(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("RoleAppService CountByReadOnlyDB error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary> 
        /// query properties by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryPropertyByReadOnlyDB(Expression<Func<Role, bool>> predicate, Expression<Func<Role, object>> selector)
        {
            try
            {
                return _repository.QueryPropertyByReadOnlyDB(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("RoleAppService QueryPropertyByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
