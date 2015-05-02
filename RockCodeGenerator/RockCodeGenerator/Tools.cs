using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RockCodeGenerator
{
    public class Tools
    {

        /// <summary>
        /// 读取模板
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static StringBuilder GetTemplate(string path)
        {
            var str = new StringBuilder();
            try
            {
                using (var strReader = new StreamReader(path, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string line = "";
                    while ((line = strReader.ReadLine()) != null)
                    {
                        str.Append(line);
                        str.Append("\n");
                    }
                    strReader.Close();
                }
                return str;
            }
            catch (Exception ex)
            {

                throw new Exception("读取(" + path + ")模板错误");
            }

        }


        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="strtext">文本内容</param>
        /// <param name="path">写入的路径</param>
        public static void WriterTemplateFile(StringBuilder strtext, string path)
        {
            try
            {
                using (var strWriter = new StreamWriter(path, false, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    strWriter.WriteLine(strtext);
                    strWriter.Flush();
                    strWriter.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("写入(" + path + ")出现异常" + ex.Message);
            }
        }

        /// <summary>
        /// 用于生成文件覆盖项目中的文件
        /// </summary>
        /// <param name="projectFilePathKey">项目文件中的路径</param>
        /// <param name="generatorFilePath"> 生成文件的路径</param>
        public static void  ConverProjectFile(string projectFilePathKey,string generatorFilePath)
        {
            var isOpenCoverProject = ConfigurationManager.AppSettings["IsOpenCoverProject"].ToLower();
            //如果启用了开关
            if(isOpenCoverProject.Equals("true"))
            {
                var projectFilePath = ConfigurationManager.AppSettings["ProjectAddress"] + ConfigurationManager.AppSettings[projectFilePathKey];

                //同时覆盖原来的文件
                File.Copy(generatorFilePath,projectFilePath,true);
            }
        }

        /// <summary>
        /// 用于生成文件覆盖项目中的文件
        /// </summary>
        /// <param name="projectFilePathKey">项目文件中的路径</param>
        /// <param name="generatorFilePath"> 生成文件的路径</param>
        /// <param name="configSpaceName">二级命名空间 </param>
        public static void ConverProjectFile(string projectFilePathKey, string generatorFilePath,string configSpaceName)
        {
            var isOpenCoverProject = ConfigurationManager.AppSettings["IsOpenCoverProject"].ToLower();
            //如果启用了开关
            if (isOpenCoverProject.Equals("true"))
            {
                var projectFilePath = ConfigurationManager.AppSettings["ProjectAddress"] + string.Format(ConfigurationManager.AppSettings[projectFilePathKey],configSpaceName);

                //同时覆盖原来的文件
                File.Copy(generatorFilePath, projectFilePath, true);
            }
        }

        /// <summary>
        /// 检查文件夹是否存在，不存在则创建
        /// </summary>
        /// <param name="path"></param>
        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        public static string Convert(string src)
        {
            return Convert(Encoding.GetEncoding("gb2312"), Encoding.UTF8, src);
        }

        public static string Convert(Encoding srcencode, Encoding desencode, string src)
        {
            byte[] bytes = srcencode.GetBytes(src);
            byte[] buffer2 = Encoding.Convert(srcencode, desencode, bytes);
            return Encoding.UTF8.GetString(buffer2);
        }

        public static SqlDbType ConvertDbType(string dbtype)
        {
            switch (dbtype)
            {
                case "bigint":
                    return SqlDbType.BigInt;

                case "binary":
                    return SqlDbType.Binary;

                case "bit":
                    return SqlDbType.Bit;

                case "char":
                    return SqlDbType.Char;

                case "datetime":
                    return SqlDbType.DateTime;

                case "date":
                    return SqlDbType.Date;

                case "decimal":
                    return SqlDbType.Decimal;

                case "float":
                    return SqlDbType.Float;

                case "image":
                    return SqlDbType.Image;

                case "int":
                    return SqlDbType.Int;

                case "money":
                    return SqlDbType.Money;

                case "nchar":
                    return SqlDbType.NChar;

                case "ntext":
                    return SqlDbType.NText;

                case "nvarchar":
                    return SqlDbType.NVarChar;

                case "real":
                    return SqlDbType.Real;

                case "smalldatetime":
                    return SqlDbType.SmallDateTime;

                case "smallint":
                    return SqlDbType.SmallInt;

                case "smallmoney":
                    return SqlDbType.SmallMoney;

                case "numeric":
                    return SqlDbType.Real;

                case "text":
                    return SqlDbType.Text;

                case "timestamp":
                    return SqlDbType.Timestamp;

                case "tinyint":
                    return SqlDbType.TinyInt;

                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;

                case "varbinary":
                    return SqlDbType.VarBinary;

                case "varchar":
                    return SqlDbType.VarChar;

                case "variant":
                    return SqlDbType.Variant;
                default:
                    return SqlDbType.VarChar;
            }
            //throw new Exception("类型转换出错！");
        }

        /// <summary>
        /// 根据db类型返回c#类型
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static Type ConvertVSType(string dbtype)
        {
            switch (dbtype)
            {
                case "bigint":
                    return typeof(long);

                case "binary":
                    return typeof(byte[]);

                case "bit":
                    return typeof(bool);

                case "char":
                    return typeof(string);

                case "datetime":
                    return typeof(DateTime);

                case "date":
                    return typeof(DateTime);

                case "decimal":
                    return typeof(decimal);

                case "float":
                    return typeof(double);

                case "image":
                    return typeof(byte[]);

                case "int":
                    return typeof(int);

                case "money":
                    return typeof(decimal);

                case "nchar":
                    return typeof(string);

                case "ntext":
                    return typeof(string);

                case "nvarchar":
                    return typeof(string);

                case "numeric":
                    return typeof(float);

                case "real":
                    return typeof(float);

                case "smalldatetime":
                    return typeof(DateTime);

                case "smallint":
                    return typeof(short);

                case "smallmoney":
                    return typeof(decimal);

                case "text":
                    return typeof(string);

                case "timestamp":
                    return typeof(byte);

                case "tinyint":
                    return typeof(byte);

                case "uniqueidentifier":
                    return typeof(Guid);

                case "varbinary":
                    return typeof(byte[]);

                case "varchar":
                    return typeof(string);

                case "xml":
                    return typeof(string);

                case "variant":
                    return typeof(object);
                default:
                    return typeof(object); ;
            }
            //throw new Exception("类型转换出错！");
        }

        public static object GetDefaultValue(string dbtype)
        {
            switch (dbtype)
            {
                case "bigint":
                    return 0;

                case "binary":
                    return null;

                case "bit":
                    return false;

                case "char":
                    return null;

                case "datetime":
                    return DateTime.Parse("1900-1-1");

                case "decimal":
                    return 0M;

                case "float":
                    return 0f;

                case "image":
                    return null;

                case "int":
                    return 0;

                case "money":
                    return 0;

                case "nchar":
                    return null;

                case "ntext":
                    return null;

                case "varchar":
                case "nvarchar":
                    return "";

                case "numeric":
                    return 0;

                case "real":
                    return 0;

                case "smalldatetime":
                    return DateTime.Parse("1900-1-1");

                case "date":
                    return DateTime.Parse("1900-1-1");

                case "smallint":
                    return 0;

                case "smallmoney":
                    return 0;

                case "text":
                    return null;

                case "timestamp":
                    return null;

                case "tinyint":
                    return 0;

                case "uniqueidentifier":
                    return Guid.Empty;

                case "varbinary":
                    return null;

                case "variant":
                    return null;
                default:
                    return SqlDbType.VarChar;
            }
            // throw new Exception("类型转换出错！");
        }
        public static object GetTypeDefaultValue(string typename)
        {
            switch (typename)
            {
                case "System.Guid":
                    return Guid.NewGuid();
                case "System.Int64":
                case "System.Int32":
                case "System.Int16":
                case "System.Decimal":
                case "System.Double":
                    return 0;
                case "System.Boolean":
                    return false;
                case "System.String":
                    return "";
                case "System.DateTime":
                    return DateTime.Now;
                default:
                    return null;
            }
        }
        public static object GetDefaultValue(Type type, bool newguid = true)
        {
            if (type == typeof(string))
            {
                return "";
            }
            if (type == typeof(Guid))
            {
                if (newguid)
                    return Guid.NewGuid().ToString();
                else
                    return Guid.Empty;
            }
            if (type == typeof(DateTime))
            {
                return DateTime.Now;
            }
            if (type == typeof(long) || type == typeof(int))
            {
                return 0;
            }
            return null;
        }

        public static string GetDefaultValue(string dbtype, string def)
        {
            string str = "";
            str = def.Replace("(", "").Replace(")", "").Replace("'", "");
            switch (dbtype)
            {
                case "bigint":
                    if (str == "")
                    {
                        return "0";
                    }
                    return str;

                case "binary":
                    return "null";

                case "bit":
                    if (!(str == "1"))
                    {
                        return "false";
                    }
                    return "true";

                case "char":
                    return ("\"" + str + "\"");

                case "datetime":
                    if (!(str.ToLower() == "getdate"))
                    {
                        return "DateTime.Parse(\"1900-1-1\")";
                    }
                    return "DateTime.Now";

                case "decimal":
                    if (str == "")
                    {
                        return "0";
                    }
                    return str;

                case "float":
                    if (str == "")
                    {
                        return "0";
                    }
                    return str;

                case "image":
                    return "null";

                case "int":
                    if (!(str == ""))
                    {
                        return str;
                    }
                    return "0";

                case "money":
                    if (str == "")
                    {
                        return "0";
                    }
                    return str;

                case "nchar":
                    return ("\"" + str + "\"");

                case "ntext":
                    return ("\"" + str + "\"");

                case "nvarchar":
                    return ("\"" + str + "\"");

                case "numeric":
                    if (!(str == ""))
                    {
                        return str;
                    }
                    return "0";

                case "real":
                    if (str == "")
                    {
                        return "0";
                    }
                    return str;

                case "smalldatetime":
                    if (str.ToLower() == "getdate")
                    {
                        return "DateTime.Now";
                    }
                    return "DateTime.Parse(\"1900-1-1\")";

                case "smallint":
                    if (str == "")
                    {
                        return "0";
                    }
                    return str;

                case "smallmoney":
                    if (str == "")
                    {
                        return "0";
                    }
                    return str;

                case "text":
                    return ("\"" + str + "\"");

                case "timestamp":
                    return "null";

                case "tinyint":
                    if (!(str == ""))
                    {
                        return str;
                    }
                    return "0";

                case "uniqueidentifier":
                    return "Guid.Empty";

                case "varbinary":
                    return "null";

                case "varchar":
                    return ("\"" + str + "\"");

                case "variant":
                    return "new object()";
                default:
                    return "new object()";
            }
            //throw new Exception("类型转换出错！");
        }

        public static string GetFieldName(string fieldName)
        {
            return (fieldName.Substring(0, 1).ToLower() + fieldName.Substring(1, fieldName.Length - 1));
        }

        public static string GetPropertyName(string fieldName)
        {
            return (fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1, fieldName.Length - 1));
        }
    }
}
