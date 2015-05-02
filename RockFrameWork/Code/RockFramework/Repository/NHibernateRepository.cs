using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using RockFramework.DomainModel;
using Spring.Data.Core;
using Spring.Data.NHibernate.Generic.Support;//采用NHibernateTemplate模式
using Common.Logging;
using RockFramework.Repository.Data;

namespace RockFramework.Repository
{
 
    public abstract class NHibernateRepository<T> : HibernateDaoSupport,IRepository<T>
    {
        private new readonly ILog _log = LogManager.GetLogger(typeof(T));

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回新增后的实体主键</returns>
        public object Save(T entity)
        {
            try
            {
                //记录日志
                //log.Info("Save Success:-->"+DateTime.Now.ToString(CultureInfo.InvariantCulture));
                entity.GetType().GetProperty("Id").SetValue(entity,Guid.NewGuid().ToString(),null);
                entity.GetType().GetProperty("CreateTime").SetValue(entity, DateTime.Now, null);
                entity.GetType().GetProperty("UpdateTime").SetValue(entity, DateTime.Now, null);
                entity.GetType().GetProperty("IsDelete").SetValue(entity, false, null);
                var currentUser = HttpContext.Current.Session[SystemConstant.CurrentUserInfo] as Entity;
                if (currentUser != null)
                {
                    entity.GetType().GetProperty("CreateUser").SetValue(entity, currentUser.Id, null);
                    entity.GetType().GetProperty("UpdateUser").SetValue(entity, currentUser.Id, null);
                }else
                {
                    entity.GetType().GetProperty("CreateUser").SetValue(entity, SystemConstant.DefaultGuid, null);
                    entity.GetType().GetProperty("UpdateUser").SetValue(entity, SystemConstant.DefaultGuid, null);
                }
                return this.HibernateTemplate.Save(entity);  
            }
            catch (Exception ex)
            {
                _log.Info(entity.GetType().Name+"Save Fail:-->" + DateTime.Now.ToString(CultureInfo.InvariantCulture)+"...............ErrorMsg:"+ex.Message); 
                throw ex;
            }
            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            try
            {
                this.HibernateTemplate.Delete(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据where条件进行删除
        /// </summary>
        /// <param name="whereStr">删除Where字符串</param>
        /// <returns>返回删除结果状态，true=删除成功、false=删除失败</returns>
        public bool Delete(string whereStr)
        {
            try
            {
                string hql = string.Format("from {0} {1}",typeof(T).ToString(),whereStr.ToUpper().StartsWith("WHERE") ? whereStr : "WHERE"+whereStr);
                this.HibernateTemplate.Delete(hql);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回修改成功状态，true=修改成功、false=修改失败</returns>
        public bool Update(T entity)
        {
            try
            {

                var currentUser = HttpContext.Current.Session[SystemConstant.CurrentUserInfo] as Entity;
                if (currentUser != null)
                {
                    entity.GetType().GetProperty("UpdateUser").SetValue(entity, currentUser.Id, null);//赋值当前修改用户Id
                }
                entity.GetType().GetProperty("UpdateTime").SetValue(entity,DateTime.Now,null);//赋值修改时间

                this.HibernateTemplate.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据主键获取实体对象
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>实体对象</returns>
        public T Get(object id)
        {
            try
            {
                return this.HibernateTemplate.Get<T>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }

        /// <summary>
        /// 根据主键获取实体对象【并进行缓存、赖加载】
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>实体对象</returns>
        public T Load(object id)
        {
            try
            {
                return this.HibernateTemplate.Load<T>(id);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <returns>实体对象集合</returns>
        public IList<T> SelectAll()
        {
            return this.HibernateTemplate.LoadAll<T>();
        }

        /// <summary>
        /// 根据Hql where条件进行查询
        /// </summary>
        /// <param name="whereStr">hql查询条件</param>
        /// <returns>查询结果集合</returns>
        public IList<T> Select(string whereStr)
        {
            
            try
            {
                string hql = string.Format("from {0} {1} order by CreateTime desc", typeof(T).ToString(), whereStr.ToUpper().StartsWith("WHERE") ? whereStr : "WHERE" + whereStr);
                IList<T> list= this.HibernateTemplate.Find<T>(hql);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 条件查询 -- 去掉重复项
        /// </summary>
        /// <param name="whereStr">查询条件字符串（Hql）</param>
        /// <param name="field">查询字段列表字符串、已“，”割开</param>
        /// <param name="alias">查询对象的别名</param>
        /// <returns>查询结果集合</returns>
        public IList<T> SelectDistinct(string whereStr, string field, string alias)
        {
            try
            {
                //拆分成别名+列名
                string[] cols = field.Split(',');

                #region 被lambda表达式替换
                //string columns = string.Empty;
                //foreach (string col in cols)
                //    columns += string.Format("{0}.{1},", alias, col);
                #endregion
                string columns = cols.Aggregate(string.Empty, (current, col) => current + string.Format("{0}.{1},", alias, col));
                columns = columns.TrimEnd(',');

                //hql
                string hql = string.Format("select distinct {2} from {0} {1} {3}",
                    typeof(T).ToString(),
                    alias,
                    columns,
                    whereStr.ToUpper().StartsWith("WHERE") ? whereStr : "WHERE " + whereStr);

                IList<T> list = this.HibernateTemplate.Find<T>(hql);
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 条件查询 -- 排序
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="propertyName">排序的列明</param>
        /// <param name="ascending">排序方向</param>
        /// <returns>返回结果集</returns>
        public IList<T> SelectWithOrder(string whereStr, string propertyName, bool ascending)
        {

            try
            {
                //排序
                var order = new Order(propertyName, ascending);

                ICriteria ic = Session.CreateCriteria(typeof (T));
                ic.AddOrder(order);

                //过滤
                ICriterion exp = Expression.Sql(whereStr);

                return ic.List<T>();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 统计总条数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            try
            {
                //T obj = (T)System.Reflection.Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());
                //string hql = string.Format("select count(*) from {0}", obj.GetType().ToString());
                string hql = string.Format("select count(*) from {0}", typeof(T).ToString());

                //Session用法1
                IQuery query = Session.CreateQuery(hql);

                object o = query.UniqueResult();
                //object o = this.HibernateTemplate.Execute<T>(session => session.CreateQuery(hql).UniqueResult<T>());//返回了对象

                return int.Parse(o.ToString());
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
    
        }

        /// <summary>
        /// 根据条件统计总条数
        /// </summary>
        /// <param name="whereStr">过滤条件</param>
        /// <returns>返回查询总条数</returns>
        public int Count(string whereStr)
        {
            try
            {
                //利用拼hql语句的方式、再利用sql注入语句、报错误：”引发类型为“Antlr.Runtime.NoViableAltException”的异常。“，会导致hql语句不正确，初步判断是NHibernate内部机制已经判断sql的安全性
                string hql = string.Format("select count(*) from {0} {1}", typeof(T).ToString(),
                                     whereStr.ToUpper().StartsWith("WHERE") ? whereStr : (string.IsNullOrWhiteSpace(whereStr) ? string.Empty : "WHERE " + whereStr));

                //object o = this.HibernateTemplate.Execute<T>(session => session.CreateQuery(hql).UniqueResult<T>());//返回了对象
                //object o = this.HibernateTemplate.ExecuteFind<T>(session => session.CreateQuery(hql).u);

                //Session用法二、与用法一有区别吗？？？是同一对象、用法一线程安全的吗？？？
                IQuery query = Session.CreateQuery(hql); //this.HibernateTemplate.SessionFactory.GetCurrentSession().CreateQuery(hql);

                object o = query.UniqueResult();

                return int.Parse(o.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        /// <summary>
        /// 根据条件统计总条数【参数化查询】
        /// </summary>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="hqlParameterDic">过滤条件的参数集合</param>
        /// <returns>返回查询总条数</returns>
        public int Count(string whereStr, Dictionary<string, HqlParameter> hqlParameterDic)
        {
            try
            {
                
                string hql = string.Format("select count(*) from {0} {1}", typeof(T).ToString(),
                                     whereStr.ToUpper().StartsWith("WHERE") ? whereStr : (string.IsNullOrWhiteSpace(whereStr) ? string.Empty : "WHERE " + whereStr));
               
                IQuery query = Session.CreateQuery(hql);

                FormatHQLParams(query,hqlParameterDic);

                object o = query.UniqueResult();

                return int.Parse(o.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询指定页数据集合
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSize">每页多少条</param>
        /// <returns>指定页数据集合</returns>
        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSize)
        {
            try
            {
                //有意思的模板反射哟~
                T obj = (T)System.Reflection.Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());

                string hql = string.Format("from {0}", obj.GetType().ToString());

                #region 写法一
                //IQuery query = Session.CreateQuery(hql);

                //IList<T> list = query.SetFirstResult((currentPageNo-1)*pageSize).SetMaxResults(pageSize).List<T>();

                #endregion

                //写法二
                IList<T> list = this.HibernateTemplate.ExecuteFind<T>(session => session.CreateQuery(hql).SetFirstResult((currentPageNo - 1) * pageSize).SetMaxResults(pageSize).List<T>());

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 根据条件查询指定页数据集合
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSiz">每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <returns>过滤后指定页数集合</returns>
        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSiz, string whereStr)
        {
            try
            {
                string hql = string.Format("from {0} {1} order by CreateTime desc", typeof(T).ToString(), whereStr.ToUpper().StartsWith("WHERE") ? whereStr : (string.IsNullOrWhiteSpace(whereStr) ? string.Empty : "WHERE " + whereStr));

                IList<T> list =
                    this.HibernateTemplate.ExecuteFind<T>(
                        session =>
                        session.CreateQuery(hql).SetFirstResult((currentPageNo - 1)*pageSiz).SetMaxResults(pageSiz).List
                            <T>());
                return list;
            }
            catch (Exception ex)
            {      
                throw ex;
            }
        }

        /// <summary>
        /// 根据条件查询指定页数据集合【参数化查询】
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSiz">每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="hqlParameterDic">过滤条件的参数字典集合</param>
        /// <returns>过滤后指定页数集合</returns>
        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSiz, string whereStr, Dictionary<string, HqlParameter> hqlParameterDic)
        {
            try
            {
                string hql = string.Format("from {0} {1} order by CreateTime desc", typeof(T).ToString(), whereStr.ToUpper().StartsWith("WHERE") ? whereStr : (string.IsNullOrWhiteSpace(whereStr) ? string.Empty : "WHERE " + whereStr));

                //IList<T> list =
                //    this.HibernateTemplate.ExecuteFind<T>(
                //        session =>
                //        session.CreateQuery(hql).SetFirstResult((currentPageNo - 1) * pageSiz).SetMaxResults(pageSiz).List
                //            <T>());

                IList<T> list = this.HibernateTemplate.ExecuteFind<T>(delegate(ISession session)
                                                                          {
                                                                              IQuery query = session.CreateQuery(hql);
                                                                              //赋值
                                                                              FormatHQLParams(query, hqlParameterDic);
                                                                              return query.SetFirstResult((currentPageNo - 1)*
                                                                                                   pageSiz).
                                                                                  SetMaxResults(
                                                                                      pageSiz).List<T>();
                                                                          });
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据条件查询指定页数据集合
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSize"> 每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="orderByDirection">排序方向</param>
        /// <returns>过滤后指定页数集合</returns>
        public virtual IList<T> SelectObjectToPagination(int currentPageNo, int pageSize, string whereStr, string orderbyStr, Direction orderByDirection)
        {
            try
            {
                string hql = string.Format("from {0} {1} order by {2}", typeof (T).ToString(),
                                           whereStr.ToUpper().StartsWith("WHERE") ? whereStr : "WHERE " + whereStr,orderbyStr+" "+orderByDirection);
                IList<T> list =
                    this.HibernateTemplate.ExecuteFind<T>(
                        session =>
                        session.CreateQuery(hql).SetFirstResult((currentPageNo - 1)*pageSize).SetMaxResults(pageSize).
                            List<T>());
                return list;
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        /// <summary>
        /// 根据条件查询指定页数据集合
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSize"> 每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="orderByDirection">排序方向</param>
        /// <param name="hqlParameterDic">过滤条件的参数字典集合</param>
        /// <returns>过滤后指定页数集合</returns>
        public IList<T> SelectObjectToPagination(int currentPageNo, int pageSize, string whereStr, string orderbyStr, Direction orderByDirection, Dictionary<string, HqlParameter> hqlParameterDic)
        {
            try
            {
                string hql = string.Format("from {0} {1} order by {2}", typeof(T).ToString(),
                                           whereStr.ToUpper().StartsWith("WHERE") ? whereStr : "WHERE " + whereStr, orderbyStr + " " + orderByDirection);
                //IList<T> list =
                //    this.HibernateTemplate.ExecuteFind<T>(
                //        session =>
                //        session.CreateQuery(hql).SetFirstResult((currentPageNo - 1) * pageSize).SetMaxResults(pageSize).
                //            List<T>());

                IList<T> list =
                    this.HibernateTemplate.ExecuteFind<T>(delegate(ISession session)
                                                              {
                                                                  IQuery query = session.CreateQuery(hql);
                                                                  //为query参数赋值
                                                                  FormatHQLParams(query,hqlParameterDic);
                                                                  return
                                                                      query.SetFirstResult((currentPageNo - 1)*pageSize)
                                                                          .SetMaxResults(pageSize).List<T>();
                                                              });
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public DataTable SelectObjectToPaginationStoredProc(string tableName, string primaryKeyName, string colName, string orderCol, int pageSize, int currentPageNo, bool orderType, string condition)
        {
            throw new NotImplementedException();
        }

        public IList ExecuteStoredProc(string spName, ICollection paramInfos)
        {
            throw new NotImplementedException();
        }

        #region 辅助方法
        /// <summary>
        /// 为IQuery对象设置差数
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dics"></param>
        private void FormatHQLParams(IQuery query, Dictionary<string, HqlParameter> dics)
        {
            foreach (KeyValuePair<string, HqlParameter> dic in dics)
            {
                //参数名称
                string parameterName = dic.Key;

                HqlParameter parameterValue = dic.Value;

                if (parameterValue != null)
                    switch (parameterValue.DataType)
                    {
                        case DataType.AnsiString:
                            break;
                        case DataType.Binary:
                            query.SetBinary(parameterName, (byte[])parameterValue.Value);
                            break;
                        case DataType.Boolean:
                            query.SetBoolean(parameterName, Convert.ToBoolean(parameterValue.Value));
                            break;
                        case DataType.Byte:
                            query.SetByte(parameterName, Convert.ToByte(parameterValue.Value));
                            break;
                        case DataType.Character:
                            query.SetCharacter(parameterName, Convert.ToChar(parameterValue.Value));
                            break;
                        case DataType.DateTime:
                            query.SetDateTime(parameterName, Convert.ToDateTime(parameterValue.Value));
                            break;
                        case DataType.DateTime2:
                            break;
                        case DataType.TimeSpan:
                            break;
                        case DataType.TimeAsTimeSpan:
                            break;
                        case DataType.DateTimeOffset:
                            break;
                        case DataType.Decimal:
                            query.SetDecimal(parameterName, Convert.ToDecimal(parameterValue.Value));
                            break;
                        case DataType.Double:
                            query.SetDouble(parameterName, Convert.ToDouble(parameterValue.Value));
                            break;
                        case DataType.Enum:
                            break;
                        case DataType.Int16:
                            query.SetInt16(parameterName, Convert.ToInt16(parameterValue.Value));
                            break;
                        case DataType.Int32:
                            query.SetInt32(parameterName, Convert.ToInt32(parameterValue.Value));
                            break;
                        case DataType.Int64:
                            query.SetInt64(parameterName, Convert.ToInt64(parameterValue.Value));
                            break;
                        case DataType.Single:
                            query.SetSingle(parameterName, Convert.ToSingle(parameterValue.Value));
                            break;
                        case DataType.String:
                            query.SetString(parameterName, GetExpressionValue(parameterValue));
                            break;
                        case DataType.Time:
                            query.SetDateTime(parameterName, Convert.ToDateTime(parameterValue.Value));
                            break;
                        case DataType.Timestamp:
                            break;
                        case DataType.Guid:
                            query.SetGuid(parameterName, Guid.Parse(parameterValue.Value.ToString()));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
            }
        }

        /// <summary>
        /// 格式化操作类型、并返回值
        /// </summary>
        /// <param name="hqlParameter"></param>
        /// <returns></returns>
        private string GetExpressionValue(HqlParameter hqlParameter)
        {
            string result = string.Empty;
            if (hqlParameter != null)
                switch (hqlParameter.ExpressionType)
                {
                    case ExpressionType.Default:
                        result = hqlParameter.Value.ToString();
                        break;
                    case ExpressionType.Like:
                        result += "%" + hqlParameter.Value.ToString() +"%";
                        break;
                    case ExpressionType.LinkStart:
                        result += "%" + hqlParameter.Value.ToString();
                        break;
                    case ExpressionType.LinkEnd:
                        result += hqlParameter.Value.ToString() + "%";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return result;
        }

        #endregion
    }
}
