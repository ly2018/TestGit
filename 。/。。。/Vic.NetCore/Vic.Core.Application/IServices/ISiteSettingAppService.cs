using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.Entities;

namespace Vic.Core.Application.IServices
{
    public interface ISiteSettingAppService
    {
        #region customer define interface by master
        /// <summary>
        /// create signal record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Create(SiteSettingDto dto);
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
        bool Edit(SiteSettingDto dto);
        /// <summary>
        /// query by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SiteSettingDto Get(Guid id);
        /// <summary>
        /// query all form table
        /// </summary>
        /// <returns></returns>
        List<SiteSettingDto> List();
        /// <summary>
        /// query list by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<SiteSettingDto> QueryList(Expression<Func<SiteSetting, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<SiteSettingDto> PageList(Expression<Func<SiteSetting, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        SiteSettingDto Get(Expression<Func<SiteSetting, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<SiteSetting, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression
        /// </summary>
        IQueryable<object> QueryProperty(Expression<Func<SiteSetting, bool>> predicate, Expression<Func<SiteSetting, object>> selector);
        #endregion

		#region customer define interface by slaves
		/// <summary>
        /// query by id ByReadOnlyDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SiteSettingDto GetByReadOnlyDB(Guid id);
        /// <summary>
        /// query all form table ByReadOnlyDB
        /// </summary>
        /// <returns></returns>
        List<SiteSettingDto> ListByReadOnlyDB();
        /// <summary>
        /// query list by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
		List<SiteSettingDto> QueryListByReadOnlyDB(Expression<Func<SiteSetting, bool>> predicate);
        /// <summary>
        /// query pager by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		List<SiteSettingDto> PageListByReadOnlyDB(Expression<Func<SiteSetting, bool>> predicate, int pageSize, int curPage, out int count);
        /// <summary>
        ///query signal record by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        SiteSettingDto GetByReadOnlyDB(Expression<Func<SiteSetting, bool>> predicate);
        /// <summary>
        ///query total count by lanbuda expression ByReadOnlyDB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int CountByReadOnlyDB(Expression<Func<SiteSetting, bool>> predicate);

        /// <summary>
        ///query properties by lanbuda expression ByReadOnlyDB
        /// </summary>
        IQueryable<object> QueryPropertyByReadOnlyDB(Expression<Func<SiteSetting, bool>> predicate, Expression<Func<SiteSetting, object>> selector);
		#endregion
    }
}
