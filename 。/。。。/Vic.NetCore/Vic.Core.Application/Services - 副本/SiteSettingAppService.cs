using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Vic.Core.Application.IServices;
using Vic.Core.Domain.IRepositories;
using System.Linq;
using System.Linq.Expressions;
using Vic.Core.Application.Dtos;
using AutoMapper;
using Vic.Core.Domain.Entities;

namespace Vic.Core.Application.Services
{
    public class SiteSettingAppService : ISiteSettingAppService
    {
        #region interface inject
        private readonly ISiteSettingRepository _repository;
        private static ILogger<SiteSettingAppService> _log;
        /// <summary>
        /// interface inject
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="repository">repository</param>
        public SiteSettingAppService(ILogger<SiteSettingAppService> log, ISiteSettingRepository repository)
        {
            _log = log;
            _repository = repository;
        }
        #endregion

        #region interface Implemente
        /// <summary>
        /// create new record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Create(SiteSettingDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<SiteSetting>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("SiteSettingAppService Create error occured:" + ex.Message);
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
                _repository.Delete(it => ids.Contains(it.ID));
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError("SiteSettingAppService DeleteBatch error occured:" + ex.Message);
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
                _log.LogError("SiteSettingAppService Delete error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Edit(SiteSettingDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<SiteSetting>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("SiteSettingAppService Edit error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SiteSettingDto Get(Guid id)
        {
            try
            {
                return Mapper.Map<SiteSettingDto>(_repository.Get(id));
            }
            catch (Exception ex)
            {
                _log.LogError("SiteSettingAppService Get error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        public List<SiteSettingDto> List()
        {
            try
            {
                return Mapper.Map<List<SiteSettingDto>>(_repository.GetAllList(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("SiteSettingAppService List error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<SiteSettingDto> QueryList(Expression<Func<SiteSetting, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<SiteSettingDto>>(_repository.QueryList(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("SiteSettingAppService List error occured:" + ex.Message);
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
        public List<SiteSettingDto> PageList(Expression<Func<SiteSetting, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<SiteSettingDto>>(_repository.LoadPageList(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("SiteSettingAppService PageList error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SiteSettingDto Get(Expression<Func<SiteSetting, bool>> predicate)
        {
            try
            {
                return Mapper.Map<SiteSettingDto>(_repository.FirstOrDefault(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("SiteSettingAppService Get lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<SiteSetting, bool>> predicate)
        {
            try
            {
                return _repository.Count(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("SiteSettingAppService Count error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// query properties by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryProperty(Expression<Func<SiteSetting, bool>> predicate, Expression<Func<SiteSetting, object>> selector)
        {
            try
            {
                return _repository.QueryProperty(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("SiteSettingAppService QueryProperty error occured:" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
