using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Vic.Core.Domain.Entities;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.IRepositories;
using Vic.Core.Application.IServices;
using Vic.Core.Utils;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Vic.Core.Application.Services
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private static ILogger<MenuAppService> _log;
        /// <summary>
        /// interface inject
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="repository">repository</param>
        public MenuAppService(ILogger<MenuAppService> log,IMenuRepository menuRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _log = log;
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public List<MenuDto> GetAllList()
        {
            var menus = _menuRepository.GetAllList().OrderBy(it => it.SerialNumber);
            //使用AutoMapper进行实体转换
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public List<MenuDto> GetMenusByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            var menus = _menuRepository.LoadPageList(startPage, pageSize, out rowCount, it => it.ParentId == parentId, it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            var menu = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
            return menu == null ? false : true;
        }
        
        /// <summary>
        /// 根据用户获取功能菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="IsTopMenu">是否顶部菜单，默认false</param>
        /// <returns></returns>
        public List<MenuDto> GetMenusByUser(Guid userId, bool IsTopMenu = false)
        {
            List<MenuDto> result = new List<MenuDto>();
            var allMenus = _menuRepository.GetAllList(it => it.IsTopMenu == IsTopMenu).OrderBy(it => it.SerialNumber);
            if (userId == ConstDefine.SuperAdminID) //超级管理员
                return Mapper.Map<List<MenuDto>>(allMenus);
            var user = _userRepository.GetWithRoles(userId);
            if (user == null)
                return result;
            var userRoles = user.UserRoles;
            List<Guid> menuIds = new List<Guid>();
            foreach (var role in userRoles)
            {
                menuIds = menuIds.Union(_roleRepository.GetAllMenuListByRole(role.RoleId)).ToList();
            }
            allMenus = allMenus.Where(it => menuIds.Contains(it.ID) && it.IsTopMenu == IsTopMenu).OrderBy(it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(allMenus);
        }
        

        #region interface Implemente
        /// <summary>
        /// create new record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Create(MenuDto dto)
        {
            try
            {
                var entity = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("MenuAppService Create error occured:" + ex.Message);
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
                _menuRepository.Delete(it => ids.Contains(it.ID));
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError("MenuAppService DeleteBatch error occured:" + ex.Message);
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
                _menuRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError("MenuAppService Delete error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Edit(MenuDto dto)
        {
            try
            {
                var entity = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("MenuAppService Edit error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuDto Get(Guid id)
        {
            try
            {
                return Mapper.Map<MenuDto>(_menuRepository.Get(id));
            }
            catch (Exception ex)
            {
                _log.LogError("MenuAppService Get error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        public List<MenuDto> List()
        {
            try
            {
                return Mapper.Map<List<MenuDto>>(_menuRepository.GetAllList(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("MenuAppService List error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<MenuDto> QueryList(Expression<Func<Menu, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<MenuDto>>(_menuRepository.QueryList(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("MenuAppService List error occured:" + ex.Message);
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
        public List<MenuDto> PageList(Expression<Func<Menu, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<MenuDto>>(_menuRepository.LoadPageList(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("MenuAppService PageList error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public MenuDto Get(Expression<Func<Menu, bool>> predicate)
        {
            try
            {
                return Mapper.Map<MenuDto>(_menuRepository.FirstOrDefault(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("MenuAppService Get lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<Menu, bool>> predicate)
        {
            try
            {
                return _menuRepository.Count(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("MenuAppService Count error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// query properties by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryProperty(Expression<Func<Menu, bool>> predicate, Expression<Func<Menu, object>> selector)
        {
            try
            {
                return _menuRepository.QueryProperty(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("MenuAppService QueryProperty error occured:" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
