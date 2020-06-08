/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   FilePackHistoryRepository.cs
* 功能描述： 文件打包历史实体类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-15 11:00:16
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-15 11:00:16		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Linq;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using System.Data;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    //public class FilePackHistoryRepository<T, U> : IDisposable
    //    where T : Impl.BasePackageZipParams<U>
    //    where U : BasePackageZipItem
    //{
    //    public HistoryType @HisType
    //    {
    //        get; set;
    //    }
    //    public void Dispose()
    //    {
    //        return;
    //    }

    //    public IList<T> Details
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 历史文件存放目录
    //    /// e.g：C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\IIS_DeployHistory
    //    /// </summary>
    //    public string HistoryDir
    //    {
    //        get;
    //        set;
    //    }
    //    /// <summary>
    //    /// 历史履历文档名称
    //    /// e.g：IISDeployHistory.DHistory
    //    /// </summary>
    //    public string HistoryListFileName
    //    {
    //        get;
    //        set;
    //    }
    //    public virtual Tuple<bool, string> AddHistory(T t)
    //    {
    //        //if (string.IsNullOrEmpty(t.HistoryPackageFileFullName))
    //        //{
    //        //    return Tuple.Create<bool, string>(false, "未指定历史包文件地址！");
    //        //}
    //        if (string.IsNullOrEmpty(HistoryDir))
    //        {
    //            throw new Exception("未指定历史包文件路径地址！");
    //        }
    //        if (!System.IO.Directory.Exists(HistoryDir))
    //        {
    //            System.IO.Directory.CreateDirectory(HistoryDir);
    //        }
    //        if (Details == null)
    //        {
    //            Details = new List<T>();
    //        }

    //        Details.Add(t);

    //        var cPackageInfoString = JsonConvert.SerializeObject(Details, Formatting.Indented);
    //        //cPackageInfoString = DESEncrypt.Encrypt(cPackageInfoString);
    //        if (string.IsNullOrEmpty(HistoryListFileName))
    //        {
    //            HistoryListFileName = ConstantValues.DEFAULT_HISTORY_LIST_FILE_NAME;
    //        }
    //        //向源目录写入发版文件
    //        File.WriteAllText(Path.Combine(HistoryDir, HistoryListFileName), cPackageInfoString, Encoding.UTF8);
    //        return Tuple.Create<bool, string>(true, "");
    //    }
    //}

    /// <summary>
    /// 文件打包历史实体类
    /// </summary>
    /// <typeparam name="T">历史项</typeparam>
    /// <typeparam name="U">历史项包对象</typeparam>
    public class FilePackHistoryRepository<T, U> : BaseHistory<T>, IDisposable
        where T : FilePackHistoryItem<U>
     where U : BasePackageZipItem
    {
        #region Ctor

        public FilePackHistoryRepository(HistoryType hisType, string historyDir = "", string historyListFileName = "")
            : base(hisType)
        {
            HisType = hisType;
            HistoryDir = historyDir;
            HistoryListFileName = historyListFileName;
            Details = base.GetHistoryList();
        }

        #endregion

        #region Methods

        protected override DataSet BuildHistoryDataConstruction()
        {
            DataTable dtDeployMain = new DataTable("PackMain");
            dtDeployMain.Columns.Add("GUID", typeof(string));
            dtDeployMain.Columns.Add("打包类型", typeof(string));
            dtDeployMain.Columns.Add("发布类型", typeof(string));
            dtDeployMain.Columns.Add("打包时间", typeof(string));
            dtDeployMain.Columns.Add("打包版本", typeof(string));
            dtDeployMain.Columns.Add("版本描述", typeof(string));
            dtDeployMain.Columns.Add("打包人", typeof(string));
            dtDeployMain.Columns.Add("备份目录", typeof(string));

            DataSet dsData = new DataSet("PackHistory");

            DataTable dtPackVersion = new DataTable("PackVersion");
            dtPackVersion.Columns.Add("GUID", typeof(string));
            dtPackVersion.Columns.Add("版本描述", typeof(string));
            dsData.Tables.Add(dtDeployMain);
            dsData.Tables.Add(dtPackVersion);

            return dsData;
        }

        public override DataSet ToDataSet()
        {
            if (Details == null || Details.Count == 0)
            {
                return null;
            }

            DataSet dsRtn = BuildHistoryDataConstruction();

            foreach (var item in Details)
            {
                string cGuid = item.GUID;

                #region 打包历史
                DataRow drMain = dsRtn.Tables["DeployMain"].NewRow();
                drMain["GUID"] = cGuid;
                drMain["打包类型"] = item.PackData?.PackType == DeployTypes.IIS ? "Web服务" : "SQL脚本";
                drMain["发布类型"] = item.PackData?.PkgType.ToString();
                drMain["打包时间"] = item.PackData?.PackageDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                drMain["打包版本"] = item.PackData?.Version?.ToString();
                drMain["版本描述"] = item.PackData?.Description?.ToString();
                drMain["打包人"] = item.PackData?.Packager?.ToString();
                drMain["备份目录"] = item.HistoryPackageFileFullName;
                dsRtn.Tables["DeployMain"].Rows.Add(drMain);
                #endregion

                #region 版本描述拆行显示

                var vVersionDescription= item.PackData?.Description?.ToString();
                var vDesLines = vVersionDescription.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var eachDescLine in vDesLines)
                {
                    DataRow drEachLine = dsRtn.Tables["PackVersion"].NewRow();
                    drEachLine["GUID"] = cGuid;
                    drEachLine["版本描述"] = eachDescLine;
                    dsRtn.Tables["PackVersion"].Rows.Add(drEachLine);
                }

                #endregion
            }

            return dsRtn;
        }

        public DataSet ToDataSet(DeployTypes dpType)
        {
            if (Details == null || Details.Count == 0)
            {
                return null;
            }
            var vFind = Details.Where((A) => { return A?.PackData?.PackType == dpType; });
            DataSet dsRtn = BuildHistoryDataConstruction();

            foreach (var item in vFind)
            {
                string cGuid = item.GUID;
                #region 打包历史
                DataRow drMain = dsRtn.Tables["PackMain"].NewRow();
                drMain["GUID"] = cGuid;
                drMain["打包类型"] = item.PackData?.PackType == DeployTypes.IIS ? "Web服务" : "SQL脚本";
                drMain["发布类型"] = item.PackData?.PkgType.ToString();
                drMain["打包时间"] = item.PackData?.PackageDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                drMain["打包版本"] = item.PackData?.Version?.ToString();
                drMain["版本描述"] = item.PackData?.Description?.ToString();
                drMain["打包人"] = item.PackData?.Packager?.ToString();
                drMain["备份目录"] = item.HistoryPackageFileFullName;
                dsRtn.Tables["PackMain"].Rows.Add(drMain);
                #endregion

                #region 版本描述拆行显示

                var vVersionDescription = item.PackData?.Description?.ToString();
                var vDesLines = vVersionDescription.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var eachDescLine in vDesLines)
                {
                    DataRow drEachLine = dsRtn.Tables["PackVersion"].NewRow();
                    drEachLine["GUID"] = cGuid;
                    drEachLine["版本描述"] = eachDescLine;
                    dsRtn.Tables["PackVersion"].Rows.Add(drEachLine);
                }

                #endregion
            }

            return dsRtn;
        }

        #endregion
    }
}
