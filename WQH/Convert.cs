using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WQH
{
    public class Convert
    {
        /// <summary>
        /// table指定行转对象
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dt">传入的表格</param>
        /// <param name="rowindex">table行索引，默认为第一行</param>
        /// <param name="isStoreDB">是否DateTime数据类型有效范围判断</param>
        /// <returns>返回实体对象</returns>
        public static T TableToEntity<T>(DataTable dt, int rowindex = 0, bool isStoreDB = true)
        {
            Type type = typeof(T);
            T entity = Activator.CreateInstance<T>(); //创建对象实例
            if (dt == null)
            {
                return entity;
            }
            DataRow row = dt.Rows[rowindex]; //要查询的行索引
            PropertyInfo[] pArray = type.GetProperties();
            foreach (PropertyInfo p in pArray)
            {
                if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                {
                    continue;
                }

                if (isStoreDB && p.PropertyType == typeof(DateTime) && System.Convert.ToDateTime(row[p.Name]) < System.Convert.ToDateTime("1753-01-02"))
                {
                    continue;
                }
                try
                {
                    var obj = System.Convert.ChangeType(row[p.Name], p.PropertyType);  //类型强转，将table字段类型转为对象字段类型
                    p.SetValue(entity, obj, null);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return entity;
        }

        /// <summary>
        /// DataTable转化为List集合
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="dt">datatable表</param>
        /// <param name="isStoreDB">是否DateTime数据类型有效范围判断</param>
        /// <returns>返回list集合</returns>
        public static List<T> TableToList<T>(DataTable dt, bool isStoreDB = true)
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] pArray = type.GetProperties(); //集合属性数组
            foreach (DataRow row in dt.Rows)
            {
                T entity = Activator.CreateInstance<T>(); //新建对象实例 
                foreach (PropertyInfo p in pArray)
                {
                    if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                    {
                        continue;
                    }
                    if (isStoreDB && p.PropertyType == typeof(DateTime) && System.Convert.ToDateTime(row[p.Name]) < System.Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    try
                    {
                        var obj = System.Convert.ChangeType(row[p.Name], p.PropertyType);
                        p.SetValue(entity, obj, null);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                list.Add(entity);
            }
            return list;

        }

        /// <summary>
        /// List集合转DataTable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">传入集合</param>
        /// <param name="isStoreDB">是否DateTime数据类型有效范围判断</param>
        /// <returns>返回DataTable结果</returns>
        public static DataTable ListToTable<T>(List<T> list, bool isStoreDB = true)
        {
            Type tp = typeof(T);
            PropertyInfo[] proInfos = tp.GetProperties();
            DataTable dt = new DataTable();
            foreach (var item in proInfos)
            {
                dt.Columns.Add(item.Name, item.PropertyType); //添加列明及对应类型
            }
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                foreach (var proInfo in proInfos)
                {
                    object obj = proInfo.GetValue(item, null);  //获取实体对应属性
                    if (obj == null)
                    {
                        continue;
                    }
                    if (isStoreDB && proInfo.PropertyType == typeof(DateTime) && System.Convert.ToDateTime(obj) < System.Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    dr[proInfo.Name] = obj;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// JToken转对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="jt">传入JToken</param>
        /// <returns>返回实体结果</returns>
        public static T JsonToEntity<T>(JToken jt)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(jt));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
