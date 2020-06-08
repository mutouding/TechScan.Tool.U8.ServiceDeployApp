/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   cModule.cs
* 功能描述： N/A
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-11-18 16:31:51
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-11-18 16:31:51		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.Controls
{
    internal sealed class cModule
    {
        // Token: 0x0600002F RID: 47 RVA: 0x00044314 File Offset: 0x00042714
        public static void applyGridTheme(ref DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.BackgroundColor = SystemColors.Window;
            grid.BorderStyle = BorderStyle.None;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersDefaultCellStyle = cModule.gridCellStyle;
            grid.ColumnHeadersHeight = 32;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.DefaultCellStyle = cModule.gridCellStyle2;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = SystemColors.GradientInactiveCaption;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = true;
            grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.RowHeadersDefaultCellStyle = cModule.gridCellStyle3;
            grid.Font = cModule.gridCellStyle.Font;

            //grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //设置自动调整高度  
            //grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
         }

        // Token: 0x06000030 RID: 48 RVA: 0x000443D4 File Offset: 0x000427D4
        public static void setGridRowHeader(ref DataGridView dgv, bool hSize = false)
        {
            dgv.TopLeftHeaderCell.Value = "NO ";
            dgv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
            try
            {
                foreach (object obj in dgv.Columns)
                {
                    DataGridViewColumn cCol = (DataGridViewColumn)obj;
                    bool flag = Operators.CompareString(cCol.ValueType.ToString(), typeof(DateTime).ToString(), false) == 0;
                    if (flag)
                    {
                        cCol.DefaultCellStyle = cModule.dateCellStyle;
                    }
                    else
                    {
                        flag = (Operators.CompareString(cCol.ValueType.ToString(), typeof(decimal).ToString(), false) == 0 | Operators.CompareString(cCol.ValueType.ToString(), typeof(double).ToString(), false) == 0);
                        if (flag)
                        {
                            cCol.DefaultCellStyle = cModule.amountCellStyle;
                        }
                    }
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //bool flag = enumerator is IDisposable;
                //if (flag)
                //{
                //    (enumerator as IDisposable).Dispose();
                //}
            }
            checked
            {
                if (hSize)
                {
                    dgv.RowHeadersWidth += 16;
                }
                dgv.AutoResizeColumns();
            }
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00044520 File Offset: 0x00042920
        public static void rowPostPaint_HeaderCount(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            string rowIdx = (checked(e.RowIndex + 1)).ToString();
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, Conversions.ToInteger(Operators.SubtractObject(e.RowBounds.Height, NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "rows", new object[]
			{
				e.RowIndex
			}, null, null, null), null, "DividerHeight", new object[0], null, null, null))));
            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        // Token: 0x0400000D RID: 13
        private static DataGridViewCellStyle dateCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleRight
        };

        // Token: 0x0400000E RID: 14
        private static DataGridViewCellStyle amountCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleRight,
            Format = "N2"
        };

        // Token: 0x0400000F RID: 15
        private static DataGridViewCellStyle gridCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = Color.FromArgb(79, 129, 189),
            Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = SystemColors.ControlLightLight,
            SelectionBackColor = SystemColors.Highlight,
            SelectionForeColor = SystemColors.HighlightText,
            WrapMode = DataGridViewTriState.True
        };

        // Token: 0x04000010 RID: 16
        private static DataGridViewCellStyle gridCellStyle2 = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = SystemColors.ControlLightLight,
            Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = SystemColors.ControlText,
            SelectionBackColor = Color.FromArgb(155, 187, 89),
            SelectionForeColor = SystemColors.HighlightText,
            WrapMode = DataGridViewTriState.False
        };

        // Token: 0x04000011 RID: 17
        private static DataGridViewCellStyle gridCellStyle3 = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = Color.Lavender,
            Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = SystemColors.WindowText,
            SelectionBackColor = Color.FromArgb(155, 187, 89),
            SelectionForeColor = SystemColors.HighlightText,
            WrapMode = DataGridViewTriState.True
        };
    }
}
