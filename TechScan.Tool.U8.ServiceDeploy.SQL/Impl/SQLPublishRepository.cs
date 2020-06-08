/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLPublishRepository.cs
* 功能描述： SQL脚本部署实现
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 16:40:19
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 16:40:19		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL脚本部署实现
    /// </summary>
    public class SQLPublishRepository
        : IDeployProvider<SQLConfig, SQLDeployParams>
    {
        #region Fileds & Property
        /// <summary>
        /// SQL脚本部署包临时解压目录
        /// </summary>
        private string PkgUnZipTempPath
        {
            get
            {
                var vUnZipTempPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), SQLMgmtEngine.SQL_UNZIP_TEMP_PATH);
                if (!System.IO.Directory.Exists(vUnZipTempPath))
                {
                    System.IO.Directory.CreateDirectory(vUnZipTempPath);
                }
                return vUnZipTempPath;
            }
        }

        public SQLConfig ConfigData
        {
            get;
            set;
        }

        public SQLDeployParams DeployParamData
        {
            get;
            set;
        }

        public string HistoryDataDir
        {
            get;
            set;
        }

        public Action<DeployLogType, string, bool, string> Log
        {
            get;
            set;
        }

        public string ProviderName
        {
            get
            {
                return "SQL";
            }
        }

        #endregion

        #region Methods

        public void DataValidationBeforeDeploy()
        {
            #region 检查是否已经安装了U8

            //if (!CheckU8Installed())
            //{
            //    throw new IISDeployBaseException("当前服务器还未安装U8系统，请确保U8应用服务已经安装！");
            //}

            #endregion

            if (string.IsNullOrEmpty(DeployParamData?.SourceDeployPackageFileName))
            {
                throw new SQLDeployBaseException($"请先指定SQL脚本安装包！");
            }
            if (string.IsNullOrEmpty(HistoryDataDir))
            {
                throw new IISDeployBaseException("请先指定历史数据目录！");
            }

            /*
             * Others todo
             */

            return;
        }

        public void Dispose()
        {
            return;
        }

        public Tuple<bool, string> ExecDeploy(Action<string> act)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<bool, string>> ExecDeployAsync(Action<string> act)
        {
            Log(DeployLogType.Info, "开始基础环境检测", false, "");
            act("开始基础环境检测");

            #region 基础环境数据校验
            DataValidationBeforeDeploy();
            Log(DeployLogType.Info, "基础环境检测通过!", false, "");
            act("基础环境检测通过!");
            #endregion

            #region Step1：解压包到发布目录

            Log(DeployLogType.Info, $"开始解压脚本包{DeployParamData?.SourceDeployPackageFileName}!", false, "");

            act("脚本包解压中");
            var sourcePackage = new SQLPackageRepository(DeployParamData?.SourceDeployPackageFileName);
            var vUnZipRtn = sourcePackage.ExecUnPack(DeployParamData?.SourceDeployPackageFileName, PkgUnZipTempPath);
            if (!vUnZipRtn.Item1)
            {
                Log(DeployLogType.Error, $"脚本包[{DeployParamData?.SourceDeployPackageFileName}]{vUnZipRtn.Item2}!", false, "");
                return vUnZipRtn;
            }

            DeployParamData.DeployVersion = new DeployVersion(DeployTypes.SQL, sourcePackage?.PackData?.PkgType ?? Base.Enums.PackageType.Release)
            {
                PackType = sourcePackage?.PackData?.PackType ?? DeployTypes.SQL,
                Version = sourcePackage?.PackData?.Version,
                PkgType = sourcePackage?.PackData?.PkgType ?? PackageType.Release,
                Description = sourcePackage?.PackData?.Description,
                PackageDateTime = (sourcePackage?.PackData?.PackageDateTime == null) ? DateTime.MinValue : sourcePackage.PackData.PackageDateTime,
                Packager = sourcePackage?.PackData?.Packager
            };

            Log(DeployLogType.Info, $"脚本包{DeployParamData?.SourceDeployPackageFileName}解压成功!", false, "");
            act("脚本包解压成功");

            #endregion

            #region 按照指定的规则获取所有SQL脚本，并排好序

            var sqlFiles = SqlFilesSearch.GetFiles(PkgUnZipTempPath);

            #endregion

            #region 检查SQL脚本的语法
            Log(DeployLogType.Info, "======开始进行SQL脚本语法检查======", false, "");
            act("开始SQL脚本语法检测");
            var bSqlHasError = false;
            sqlFiles.ForEach(A =>
            {
                string err = "";
                if (Parser.ParseSqlFile(A))
                {
                    foreach (var item in Parser.SqlErrors)
                    {
                        bSqlHasError = true;
                        err += $"脚本：[{Path.GetFileNameWithoutExtension(A)}]语法有误：{item.Message}line:{item.Line.ToString()}";
                    }
                    if (!string.IsNullOrEmpty(err))
                    {
                        Log(DeployLogType.Warn, err, false, "");
                        Log(DeployLogType.Warn, A, true, "SQL");
                    }
                }
                else
                {
                    Log(DeployLogType.Info, $"脚本[{Path.GetFileNameWithoutExtension(A)}]检查通过！", false, "");
                }
            });

            if (bSqlHasError)
            {
                Log(DeployLogType.Error, "SQL脚本语法检测不通过！", false, "");
                return Tuple.Create<bool, string>(false, "SQL脚本语法检测不通过！");
            }
            else
            {
                Log(DeployLogType.Info, $"所有脚本语法检查通过！", false, "");
                act("所有脚本语法检查通过");
            }

            #endregion

            #region 开始执行SQL

            Log(DeployLogType.Info, "======开始部署SQL脚本======", false, "");
            act("开始部署SQL脚本");

            foreach (var eachDb in DeployParamData?.DbServers)
            {
                Log(DeployLogType.Info, $"===Server:{eachDb.Server}DB:{eachDb.Database}===", false, "");

                var vExecRtn = await ExecuteSql(eachDb, sqlFiles);
                if (!vExecRtn.IsOk)
                {
                    return Tuple.Create<bool, string>(false, vExecRtn.Msg);
                }
            }
            Log(DeployLogType.Info, "脚本部署成功！", false, "");
            act("脚本部署成功");
            #endregion

            #region Step3：发布完成后数据处理

            //拷贝包到历史记录所在目录
            var cDestBackFileName = string.Empty;
            if (DeployParamData?.PublishType == PublishTypes.General)
            {
                //正常发布时需要备份，回滚则不需要备份了，本来就是备份zip包
                cDestBackFileName = CopyHelper.FileCopy(DeployParamData?.SourceDeployPackageFileName, HistoryDataDir);
            }
            else
            {
                cDestBackFileName = DeployParamData?.SourceDeployPackageFileName;
            }
            //添加包历史
            SQLDeployHistoryRepository hisFactory = new SQLDeployHistoryRepository(Base.Enums.HistoryType.DEPLOY_SQL, HistoryDataDir)
            {
                HistoryDir = HistoryDataDir
            };

            Tuple<bool, string> tAddRtn = hisFactory.AddHistory(new SQLDeployHistoryItem()
            {
                GUID = System.Guid.NewGuid().ToString("N").ToUpper(),
                TYPE = Base.Enums.HistoryType.DEPLOY_SQL,
                DeployParam = this.DeployParamData,
                ConfigData = this.ConfigData,
                HistoryPackageFileFullName = cDestBackFileName
            });
            if (tAddRtn.Item1)
            {
                Log(DeployLogType.Info, $"数据备份成功!{ Path.Combine(HistoryDataDir, Path.GetFileName(DeployParamData?.SourceDeployPackageFileName))}", true, "");
                act("数据备份成功");
                return Tuple.Create<bool, string>(true, "SQL脚本部署成功！");
            }
            else
            {
                return tAddRtn;
            }
            #endregion
        }

        private async Task<SqlCallResult> ExecuteSql(DbServerInfo dbServer, List<string> sqlFiles)
        {
            return await Task.Run<SqlCallResult>(() =>
            {
                using (var cnn = SqlHelper.CreateNewConnection(dbServer))
                {
                    if (cnn.State != ConnectionState.Open)
                    {
                        cnn.Open();
                    }
                    var command = cnn.CreateCommand();
                    var transaction = cnn.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.Connection = cnn;
                    command.Transaction = transaction;
                    var iIndex = 0;
                    try
                    {
                        for (int i = 0; i < sqlFiles.Count; i++)
                        {
                            var file = new FileInfo(sqlFiles[i]);
                            string script = file.OpenText().ReadToEnd();
                            command.CommandText = script;
                            command.ExecuteNonQuery();
                            Log(DeployLogType.Info, $"脚本{ Path.GetFileNameWithoutExtension(sqlFiles[i])}执行成功！", false, "");
                            Thread.Sleep(200);
                            iIndex++;
                        }
                        transaction.Commit();

                        Log(DeployLogType.Info, $"{dbServer.GetDbShortName()}中所有脚本执行成功！", false, "");

                        return new SqlCallResult() { IsOk = true, Msg = "" };
                    }
                    catch (Exception e)
                    {
                        Log(DeployLogType.Error, $"{dbServer.GetDbShortName()}中脚本[{sqlFiles[iIndex]}执行失败[{e.Message}]", true, "");
                        Log(DeployLogType.Info, $"开始脚本回滚...", false, "");

                        try
                        {
                            transaction.Rollback();
                        }
                        catch (SqlException ex)
                        {
                            if (transaction.Connection != null)
                            {
                                Log(DeployLogType.Error, $"{dbServer.GetDbShortName()}中脚本[{sqlFiles[iIndex]}执行失败，回滚失败！{ex.Message}", false, "");
                                //log.Error("An exception of type " + ex.GetType() +
                                //    " was encountered while attempting to roll back the transaction.");
                            }
                        }

                        //Log(DeployLogType.Error, $"An exception of type {e.GetType()} was encountered while inserting the data.", false);
                        Log(DeployLogType.Info, $"脚本回滚成功！", false, "");
                        return new SqlCallResult() { IsOk = false, Msg = $"{dbServer.GetDbShortName()}中脚本[{sqlFiles[iIndex]}执行失败！{e.Message}" };
                    }
                }
            });
        }

        #endregion
    }
}
