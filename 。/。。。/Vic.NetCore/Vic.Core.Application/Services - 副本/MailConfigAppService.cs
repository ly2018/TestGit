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
    public class MailConfigAppService : IMailConfigAppService
    {
        #region interface inject
        private readonly IMailConfigRepository _repository;
        private static ILogger<MailConfigAppService> _log;
        /// <summary>
        /// interface inject
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="repository">repository</param>
        public MailConfigAppService(ILogger<MailConfigAppService> log, IMailConfigRepository repository)
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
        public bool Create(MailConfigDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<MailConfig>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("MailConfigAppService Create error occured:" + ex.Message);
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
                _log.LogError("MailConfigAppService DeleteBatch error occured:" + ex.Message);
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
                _log.LogError("MailConfigAppService Delete error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Edit(MailConfigDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<MailConfig>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("MailConfigAppService Edit error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MailConfigDto Get(Guid id)
        {
            try
            {
                return Mapper.Map<MailConfigDto>(_repository.Get(id));
            }
            catch (Exception ex)
            {
                _log.LogError("MailConfigAppService Get error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        public List<MailConfigDto> List()
        {
            try
            {
                return Mapper.Map<List<MailConfigDto>>(_repository.GetAllList(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("MailConfigAppService List error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<MailConfigDto> QueryList(Expression<Func<MailConfig, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<MailConfigDto>>(_repository.QueryList(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("MailConfigAppService List error occured:" + ex.Message);
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
        public List<MailConfigDto> PageList(Expression<Func<MailConfig, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<MailConfigDto>>(_repository.LoadPageList(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("MailConfigAppService PageList error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public MailConfigDto Get(Expression<Func<MailConfig, bool>> predicate)
        {
            try
            {
                return Mapper.Map<MailConfigDto>(_repository.FirstOrDefault(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("MailConfigAppService Get lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<MailConfig, bool>> predicate)
        {
            try
            {
                return _repository.Count(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("MailConfigAppService Count error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// query properties by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryProperty(Expression<Func<MailConfig, bool>> predicate, Expression<Func<MailConfig, object>> selector)
        {
            try
            {
                return _repository.QueryProperty(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("MailConfigAppService QueryProperty error occured:" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
