using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using RockFramework.Repository.Data;

namespace RockFramework.Repository
{
    /// <summary>
    /// 业务对象抽象类【保护所有对象共有行为及属性】
    /// </summary>
    /// <typeparam name="T">业务类型</typeparam>
    public class BaseBusinessRepositoryImpl<T> : IBusinessRepository<T>
    {
        /// <summary>
        /// 当前对象Dao层
        /// </summary>
        public IRepository<T> CurrentDao { get; set; }

        /// <summary>
        /// Dao辅助类【在业务层只用于查询使用】
        /// </summary>
        public RockAdoTemplate AdoTemplate { get; set; }


        public object Save(T entity)
        {
            return CurrentDao.Save(entity);
        }

        public bool Delete(T entity)
        {
            return CurrentDao.Delete(entity);
        }

        public bool Delete(string whereStr)
        {
            return this.CurrentDao.Delete(whereStr);
        }

        public bool Update(T entity)
        {
            return this.CurrentDao.Update(entity);
        }

        public T Get(object id)
        {
            return this.CurrentDao.Get(id);
        }

        public T Load(object id)
        {
            return this.CurrentDao.Load(id);
        }

        public IList<T> SelectAll()
        {
            return this.CurrentDao.SelectAll();
        }

        public IList<T> Select(string whereStr)
        {
            return this.CurrentDao.Select(whereStr);
        }

        public IList<T> SelectDistinct(string whereStr, string field, string alias)
        {
            throw new NotImplementedException();
        }

        public IList<T> SelectWithOrder(string whereStr, string propertyName, bool ascending)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return this.CurrentDao.Count();
        }

        public int Count(string whereStr)
        {
            return this.CurrentDao.Count(whereStr);
        }

        public int Count(string whereStr, Dictionary<string, HqlParameter> hqlParameterDic)
        {
            return this.CurrentDao.Count(whereStr, hqlParameterDic);
        }

        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSize)
        {
            return this.CurrentDao.SelectObjectToPagination(currentPageNo, pageSize);
        }

        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSiz, string whereStr)
        {
            return this.CurrentDao.SelectObjectToPagination(currentPageNo, pageSiz, whereStr);
        }

        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSiz, string whereStr, Dictionary<string, HqlParameter> hqlParameterDic)
        {
            return this.CurrentDao.SelectObjectToPagination(currentPageNo, pageSiz, whereStr, hqlParameterDic);
        }

        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSize, string whereStr, string orderbyStr, Direction orderByDirection)
        {
            return this.CurrentDao.SelectObjectToPagination(currentPageNo, pageSize, whereStr, orderbyStr,
                                                            orderByDirection);
        }

        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSize, string whereStr, string orderbyStr, Direction orderByDirection, Dictionary<string, HqlParameter> hqlParameterDic)
        {
            return this.CurrentDao.SelectObjectToPagination(currentPageNo, pageSize, whereStr, orderbyStr, orderByDirection,
                                                     hqlParameterDic);
        }

        public DataTable SelectObjectToPaginationStoredProc(string tableName, string primaryKeyName, string colName, string orderCol, int pageSize, int currentPageNo, bool orderType, string condition)
        {
            throw new NotImplementedException();
        }

        public IList ExecuteStoredProc(string spName, ICollection paramInfos)
        {
            throw new NotImplementedException();
        }

    }
}
