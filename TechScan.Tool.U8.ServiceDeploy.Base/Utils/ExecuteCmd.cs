/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   ExecuteCmd.cs
* 功能描述： CMD命令行操作帮助
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-08 14:14:46
* Client：   MYLPC
* 文件版本： V1.0.1

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-08 14:14:46		Myl		Create
* V1.0.1	2020-01-16      		Myl		修改为默认以管理员方式运行
* 
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// CMD命令行操作帮助
    /// </summary>
    public class ExecuteCmd
    {
        #region ExecuteCommand Sync and Async
        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="cmdString">string command</param>
        /// <returns>string, as output of the command.</returns>
        public void ExecuteCommandSync(object cmdString)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + cmdString);
                // The following commands are needed to redirect the standard output. 
                //This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;

                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();

                /*
                 * process.StandardInput.WriteLine(c);
            process.StandardInput.AutoFlush = true;
            process.StandardInput.WriteLine("exit");
 
            StreamReader reader = process.StandardOutput;//截取输出流
 
            string output = reader.ReadLine();//每次读取一行
 
            while (!reader.EndOfStream)
            {
                //PrintThrendInfo(output);
                output += reader.ReadLine();
            }
 
            process.WaitForExit();
                 */

                // return result;

            }
            catch (Exception objException)
            {
                // Log the exception
                throw objException;
            }
        }

        /// <summary>
        /// Execute the command Asynchronously.
        /// </summary>
        /// <param name="command">string command.</param>
        public void ExecuteCommandAsync(string cmdString)
        {
            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(cmdString);
            }
            catch (ThreadStartException objException)
            {
                // Log the exception
                throw objException;
            }
            catch (ThreadAbortException objException)
            {
                // Log the exception
                throw objException;
            }
            catch (Exception objException)
            {
                // Log the exception
                throw objException;
            }
        }

        /// <summary>
        /// 异步执行CMD命令
        /// </summary>
        /// <param name="cmdString">命令语句</param>
        /// <param name="workDirectory"></param>
        /// <returns></returns>
        public static async Task<string> ExecCmdAsync(string cmdString, string workDirectory = null)
        {
            return await Task.Run<string>(() =>
            {
                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + cmdString);
                // The following commands are needed to redirect the standard output. 
                //This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.WorkingDirectory = workDirectory;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                procStartInfo.Verb = "RunAs";//以管理员方式运行 Myl20200116
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the output into a string
                return proc.StandardOutput.ReadToEnd();
            });

            #region memo
            /*
             * process.StandardInput.WriteLine(c);
        process.StandardInput.AutoFlush = true;
        process.StandardInput.WriteLine("exit");

        StreamReader reader = process.StandardOutput;//截取输出流

        string output = reader.ReadLine();//每次读取一行

        while (!reader.EndOfStream)
        {
            //PrintThrendInfo(output);
            output += reader.ReadLine();
        }

        process.WaitForExit();
             */

            // return result;
            #endregion
        }

        #endregion
    }
}
