using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace PrintNiceLabel.VICO.Controls
{
	// Token: 0x02000002 RID: 2
	public class GraphicButton : Button
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public GraphicButton()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002144 File Offset: 0x00000344
		public GraphicButton(IContainer container)
		{
			container.Add(this);
			this.InitializeComponent();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000223D File Offset: 0x0000043D
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002245 File Offset: 0x00000445
		[Category("Customer属性")]
		[Description("右侧和上侧(外)的颜色")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "233,249,249")]
		public Color LeftOutsideColor
		{
			get
			{
				return this.leftOutsideColor;
			}
			set
			{
				this.leftOutsideColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002254 File Offset: 0x00000454
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000225C File Offset: 0x0000045C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "58,242,237")]
		[Category("Customer属性")]
		[Description("左侧和上侧(内)的颜色")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public Color LeftInsideColor
		{
			get
			{
				return this.leftInsideColor;
			}
			set
			{
				this.leftInsideColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000226B File Offset: 0x0000046B
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002273 File Offset: 0x00000473
		[DefaultValue(typeof(Color), "0,0,0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Customer属性")]
		[Description("右侧和下侧(外)的颜色")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public Color RightOutsideColor
		{
			get
			{
				return this.rightOutsideColor;
			}
			set
			{
				this.rightOutsideColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002282 File Offset: 0x00000482
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000228A File Offset: 0x0000048A
		[Description("右侧和下侧(内)的颜色")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "25,126,198")]
		[Category("Customer属性")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public Color RightInsideColor
		{
			get
			{
				return this.rightInsideColor;
			}
			set
			{
				this.rightInsideColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002299 File Offset: 0x00000499
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000022A1 File Offset: 0x000004A1
		[DefaultValue(typeof(Color), "98, 183, 243")]
		[Description("背景(起点)的颜色")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Customer属性")]
		public Color RectangleColor1
		{
			get
			{
				return this.rectangleColor1;
			}
			set
			{
				this.rectangleColor1 = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022B0 File Offset: 0x000004B0
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022B8 File Offset: 0x000004B8
		[RefreshProperties(RefreshProperties.Repaint)]
		[Description("背景(终点)的颜色")]
		[Category("Customer属性")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "25,126,198")]
		public Color RectangleColor2
		{
			get
			{
				return this.rectangleColor2;
			}
			set
			{
				this.rectangleColor2 = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022C7 File Offset: 0x000004C7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022CF File Offset: 0x000004CF
		[Category("Customer属性")]
		[Description("鼠标按下的颜色")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "80,85,88")]
		public Color MousePushedColor
		{
			get
			{
				return this.mousePushedColor;
			}
			set
			{
				this.mousePushedColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022DE File Offset: 0x000004DE
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022E6 File Offset: 0x000004E6
		[Category("Customer属性")]
		[DefaultValue(typeof(Color), "255,255,255")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("无效状态的颜色")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public Color DisableColor1
		{
			get
			{
				return this.disableColor1;
			}
			set
			{
				this.disableColor1 = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022F5 File Offset: 0x000004F5
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022FD File Offset: 0x000004FD
		[RefreshProperties(RefreshProperties.Repaint)]
		[Category("Customer属性")]
		[Description("无效状态的背景颜色")]
		[DefaultValue(typeof(Color), "192,192,192")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color DisableColor2
		{
			get
			{
				return this.disableColor2;
			}
			set
			{
				this.disableColor2 = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000230C File Offset: 0x0000050C
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002314 File Offset: 0x00000514
		[Browsable(true)]
		[Category("Customer属性")]
		[Description("MaskColor")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color MaskColor
		{
			get
			{
				return this.maskColor;
			}
			set
			{
				this.maskColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002324 File Offset: 0x00000524
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			base.Region = new Region(base.ClientRectangle);
			e.Graphics.Clear(Color.Transparent);
			int x = base.Width;
			int y = base.Height;
			this.CreatePensBrushes();
			if (!base.Enabled)
			{
				e.Graphics.FillRectangle(this.disableBrush1, 0, 0, x, y);
				e.Graphics.FillRectangle(this.disableBrush2, 1, 1, x - 2, y - 2);
			}
			else
			{
				switch (this.state)
				{
				case GraphicButton.States.Normal:
					e.Graphics.DrawLine(this.leftOutsidePen, 0, 0, 0, y - 2);
					e.Graphics.DrawLine(this.leftOutsidePen, 1, 0, x - 1, 0);
					e.Graphics.DrawLine(this.leftInsidePen, 1, 2, 1, y - 2);
					e.Graphics.DrawLine(this.leftInsidePen, 1, 1, x - 2, 1);
					e.Graphics.DrawLine(this.rightOutsidePen, x - 1, 1, x - 1, y - 2);
					e.Graphics.DrawLine(this.rightOutsidePen, 0, y - 1, x - 1, y - 1);
					e.Graphics.DrawLine(this.rightInsidePen, x - 2, 2, x - 2, y - 3);
					e.Graphics.DrawLine(this.rightInsidePen, 2, y - 2, x - 2, y - 2);
					e.Graphics.FillRectangle(this.rectangleBrush, 2, 2, x - 4, y - 4);
					break;
				case GraphicButton.States.Pushed:
					e.Graphics.FillRectangle(this.rectangleBrushReverse, 2, 2, x - 4, y - 4);
					e.Graphics.DrawRectangle(this.mousePushedPen, 1, 1, x - 2, y - 2);
					break;
				}
				if (this.Focused)
				{
					ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(3, 3, x - 6, y - 6));
				}
			}
			this.DisposePensBrushes();
			StringFormat sf = new StringFormat();
			sf.HotkeyPrefix = HotkeyPrefix.Show;
			this.GetPoints();
			if (base.Image != null)
			{
				if (base.Enabled)
				{
					e.Graphics.DrawImage(base.Image, this.iPoint);
				}
				else
				{
					ControlPaint.DrawImageDisabled(e.Graphics, base.Image, this.iPoint.X, this.iPoint.Y, this.BackColor);
				}
			}
			if (this.Text.Length != 0)
			{
				e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), this.tPoint, sf);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025A8 File Offset: 0x000007A8
		private void GetPoints()
		{
			int X = base.Width;
			int Y = base.Height;
			Image image = base.Image;
			string text = this.Text;
			if (image != null)
			{
				if (text.Length == 0)
				{
					this.iPoint = new Point((X - image.Width) / 2, (Y - image.Height) / 2);
				}
				else
				{
					this.iPoint = new Point(7, (Y - image.Height) / 2);
				}
				this.tPoint = new Point(7 + image.Width + 5, (Y - this.Font.Height) / 2);
				return;
			}
			this.tPoint = new Point((X - this.GetTextSize(base.CreateGraphics(), text.Replace("&", ""), this.Font, new Size(X, Y)).Width - 2) / 2, (Y - this.Font.Height) / 2);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000268C File Offset: 0x0000088C
		private Size GetTextSize(Graphics graphics, string text, Font font, Size size)
		{
			if (text.Length == 0)
			{
				return Size.Empty;
			}
			StringFormat format = new StringFormat();
			format.FormatFlags = StringFormatFlags.FitBlackBox;
			RectangleF layoutRect = new RectangleF(0f, 0f, (float)size.Width, (float)size.Height);
			CharacterRange[] chRange = new CharacterRange[]
			{
				new CharacterRange(0, text.Length)
			};
			Region[] regs = new Region[1];
			format.SetMeasurableCharacterRanges(chRange);
			regs = graphics.MeasureCharacterRanges(text, font, layoutRect, format);
			Rectangle rect = Rectangle.Round(regs[0].GetBounds(graphics));
			return new Size(rect.Width, rect.Height);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002735 File Offset: 0x00000935
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				this.state = GraphicButton.States.Pushed;
				base.Invalidate();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002758 File Offset: 0x00000958
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.state = GraphicButton.States.Normal;
			base.Invalidate();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000276E File Offset: 0x0000096E
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			if (ke.KeyData == Keys.Return || ke.KeyData == Keys.Space)
			{
				this.state = GraphicButton.States.Pushed;
				base.Invalidate();
			}
			base.OnKeyDown(ke);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002798 File Offset: 0x00000998
		protected override void OnKeyUp(KeyEventArgs ke)
		{
			base.OnKeyUp(ke);
			this.state = GraphicButton.States.Normal;
			base.Invalidate();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027B0 File Offset: 0x000009B0
		private void CreatePensBrushes()
		{
			this.DisposePensBrushes();
			if (base.Region == null)
			{
				return;
			}
			int width = base.Width;
			int height = base.Height;
			this.leftOutsidePen = new Pen(this.leftOutsideColor);
			this.leftInsidePen = new Pen(this.leftInsideColor);
			this.rightOutsidePen = new Pen(this.rightOutsideColor);
			this.rightInsidePen = new Pen(this.rightInsideColor);
			this.mouseOverPen = new Pen(this.mouseOverColor, 2f);
			this.mousePushedPen = new Pen(this.mousePushedColor, 2f);
			this.disableBrush1 = new SolidBrush(this.disableColor1);
			this.disableBrush2 = new SolidBrush(this.disableColor2);
			this.rectangleBrush = new LinearGradientBrush(base.ClientRectangle, this.rectangleColor1, this.rectangleColor2, 90f);
			this.rectangleBrushReverse = new LinearGradientBrush(base.ClientRectangle, this.rectangleColor2, this.rectangleColor1, 90f);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028B0 File Offset: 0x00000AB0
		private void DisposePensBrushes()
		{
			if (this.leftOutsidePen != null)
			{
				this.leftOutsidePen.Dispose();
			}
			if (this.leftInsidePen != null)
			{
				this.leftInsidePen.Dispose();
			}
			if (this.rightOutsidePen != null)
			{
				this.rightOutsidePen.Dispose();
			}
			if (this.rightInsidePen != null)
			{
				this.rightInsidePen.Dispose();
			}
			if (this.mouseOverPen != null)
			{
				this.mouseOverPen.Dispose();
			}
			if (this.mousePushedPen != null)
			{
				this.mousePushedPen.Dispose();
			}
			if (this.disableBrush1 != null)
			{
				this.disableBrush1.Dispose();
			}
			if (this.disableBrush2 != null)
			{
				this.disableBrush2.Dispose();
			}
			if (this.rectangleBrush != null)
			{
				this.rectangleBrush.Dispose();
			}
			if (this.rectangleBrushReverse != null)
			{
				this.rectangleBrushReverse.Dispose();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000297C File Offset: 0x00000B7C
		private void CreateRegion()
		{
			int x = base.Width;
			int y = base.Height;
			Point[] points = new Point[]
			{
				new Point(0, 0),
				new Point(x, 0),
				new Point(x, y),
				new Point(0, y)
			};
			GraphicsPath path = new GraphicsPath();
			path.AddLines(points);
			base.Region = new Region(path);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A0B File Offset: 0x00000C0B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A2A File Offset: 0x00000C2A
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x04000001 RID: 1
		private const int LeftMargin = 7;

		// Token: 0x04000002 RID: 2
		private const int TextMargin = 5;

		// Token: 0x04000003 RID: 3
		private GraphicButton.States state;

		// Token: 0x04000004 RID: 4
		private Color leftOutsideColor = Color.FromArgb(233, 249, 249);

		// Token: 0x04000005 RID: 5
		private Color leftInsideColor = Color.FromArgb(58, 242, 237);

		// Token: 0x04000006 RID: 6
		private Color rightOutsideColor = Color.FromArgb(0, 0, 0);

		// Token: 0x04000007 RID: 7
		private Color rightInsideColor = Color.FromArgb(25, 126, 198);

		// Token: 0x04000008 RID: 8
		private Color rectangleColor1 = Color.FromArgb(98, 183, 243);

		// Token: 0x04000009 RID: 9
		private Color rectangleColor2 = Color.FromArgb(25, 126, 198);

		// Token: 0x0400000A RID: 10
		private Color mouseOverColor = Color.Yellow;

		// Token: 0x0400000B RID: 11
		private Color mousePushedColor = Color.FromArgb(80, 85, 88);

		// Token: 0x0400000C RID: 12
		private Color disableColor1 = Color.FromArgb(255, 255, 255);

		// Token: 0x0400000D RID: 13
		private Color disableColor2 = Color.FromArgb(192, 192, 192);

		// Token: 0x0400000E RID: 14
		private Color maskColor = Color.Red;

		// Token: 0x0400000F RID: 15
		private Point iPoint;

		// Token: 0x04000010 RID: 16
		private Point tPoint;

		// Token: 0x04000011 RID: 17
		private Pen leftOutsidePen;

		// Token: 0x04000012 RID: 18
		private Pen leftInsidePen;

		// Token: 0x04000013 RID: 19
		private Pen rightOutsidePen;

		// Token: 0x04000014 RID: 20
		private Pen rightInsidePen;

		// Token: 0x04000015 RID: 21
		private Pen mouseOverPen;

		// Token: 0x04000016 RID: 22
		private Pen mousePushedPen;

		// Token: 0x04000017 RID: 23
		private LinearGradientBrush rectangleBrush;

		// Token: 0x04000018 RID: 24
		private LinearGradientBrush rectangleBrushReverse;

		// Token: 0x04000019 RID: 25
		private SolidBrush disableBrush1;

		// Token: 0x0400001A RID: 26
		private SolidBrush disableBrush2;

		// Token: 0x0400001B RID: 27
		private IContainer components;

		// Token: 0x02000003 RID: 3
		private enum States
		{
			// Token: 0x0400001D RID: 29
			Normal,
			// Token: 0x0400001E RID: 30
			MouseOver,
			// Token: 0x0400001F RID: 31
			Pushed
		}
	}
}
