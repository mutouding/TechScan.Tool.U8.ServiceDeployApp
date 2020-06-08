using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.Controls
{
    public partial class MasterGrid : DataGridView
    {
        #region "Variables"
        private DataSet _cDataset;
        private string _foreignKey;
        private string _filterFormat;
        // Token: 0x04000016 RID: 22
        public List<int> rowCurrent;

        // Token: 0x04000017 RID: 23
        public object rowDefaultHeight;

        // Token: 0x04000018 RID: 24
        public object rowExpandedHeight;

        // Token: 0x04000019 RID: 25
        public object rowDefaultDivider;

        // Token: 0x0400001A RID: 26
        public object rowExpandedDivider;

        // Token: 0x0400001B RID: 27
        public object rowDividerMargin;

        // Token: 0x0400001C RID: 28
        public bool collapseRow;

        // Token: 0x0400001D RID: 29
        public DetailGrid childView;
        #endregion

        /// <summary>
        /// Myl 200102 暂时无用 测试用的
        /// </summary>
        /// <param name="ds"></param>
        public void InitGrid(ref DataSet ds)
        {
            base.Scroll += this.MasterControl_Scroll;
            base.SelectionChanged += this.MasterControl_SelectionChanged;
            base.RowHeaderMouseClick += this.MasterControl_RowHeaderMouseClick;
            base.RowPostPaint += this.MasterControl_RowPostPaint;
            this.rowCurrent = new List<int>();
            this.rowDefaultHeight = 22;
            this.rowExpandedHeight = 300;
            this.rowDefaultDivider = 0;
            this.rowExpandedDivider = 278;
            this.rowDividerMargin = 5;
            this.childView = new DetailGrid()
            {
                Height = Conversions.ToInteger(Operators.SubtractObject(this.rowExpandedDivider, Operators.MultiplyObject(this.rowDividerMargin, 2))),
                Visible = false
            };
            this.Controls.Add(this.childView);
            //this.InitializeComponent();
            this._cDataset = ds;
            this.childView._cDataset = ds;
            DataGridView dataGridView = this;
            cModule.applyGridTheme(ref dataGridView);
            this.Dock = DockStyle.Fill;
        }

        public MasterGrid(ref DataSet cDataset)
        {
            base.Scroll += this.MasterControl_Scroll;
            base.SelectionChanged += this.MasterControl_SelectionChanged;
            base.RowHeaderMouseClick += this.MasterControl_RowHeaderMouseClick;
            base.RowPostPaint += this.MasterControl_RowPostPaint;
            this.rowCurrent = new List<int>();
            this.rowDefaultHeight = 22;
            this.rowExpandedHeight = 300;
            this.rowDefaultDivider = 0;
            this.rowExpandedDivider = 278;
            this.rowDividerMargin = 5;
            this.childView = new DetailGrid()
            {
                Height = Conversions.ToInteger(Operators.SubtractObject(this.rowExpandedDivider, Operators.MultiplyObject(this.rowDividerMargin, 2))),
                Visible = false
            };
            this.Controls.Add(this.childView);
            this.InitializeComponent();
            this._cDataset = cDataset;
            this.childView._cDataset = cDataset;
            DataGridView dataGridView = this;
            cModule.applyGridTheme(ref dataGridView);
            this.Dock = DockStyle.Fill;
        }

        public MasterGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public void SetParentSource(string tableName, string foreignKey)
        {
            this.DataSource = new DataView(this._cDataset.Tables[tableName]);
            DataGridView dataGridView = this;
            cModule.setGridRowHeader(ref dataGridView, false);
            this._foreignKey = foreignKey;
            bool flag = Operators.CompareString(this._cDataset.Tables[tableName].Columns[foreignKey].GetType().ToString(), typeof(int).ToString(), false) == 0 | Operators.CompareString(this._cDataset.Tables[tableName].Columns[foreignKey].GetType().ToString(), typeof(double).ToString(), false) == 0 | Operators.CompareString(this._cDataset.Tables[tableName].Columns[foreignKey].GetType().ToString(), typeof(decimal).ToString(), false) == 0;
            if (flag)
            {
                this._filterFormat = foreignKey + "={0}";
            }
            else
            {
                this._filterFormat = foreignKey + "='{0}'";
            }
        }
        private void MasterControl_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Rectangle rect = new Rectangle(Conversions.ToInteger(Operators.DivideObject(Operators.SubtractObject(this.rowDefaultHeight, 16), 2)), Conversions.ToInteger(Operators.DivideObject(Operators.SubtractObject(this.rowDefaultHeight, 16), 2)), 16, 16);
            bool flag = rect.Contains(e.Location);
            if (flag)
            {
                bool flag2 = this.rowCurrent.Contains(e.RowIndex);
                if (flag2)
                {
                    this.rowCurrent.Clear();
                    this.Rows[e.RowIndex].Height = Conversions.ToInteger(this.rowDefaultHeight);
                    this.Rows[e.RowIndex].DividerHeight = Conversions.ToInteger(this.rowDefaultDivider);
                }
                else
                {
                    flag2 = (this.rowCurrent.Count != 0);
                    if (flag2)
                    {
                        int eRow = this.rowCurrent[0];
                        this.rowCurrent.Clear();
                        this.Rows[eRow].Height = Conversions.ToInteger(this.rowDefaultHeight);
                        this.Rows[eRow].DividerHeight = Conversions.ToInteger(this.rowDefaultDivider);
                        this.ClearSelection();
                        this.collapseRow = true;
                        this.Rows[eRow].Selected = true;
                    }
                    this.rowCurrent.Add(e.RowIndex);
                    this.Rows[e.RowIndex].Height = Conversions.ToInteger(this.rowExpandedHeight);
                    this.Rows[e.RowIndex].DividerHeight = Conversions.ToInteger(this.rowExpandedDivider);
                }
                this.ClearSelection();
                this.collapseRow = true;
                this.Rows[e.RowIndex].Selected = true;
            }
            else
            {
                this.collapseRow = false;
            }
        }
        private void MasterControl_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rect = new Rectangle(Conversions.ToInteger(Operators.AddObject(e.RowBounds.X, Operators.DivideObject(Operators.SubtractObject(this.rowDefaultHeight, 16), 2))), Conversions.ToInteger(Operators.AddObject(e.RowBounds.Y, Operators.DivideObject(Operators.SubtractObject(this.rowDefaultHeight, 16), 2))), 16, 16);
            bool flag = this.collapseRow;
            if (flag)
            {
                bool flag2 = this.rowCurrent.Contains(e.RowIndex);
                if (flag2)
                {
                    NewLateBinding.LateSetComplex(NewLateBinding.LateGet(sender, null, "Rows", new object[]
                    {
                        e.RowIndex
                    }, null, null, null), null, "DividerHeight", new object[]
                    {
                        Operators.SubtractObject(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Rows", new object[]
                        {
                            e.RowIndex
                        }, null, null, null), null, "height", new object[0], null, null, null), this.rowDefaultHeight)
                    }, null, null, false, true);
                    e.Graphics.DrawImage(this.RowHeaderIconList.Images[1], rect);
                    System.Windows.Forms.Control control = this.childView;
                    Point location = new Point(Conversions.ToInteger(Operators.AddObject(e.RowBounds.Left, NewLateBinding.LateGet(sender, null, "RowHeadersWidth", new object[0], null, null, null))), Conversions.ToInteger(Operators.AddObject(Operators.AddObject(e.RowBounds.Top, this.rowDefaultHeight), 5)));
                    control.Location = location;
                    this.childView.Width = Conversions.ToInteger(Operators.SubtractObject(e.RowBounds.Right, NewLateBinding.LateGet(sender, null, "rowheaderswidth", new object[0], null, null, null)));
                    this.childView.Height = Conversions.ToInteger(Operators.SubtractObject(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Rows", new object[]
                    {
                        e.RowIndex
                    }, null, null, null), null, "DividerHeight", new object[0], null, null, null), 10));
                    this.childView.Visible = true;
                }
                else
                {
                    this.childView.Visible = false;
                    e.Graphics.DrawImage(this.RowHeaderIconList.Images[0], rect);
                }
                this.collapseRow = false;
            }
            else
            {
                bool flag2 = this.rowCurrent.Contains(e.RowIndex);
                if (flag2)
                {
                    NewLateBinding.LateSetComplex(NewLateBinding.LateGet(sender, null, "Rows", new object[]
                    {
                        e.RowIndex
                    }, null, null, null), null, "DividerHeight", new object[]
                    {
                        Operators.SubtractObject(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Rows", new object[]
                        {
                            e.RowIndex
                        }, null, null, null), null, "height", new object[0], null, null, null), this.rowDefaultHeight)
                    }, null, null, false, true);
                    e.Graphics.DrawImage(this.RowHeaderIconList.Images[1], rect);
                    System.Windows.Forms.Control control2 = this.childView;
                    Point location = new Point(Conversions.ToInteger(Operators.AddObject(e.RowBounds.Left, NewLateBinding.LateGet(sender, null, "RowHeadersWidth", new object[0], null, null, null))), Conversions.ToInteger(Operators.AddObject(Operators.AddObject(e.RowBounds.Top, this.rowDefaultHeight), 5)));
                    control2.Location = location;
                    this.childView.Width = Conversions.ToInteger(Operators.SubtractObject(e.RowBounds.Right, NewLateBinding.LateGet(sender, null, "rowheaderswidth", new object[0], null, null, null)));
                    this.childView.Height = Conversions.ToInteger(Operators.SubtractObject(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Rows", new object[]
                    {
                        e.RowIndex
                    }, null, null, null), null, "DividerHeight", new object[0], null, null, null), 10));
                    this.childView.Visible = true;
                }
                else
                {
                    e.Graphics.DrawImage(this.RowHeaderIconList.Images[0], rect);
                }
            }
            cModule.rowPostPaint_HeaderCount(RuntimeHelpers.GetObjectValue(sender), e);
        }
        private void MasterControl_Scroll(object sender, ScrollEventArgs e)
        {
            bool flag = this.rowCurrent.Count != 0;
            if (flag)
            {
                this.collapseRow = true;
                this.ClearSelection();
                this.Rows[this.rowCurrent[0]].Selected = true;
            }
        }
        private void MasterControl_SelectionChanged(object sender, EventArgs e)
        {
            bool flag = this.RowCount != 0;
            if (flag)
            {
                bool flag2 = this.rowCurrent.Contains(this.CurrentRow.Index);
                if (flag2)
                {
                    try
                    {
                        foreach (DataGridView cGrid in this.childView.childGrid)
                        {
                            ((DataView)cGrid.DataSource).RowFilter = string.Format(this._filterFormat, RuntimeHelpers.GetObjectValue(this[this._foreignKey, this.CurrentRow.Index].Value));
                        }
                    }
                    finally
                    {
                        //List<DataGridView>.Enumerator enumerator;
                        //((IDisposable)enumerator).Dispose();
                    }
                }
            }
        }


        #region 选择的回滚项目 --通过GUID来 2020-01-02 Myl

        /// <summary>
        /// 获取当前版本行的GUID值
        /// </summary>
        public string SelectedVersionGUID
        {
            get
            {
                if (this.DataSource == null)
                {
                    return "";
                }

                if (this.CurrentRow == null)
                {
                    throw new Exception("请先选择需要回滚的版本行！");
                }
                return this.CurrentRow.Cells["GUID"]?.Value.ToString().Trim();                 
            }
            private set
            { }
        }


        #endregion
    }

    public enum rowHeaderIcons
    {
        // Token: 0x04000024 RID: 36
        expand,
        // Token: 0x04000025 RID: 37
        collapse
    }
}
