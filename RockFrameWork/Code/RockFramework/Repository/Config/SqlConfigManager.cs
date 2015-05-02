using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using RockFramework.Tool.FileHelper;

namespace RockFramework.Repository.Config
{
    /// <summary>
    /// SQL 配置文件辅助类
    /// </summary>
    public class SqlConfigManager
    {
        /// <summary>
        /// SQL配置字段
        /// </summary>
        private static Dictionary<string,string> _sqlConfigDictionary=null;
 
        static SqlConfigManager()
        {
            InitSqlConfig();
        }

        /// <summary>
        /// 读取所有SQL配置文件、并保存到SqlConfigDictionary字典中
        /// </summary>
        private static void InitSqlConfig()
        {
            _sqlConfigDictionary=new Dictionary<string, string>();

            //sql 配置文件根目录
            string filesPath = ConfigurationManager.AppSettings["BaseSqlConfigPath"].ToString(CultureInfo.InvariantCulture);

            filesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filesPath);

            //获取此下面所有的xml配置文件
            string[] xmlFiles = DirFileOperate.GetFileNames(filesPath, "*Sql.xml", true);


            //遍历所有文件
            foreach (var xmlFile in xmlFiles)
            {
                //遍历文件下面所有的SQL到字典中
                XElement xmlElement = XElement.Load(xmlFile);
                var sqlElements=xmlElement.Elements("sql");
                foreach (var sqlElement in sqlElements)
                {
                    //key
                    var idElement = sqlElement.Element("Id");

                    //text
                    var textElement = sqlElement.Element("Text");

                    if (idElement != null && textElement !=null)
                    {
                        var key = idElement.Value;
                        var value = textElement.Value;

                        _sqlConfigDictionary.Add(key,value);
                    }
                }
            }
        }

        /// <summary>
        /// 根据Key获取对应SQL语句
        /// </summary>
        /// <param name="key">SQL语句对应的Key名称</param>
        /// <returns></returns>
        public static string GetSQLStringByKey(string key)
        {
            string sql = string.Empty;

            if (string.IsNullOrEmpty(key))
                return string.Empty;

            if(_sqlConfigDictionary ==null)
            {
                InitSqlConfig();
            }

            if(_sqlConfigDictionary != null && !_sqlConfigDictionary.ContainsKey(key))
                throw  new ArgumentNullException("sql配置文件不包含此Key="+key);
            else
            {
                if (_sqlConfigDictionary != null) sql = _sqlConfigDictionary[key];
            }
            
            return sql;
        }

    }
}
