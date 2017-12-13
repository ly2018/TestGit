using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vic.Core.Domain.Entities;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.IRepositories;
using Vic.Core.Application.IServices;
using NLog;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Vic.Core.Application.Services
{
    public class DepartmentAppService : IDepartmentAppService
    {
        private readonly IDepartmentRepository _repository; 
        private static ILogger<DepartmentAppService> _log;
        /// <summary>
        /// interface inject
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="repository">repository</param>
        public DepartmentAppService(ILogger<DepartmentAppService> log, IDepartmentRepository repository)
        {
            _log = log;
            _repository = repository;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<DepartmentDto> GetAllList()
        {
            return Mapper.Map<List<DepartmentDto>>(_repository.GetAllList(it => it.ID != Guid.Empty).OrderBy(it => it.Code));
        }

        /// <summary>
        /// 根据父级Id获取子级列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="rowCount">数据总数</param>
        /// <returns></returns>
        public List<DepartmentDto> GetChildrenByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            return Mapper.Map<List<DepartmentDto>>(_repository.LoadPageList(startPage, pageSize, out rowCount, it => it.ParentId == parentId, it => it.Code));
        }

        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public bool InsertOrUpdate(DepartmentDto dto)
        {
            var menu = _repository.InsertOrUpdate(Mapper.Map<Department>(dto));
            return menu == null ? false : true;
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
                _repository.Delete(it => ids.Contains(it.ID));
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError("DepartmentAppService DeleteBatch error occured:" + ex.Message);
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
                _repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError("DepartmentAppService Delete error occured:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public DepartmentDto Get(Guid id)
        {
            return Mapper.Map<DepartmentDto>(_repository.Get(id));
        }

        #region interface Implemente
        /// <summary>
        /// create new record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Create(DepartmentDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<Department>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("DepartmentAppService Create error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Edit(DepartmentDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<Department>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("DepartmentAppService Edit error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        public List<DepartmentDto> List()
        {
            try
            {
                return Mapper.Map<List<DepartmentDto>>(_repository.GetAllList(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("DepartmentAppService List error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<DepartmentDto> QueryList(Expression<Func<Department, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<DepartmentDto>>(_repository.QueryList(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("DepartmentAppService List error occured:" + ex.Message);
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
        public List<DepartmentDto> PageList(Expression<Func<Department, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<DepartmentDto>>(_repository.LoadPageList(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("DepartmentAppService PageList error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public DepartmentDto Get(Expression<Func<Department, bool>> predicate)
        {
            try
            {
                return Mapper.Map<DepartmentDto>(_repository.FirstOrDefault(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("DepartmentAppService Get lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<Department, bool>> predicate)
        {
            try
            {
                return _repository.Count(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("DepartmentAppService Count error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// query properties by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryProperty(Expression<Func<Department, bool>> predicate, Expression<Func<Department, object>> selector)
        {
            try
            {
                return _repository.QueryProperty(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("DepartmentAppService QueryProperty error occured:" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
