namespace TechScan.Tool.Controls
{
    partial class MasterGrid
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterGrid));
            this.RowHeaderIconList = new System.Windows.Forms.ImageList(this.components);
            // 
            // RowHeaderIconList
            // 
            this.RowHeaderIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("RowHeaderIconList.ImageStream")));
            this.RowHeaderIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.RowHeaderIconList.Images.SetKeyName(0, "expand.png");
            this.RowHeaderIconList.Images.SetKeyName(1, "collapse.png");

        }

        #endregion

        internal System.Windows.Forms.ImageList RowHeaderIconList;
    }
}
