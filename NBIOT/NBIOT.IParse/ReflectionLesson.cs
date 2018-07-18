using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace NBIOT.IParse
{
    /// <summary>
    /// 反射类
    /// 利用反射动态调用DLL类库。
    /// </summary>
    public class ReflectionLesson
    {
        private string m_strDllName = "";
        private string m_strClaName = "";
        private string m_strPath = "";

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="DllName">调用的DLL类库名</param>
        /// <param name="ClaName">调用的类名</param>
        /// <param name="MetName">调用的方法名(数组)</param>
        public ReflectionLesson(string strPath, string DllName, string ClaName)
        {
            //获取调用的DLL类库
            this.m_strPath = strPath;
            this.m_strClaName = ClaName;
            this.m_strDllName = DllName;
        }
        /// <summary>
        /// 利用反射动态调用DLL类库
        /// </summary>
        public IParseClass ReflectionClass()
        {
            if (File.Exists(m_strPath))
            {
                Assembly ass = Assembly.LoadFrom(m_strPath);
                Type t = ass.GetType(m_strDllName + "." + this.m_strClaName);
                IParseClass obj = (IParseClass)Activator.CreateInstance(t);

                return obj;
            }
            else
            {
                return null;
            }
        }

        public Type ReflectionType()
        {
            if (File.Exists(m_strPath))
            {
                Assembly ass = Assembly.LoadFrom(m_strPath);
                Type t = ass.GetType(m_strDllName + "." + this.m_strClaName);

                return t;
            }
            else
            {
                return null;
            }
        }

        public IParseClass ReflectionClass(Type type)
        {
            if (type != null)
            {
                IParseClass obj = (IParseClass)Activator.CreateInstance(type);

                return obj;
            }
            else
            {
                return null;
            }
        }
    }
}
