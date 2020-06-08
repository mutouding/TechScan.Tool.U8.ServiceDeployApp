namespace TechScan.Tool.U8.ServiceDeployWin.SQL
{
    partial class SqlScriptShowForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlScriptShowForm));
            this.rtbSQL = new ICSharpCode.TextEditor.TextEditorControl();
            this.picFileLoading = new TechScan.Tool.Controls.SpinningCircles();
            this.SuspendLayout();
            // 
            // rtbSQL
            // 
            this.rtbSQL.AllowDrop = true;
            this.rtbSQL.AutoScroll = true;
            this.rtbSQL.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.rtbSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSQL.IsReadOnly = false;
            this.rtbSQL.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.FullRow;
            this.rtbSQL.Location = new System.Drawing.Point(0, 0);
            this.rtbSQL.Name = "rtbSQL";
            this.rtbSQL.ShowEOLMarkers = true;
            this.rtbSQL.ShowHRuler = true;
            this.rtbSQL.ShowInvalidLines = true;
            this.rtbSQL.ShowSpaces = true;
            this.rtbSQL.ShowTabs = true;
            this.rtbSQL.Size = new System.Drawing.Size(705, 524);
            this.rtbSQL.TabIndex = 1;
            // 
            // picFileLoading
            // 
            this.picFileLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picFileLoading.BackColor = System.Drawing.Color.Transparent;
            this.picFileLoading.FullTransparent = true;
            this.picFileLoading.Increment = 1F;
            this.picFileLoading.Location = new System.Drawing.Point(283, 184);
            this.picFileLoading.N = 8;
            this.picFileLoading.Name = "picFileLoading";
            this.picFileLoading.Radius = 2.5F;
            this.picFileLoading.Size = new System.Drawing.Size(139, 157);
            this.picFileLoading.TabIndex = 8;
            this.picFileLoading.Text = "FileLoading";
            // 
            // SqlScriptShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 524);
            this.Controls.Add(this.picFileLoading);
            this.Controls.Add(this.rtbSQL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SqlScriptShowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL";
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl rtbSQL;
        private Controls.SpinningCircles picFileLoading;
    }
}