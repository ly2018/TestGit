﻿using Microsoft.Extensions.Logging;
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
    public class VerifyCodeAppService : IVerifyCodeAppService
    {
        #region interface inject
        private readonly IVerifyCodeRepository _repository;
        private static ILogger<VerifyCodeAppService> _log;
        /// <summary>
        /// interface inject
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="repository">repository</param>
        public VerifyCodeAppService(ILogger<VerifyCodeAppService> log, IVerifyCodeRepository repository)
        {
            _log = log;
            _repository = repository;
        }
        #endregion

      
	    #region implement interface by master
        /// <summary>
        /// create new record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Create(VerifyCodeDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<VerifyCode>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService Create error occured:" + ex.Message);
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
                _log.LogError("VerifyCodeAppService DeleteBatch error occured:" + ex.Message);
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
                _log.LogError("VerifyCodeAppService Delete error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Edit(VerifyCodeDto dto)
        {
            try
            {
                var entity = _repository.InsertOrUpdate(Mapper.Map<VerifyCode>(dto));
                return entity == null ? false : true;
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService Edit error occured:" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VerifyCodeDto Get(Guid id)
        {
            try
            {
                return Mapper.Map<VerifyCodeDto>(_repository.Get(id));
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService Get error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        public List<VerifyCodeDto> List()
        {
            try
            {
                return Mapper.Map<List<VerifyCodeDto>>(_repository.GetAllList(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService List error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<VerifyCodeDto> QueryList(Expression<Func<VerifyCode, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<VerifyCodeDto>>(_repository.QueryList(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService List error occured:" + ex.Message);
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
        public List<VerifyCodeDto> PageList(Expression<Func<VerifyCode, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<VerifyCodeDto>>(_repository.LoadPageList(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("VerifyCodeAppService PageList error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public VerifyCodeDto Get(Expression<Func<VerifyCode, bool>> predicate)
        {
            try
            {
                return Mapper.Map<VerifyCodeDto>(_repository.FirstOrDefault(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("VerifyCodeAppService Get lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<VerifyCode, bool>> predicate)
        {
            try
            {
                return _repository.Count(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("VerifyCodeAppService Count error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// query properties by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryProperty(Expression<Func<VerifyCode, bool>> predicate, Expression<Func<VerifyCode, object>> selector)
        {
            try
            {
                return _repository.QueryProperty(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("VerifyCodeAppService QueryProperty error occured:" + ex.Message);
                return null;
            }
        }
        #endregion

		#region  implement interface by  by slaves
		/// <summary>
        /// get by id ByReadOnlyDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VerifyCodeDto GetByReadOnlyDB(Guid id)
        {
            try
            {
                return Mapper.Map<VerifyCodeDto>(_repository.GetByReadOnlyDB(id));
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService GetByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query all form table ByReadOnlyDB
        /// </summary>
        /// <returns></returns>
        public List<VerifyCodeDto> ListByReadOnlyDB()
        {
            try
            {
                return Mapper.Map<List<VerifyCodeDto>>(_repository.GetAllListByReadOnlyDB(it => it.ID != Guid.Empty).OrderByDescending(it => it.CreateTime));
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService ListByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  query list by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<VerifyCodeDto> QueryListByReadOnlyDB(Expression<Func<VerifyCode, bool>> predicate)
        {
            try
            {
                return Mapper.Map<List<VerifyCodeDto>>(_repository.QueryListByReadOnlyDB(predicate));
            }
            catch (Exception ex)
            {
                _log.LogError("VerifyCodeAppService ListByReadOnlyDB error occured:" + ex.Message);
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
        public List<VerifyCodeDto> PageListByReadOnlyDB(Expression<Func<VerifyCode, bool>> predicate, int pageSize, int curPage, out int count)
        {
            try
            {
                return Mapper.Map<List<VerifyCodeDto>>(_repository.LoadPageListByReadOnlyDB(curPage, pageSize, out count, predicate, a => a.CreateTime));
            }
            catch (Exception ex)
            {
                count = 0;
                _log.LogError("VerifyCodeAppService PageListByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query signal record by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public VerifyCodeDto GetByReadOnlyDB(Expression<Func<VerifyCode, bool>> predicate)
        {
            try
            {
                return Mapper.Map<VerifyCodeDto>(_repository.FirstOrDefaultByReadOnlyDB(predicate));
            }
            catch (Exception ex)
            {

                _log.LogError("VerifyCodeAppService GetByReadOnlyDB lanbuda error occured:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// query total count by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int CountByReadOnlyDB(Expression<Func<VerifyCode, bool>> predicate)
        {
            try
            {
                return _repository.CountByReadOnlyDB(predicate);
            }
            catch (Exception ex)
            {

                _log.LogError("VerifyCodeAppService CountByReadOnlyDB error occured:" + ex.Message);
                return 0;
            }
        }
        /// <summary> 
        /// query properties by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IQueryable<object> QueryPropertyByReadOnlyDB(Expression<Func<VerifyCode, bool>> predicate, Expression<Func<VerifyCode, object>> selector)
        {
            try
            {
                return _repository.QueryPropertyByReadOnlyDB(predicate, selector);
            }
            catch (Exception ex)
            {

                _log.LogError("VerifyCodeAppService QueryPropertyByReadOnlyDB error occured:" + ex.Message);
                return null;
            }
        }
		#endregion
    }
}
