/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BaseHistory.cs
* 功能描述： 历史对象基类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-30 13:52:03
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-30 13:52:03		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using System.Linq;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 历史对象基类
    /// </summary>
    /// <typeparam name="T">历史项参数类型</typeparam>
    public class BaseHistory<T> : IHistory<T>
        where T : BaseHistoryItem
    {
        #region Property

        public HistoryType @HisType
        {
            get; set;
        }

        ///// <summary>
        ///// 历史静态对象列表
        ///// </summary>
        //private static IList<T> m_HistoryCacheInstance;
        ///// <summary>
        ///// 部署参数静态实例
        ///// </summary>
        //public static IList<T> HistoryCacheInstance
        //{
        //    get
        //    {
        //        if (m_HistoryCacheInstance == null)
        //        {
        //            m_DeployConfigCacheInstance = ReadConfig();
        //        }
        //        return m_DeployConfigCacheInstance;
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            throw new DeployBaseException("配置参数为NULL错误！");
        //        }
        //        m_DeployConfigCacheInstance = value;
        //        m_DeployConfigCacheInstance.SaveConfig();
        //    }
        //}

        /// <summary>
        /// 历史项明细列表
        /// </summary>
        public IList<T> Details
        {
            get;
            set;
        }


        private DataSet m_DetailsData;
        /// <summary>
        /// 历史明细DataSet
        /// </summary>
        public DataSet DetailsData
        {
            get
            {
                //if (m_DetailsData == null)
                {
                    m_DetailsData = ToDataSet();
                }
                return m_DetailsData;
            }
            private set
            {
                m_DetailsData = value;
            }
        }

        /// <summary>
        /// 历史文件存放目录
        /// e.g：C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\IIS_DeployHistory
        /// </summary>
        public string HistoryDir
        {
            get;
            set;
        }
        /// <summary>
        /// 历史履历文档名称
        /// e.g：IISDeployHistory.DHistory
        /// </summary>
        public string HistoryListFileName
        {
            get;
            set;
        }

        #endregion

        #region Ctor
        public BaseHistory(HistoryType hisType)
        {
            HisType = hisType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 增加历史项
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual Tuple<bool, string> AddHistory(T t)
        {
            if (string.IsNullOrEmpty(t.HistoryPackageFileFullName))
            {
                return Tuple.Create<bool, string>(false, "未指定历史包文件地址！");
            }
            if (string.IsNullOrEmpty(HistoryDir))
            {
                throw new Exception("未指定历史包文件路径地址！");
            }

            if (Details == null)
            {
                Details = new List<T>();
            }

            Details.Add(t);

            var cPackageInfoString = JsonConvert.SerializeObject(Details, Formatting.Indented);
            //cPackageInfoString = DESEncrypt.Encrypt(cPackageInfoString);
            if (string.IsNullOrEmpty(HistoryListFileName))
            {
                HistoryListFileName = ConstantValues.DEFAULT_HISTORY_LIST_FILE_NAME;
            }
            //向源目录写入发版文件
            File.WriteAllText(Path.Combine(HistoryDir, HistoryListFileName), cPackageInfoString, Encoding.UTF8);
            return Tuple.Create<bool, string>(true, "");
        }
        /// <summary>
        /// 获取历史记录列表
        /// </summary>
        /// <returns></returns>
        public IList<T> GetHistoryList()
        {
            if (string.IsNullOrEmpty(HistoryDir))
            {
                throw new Exception("未指定历史包文件路径地址！");
            }
            if (string.IsNullOrEmpty(HistoryListFileName))
            {
                HistoryListFileName = ConstantValues.DEFAULT_HISTORY_LIST_FILE_NAME;
            }
            var cHistoryListFileFullName = System.IO.Path.Combine(HistoryDir, HistoryListFileName);
            if (!System.IO.File.Exists(cHistoryListFileFullName))
            {
                return null;
            }
            string cHistoryListInfoString = File.ReadAllText(cHistoryListFileFullName, Encoding.UTF8);
            //cHistoryListInfoString = DESEncrypt.Decrypt(cHistoryListInfoString);
            var cPackageInfoObj = JsonConvert.DeserializeObject<IList<T>>(cHistoryListInfoString);
            if (@HisType == HistoryType.PACK_IIS || @HisType == HistoryType.PACK_SQL)
            {
                return cPackageInfoObj;
            }
            else
            {
                return cPackageInfoObj.Where(A => { return A.TYPE == HisType; })?.ToList();
            }
        }
        /// <summary>
        /// 返回历史项目数据集
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ToDataSet()
        {
            DataSet result = new DataSet();
            if (Details == null || Details.Count == 0)
            {
                return null;
            }
            DataTable _DataTable = new DataTable();

            PropertyInfo[] propertys = Details[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                _DataTable.Columns.Add(pi.Name, pi.PropertyType);//columnName Type
            }

            for (int i = 0; i < Details.Count; i++)
            {
                ArrayList tempList = new ArrayList();    //一行数据的数组
                foreach (PropertyInfo pi in propertys)
                {
                    object obj = pi.GetValue(Details[i], null);//p_List[i]是集合中的某一个对象，GetValue 返回其属性值
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                _DataTable.LoadDataRow(array, true); //LoadDataRow 查找和更新特定行。 如果找不到任何匹配行，则使用给定值创建新行
            }

            result.Tables.Add(_DataTable);
            return result;
        }
        /// <summary>
        /// 构建历史数据集结构
        /// </summary>
        /// <returns></returns>
        protected virtual DataSet BuildHistoryDataConstruction()
        {
            return null;
        }

        public void Dispose()
        {
            DetailsData?.Dispose();
            Details = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return;
        }
        #endregion
    }
}
