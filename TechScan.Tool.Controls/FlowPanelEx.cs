using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.Controls
{
    public class FlowPanelEx : System.Windows.Forms.FlowLayoutPanel
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        private Color _borderColor = Color.FromArgb(184, 200, 210);
        private int _borderWidth = 1;

        [DefaultValue(null), Description("边框颜色")]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        [DefaultValue(null), Description("边框宽度")]
        public int BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
                this.Invalidate();
            }
        }

        public FlowPanelEx()
        {

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.Paint += new PaintEventHandler(FlowPanelEx_Paint);

        }

        private void FlowPanelEx_Paint(object sender, PaintEventArgs e)
        {
            if (this.BorderStyle == BorderStyle.FixedSingle)
            {
                IntPtr hDC = GetWindowDC(this.Handle);
                Graphics g = Graphics.FromHdc(hDC);

                ControlPaint.DrawBorder(
                    g,
                    new Rectangle(0, 0, this.Width, this.Height),
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid,
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid,
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid,
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid);
                g.Dispose();
                ReleaseDC(Handle, hDC);
            }
        }

        protected override Point ScrollToControl(System.Windows.Forms.Control activeControl)
        {
            return this.AutoScrollPosition;
        }
    }
}
