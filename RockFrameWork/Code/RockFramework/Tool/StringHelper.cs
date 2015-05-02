using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RockFramework.Tool
{
    public  class StringHelper
    {
        /// <summary>
        /// 用时间组成文件名称
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFileName()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond;
        }

        /// <summary>
        /// 生产由时间组成的编号
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeNo()
        {
            var orderNo = DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour +
                          "" + DateTime.Now.Minute + "" + DateTime.Now.Second;

            return orderNo;
        }

        /// <summary>
        /// 检查文件是否是图片
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>是图片文件则返回true、否则返回false</returns>
        public static bool CheckFileTypeIsPicture(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".bmp":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 检查文件是否是图片
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>是图片文件则返回true、否则返回false</returns>
        public static bool CheckFileTypeIsExecl(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".xls":
                    return true;
                case ".xlsx":
                    return true;
                default:
                    return false;
            }
        }
    }
}
