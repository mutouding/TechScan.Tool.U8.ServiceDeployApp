namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    partial class frmZipDeployPackage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZipDeployPackage));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pnlZipCtls = new TechScan.Tool.Controls.FlowPanelEx();
            this.btnAddZipCtl = new TechScan.Tool.Controls.ButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlZipCtls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddZipCtl)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlZipCtls);
            this.splitContainer1.Size = new System.Drawing.Size(884, 662);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pnlZipCtls
            // 
            this.pnlZipCtls.AutoScroll = true;
            this.pnlZipCtls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlZipCtls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlZipCtls.BackgroundImage")));
            this.pnlZipCtls.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.pnlZipCtls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlZipCtls.BorderWidth = 1;
            this.pnlZipCtls.Controls.Add(this.btnAddZipCtl);
            this.pnlZipCtls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlZipCtls.Location = new System.Drawing.Point(0, 0);
            this.pnlZipCtls.Name = "pnlZipCtls";
            this.pnlZipCtls.Size = new System.Drawing.Size(884, 437);
            this.pnlZipCtls.TabIndex = 1;
            // 
            // btnAddZipCtl
            // 
            this.btnAddZipCtl.BackColor = System.Drawing.Color.Transparent;
            this.btnAddZipCtl.Image = ((System.Drawing.Image)(resources.GetObject("btnAddZipCtl.Image")));
            this.btnAddZipCtl.ImageClick = ((System.Drawing.Image)(resources.GetObject("btnAddZipCtl.ImageClick")));
            this.btnAddZipCtl.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnAddZipCtl.ImageHover")));
            this.btnAddZipCtl.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnAddZipCtl.ImageNormal")));
            this.btnAddZipCtl.IsSwitch = false;
            this.btnAddZipCtl.Location = new System.Drawing.Point(5, 5);
            this.btnAddZipCtl.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddZipCtl.Name = "btnAddZipCtl";
            this.btnAddZipCtl.Selected = false;
            this.btnAddZipCtl.Size = new System.Drawing.Size(120, 120);
            this.btnAddZipCtl.TabIndex = 0;
            this.btnAddZipCtl.TabStop = false;
            this.btnAddZipCtl.Click += new System.EventHandler(this.btnAddZipCtl_Click);
            // 
            // frmZipDeployPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(130)))), ((int)(((byte)(194)))));
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmZipDeployPackage";
            this.Text = "frmZipIISDeployPackage";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlZipCtls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAddZipCtl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.FlowPanelEx pnlZipCtls;
        private Controls.ButtonEx btnAddZipCtl;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}