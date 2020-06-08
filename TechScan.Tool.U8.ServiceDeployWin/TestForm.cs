using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.Base;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Impl;
using TechScan.Tool.U8.ServiceDeployWin.Models;

namespace TechScan.Tool.U8.ServiceDeployWin
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }
        private string SelectProjectPath = "";
        private void button1_Click(object sender, EventArgs e)
        {
            var folder = "";
            using (var fsd = new FolderSelectDialog())
            {
                fsd.Title = "选择指定的文件夹发布";
                if (fsd.ShowDialog(this.Handle))
                {
                    folder = fsd.FileName;
                    if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder))
                    {
                        this.SelectProjectPath = folder;
                        label1.Text = folder;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }


            try
            {
                byte[] bt =
                ZipHelper.DoCreateFromDirectory("c:\\HondaAndroidService", CompressionLevel.Optimal, true);

                using (System.IO.FileStream fs = new FileStream("c:\\mytest.zip", FileMode.Create))
                {
                    fs.Write(bt, 0, bt.Length);
                    fs.Flush();
                    fs.Close();
                }

                //ZipHelper.ZipFiles(@"c:\my.mytest", Directory.GetFiles(folder, "*", SearchOption.AllDirectories));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var folder = "";
            using (var fsd = new FolderSelectDialog())
            {
                fsd.Title = "选择指定的文件夹发布";
                if (fsd.ShowDialog(this.Handle))
                {
                    folder = fsd.FileName;
                    if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder))
                    {
                        this.SelectProjectPath = folder;
                        label1.Text = folder;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }

            try
            {
                ZipHelpertest.Unzip(@"c:\\mytest.zip", folder);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //int i = IISHelper.GetIISVersion();
            //IISHelper.installsite();

            MessageBox.Show(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //IISHelper.IISPortIsOccupied(new List<int>() { 80, 81 });


                Process pro = new Process();

                // 设置命令行、参数
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.CreateNoWindow = true;
                // 启动CMD
                pro.Start();
                // 运行端口检查命令
                pro.StandardInput.WriteLine("netstat -ano");
                pro.StandardInput.WriteLine("exit");

                // 获取结果
                Regex reg = new Regex("\\s+", RegexOptions.Compiled);
                string line = null;
                while ((line = pro.StandardOutput.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                    {
                        line = reg.Replace(line, ",");

                        string[] arr = line.Split(',');
                        if (arr[1].EndsWith(":80"))
                        {
                            Console.WriteLine("80端口的进程ID：{0}", arr[4]);

                            int pid = Int32.Parse(arr[4]);
                            Process pro80 = Process.GetProcessById(pid);
                            // 处理该进程

                            break;
                        }
                    }
                }

                pro.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                bool b = IISHelper.ExistSqlServerService("W3SVC");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            //ZipHelper.UnZipToDirectory(@"C:\Users\Myl\Desktop\DeployTest\IIS_Publish_20191226192521.zip");
            //if (te?.i == 0)
            //{
            //    MessageBox.Show("y");
            //}
            //else
            //{

            //}
            //foreach (var er in Enum.GetValues(typeof(PackageType)))//枚举转List
            //{

            //}


            MessageBox.Show("Start33");

            label3.Text = await Task.Run(() =>
            {
                //for (int q = 0; q < 1000; q++)
                //{
                //    label1.Text = q.ToString();
                //    //Application.DoEvents();//实时响应文本框中的值
                //}
                Thread.Sleep(5000);
                return "123ttttt少时诵诗书";
            });

            MessageBox.Show("End33");
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Start");

            label1.Text = await Task.Run(() =>
            {
                //for (int q = 0; q < 1000; q++)
                //{
                //    label1.Text = q.ToString();
                //    //Application.DoEvents();//实时响应文本框中的值
                //}
                Thread.Sleep(5000);
                return "123ttttt";
            });



            MessageBox.Show("End");


        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<ServiceDeploy.Base.Impl.BaseHistoryItem> Details=null;

            MessageBox.Show(Details?.Count.ToString());
        }
    }

    class testa
    {
        public int i;
    }
}
