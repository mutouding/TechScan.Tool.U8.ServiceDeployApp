using ICSharpCode.TextEditor.Document;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeployWin.Util;

namespace TechScan.Tool.U8.ServiceDeployWin.SQL
{
    /// <summary>
    /// 显示SQL脚本窗体
    /// </summary>
    public partial class SqlScriptShowForm : Form
    {
        #region Fields & Property

        private string m_SqlFileName;

        #endregion

        #region Ctor
        public SqlScriptShowForm(string sqlFileName)
        {
            InitializeComponent();
            m_SqlFileName = sqlFileName;
            this.Load += SqlScriptShowForm_Load;
        }

        private async void SqlScriptShowForm_Load(object sender, EventArgs e)
        {
            ControlLoadingCircle(true);
            
            await LoadSqlScript();

            ControlLoadingCircle(false);
        }
        #endregion

        #region Methods

        private void ControlLoadingCircle(bool bShow)
        {
            this.BeginInvoke(new Action(() =>
            {
                picFileLoading.Visible = bShow;
            }));
        }

        private async Task LoadSqlScript()
        {
            try
            {
                if (string.IsNullOrEmpty(m_SqlFileName) || !System.IO.File.Exists(m_SqlFileName))
                {
                    return;
                }
                this.Text = $"SQL[{m_SqlFileName}]";
                await Task.Run(() =>
                {
                    var text = File.ReadAllText(m_SqlFileName);
                    if (rtbSQL.InvokeRequired)
                    {
                        rtbSQL.Invoke(new Action(() => 
                        {
                            rtbSQL.Text = text;
                            //https://blog.csdn.net/weixin_34290390/article/details/85912387
                            rtbSQL.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
                            rtbSQL.Encoding = System.Text.Encoding.Default;
                        }));
                    }
                    else
                    {
                        rtbSQL.Text = text;
                    }
                    Thread.Sleep(300);
                });
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        #endregion
    }
}
