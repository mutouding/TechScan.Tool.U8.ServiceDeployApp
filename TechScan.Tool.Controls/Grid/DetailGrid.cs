using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.Controls
{
    public partial class DetailGrid : TabControl
    {
        #region "Variables"
        internal List<DataGridView> childGrid;
        internal DataSet _cDataset;
        #endregion

        public DetailGrid()
        {
            InitializeComponent();
            childGrid = new List<DataGridView>();
        }
        public DetailGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #region "Populate Childview"
        public void Add(string tableName, string pageCaption)
        {
            TabPage tPage = new TabPage() { Text = pageCaption };
            this.TabPages.Add(tPage);
            DataGridView newGrid = new DataGridView() 
            {
                Dock= DockStyle.Fill,
                DataSource = new DataView(_cDataset.Tables[tableName])
            };
            tPage.Controls.Add(newGrid);
            cModule.applyGridTheme(ref newGrid);
            cModule.setGridRowHeader(ref newGrid);
            newGrid.RowPostPaint += cModule.rowPostPaint_HeaderCount;
            this.childGrid.Add(newGrid);
        }

        #endregion
    }
}
