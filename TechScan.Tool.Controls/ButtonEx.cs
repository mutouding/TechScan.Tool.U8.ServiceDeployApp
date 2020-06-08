using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.Controls
{
    public class ButtonEx : System.Windows.Forms.PictureBox
    {
        private Image myImageNormal;
        private Image myImageHover;
        private Image myImageClick;
        private Image myImageSelected;

        private bool myIsSwtich = false;
        private bool mySelected = false;

        public bool MouseIsLeave = false;

        public ButtonEx()
        {
            this.BackColor = Color.Transparent;

            this.MouseEnter += new EventHandler(ButtonEx_MouseEnter);
            this.MouseLeave += new EventHandler(ButtonEx_MouseLeave);
            this.MouseDown += new MouseEventHandler(ButtonEx_MouseDown);
            this.MouseUp += new MouseEventHandler(ButtonEx_MouseUp);
            this.MouseClick += new MouseEventHandler(ButtonEx_MouseClick);
        }

        void ButtonEx_MouseClick(object sender, MouseEventArgs e)
        {
            if (myIsSwtich)
            {
                Selected = true;
            }
        }

        void ButtonEx_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseIsLeave)
            {
                this.Image = mySelected ? myImageSelected : myImageNormal;
                MouseIsLeave = false;
            }
            else
            {
                this.Image = mySelected ? myImageSelected : (myImageHover == null ? myImageNormal : myImageHover);
            }
        }

        void ButtonEx_MouseDown(object sender, MouseEventArgs e)
        {
            if (myImageClick != null)
            {
                this.Image = myImageClick;
            }
        }

        void ButtonEx_MouseLeave(object sender, EventArgs e)
        {
            this.Image = mySelected ? myImageSelected : myImageNormal;
        }

        void ButtonEx_MouseEnter(object sender, EventArgs e)
        {
            if (myImageHover != null)
            {
                this.Image = mySelected ? myImageSelected : myImageHover;
            }
        }

        [DefaultValue(null), Description("是否切换开关")]
        public bool IsSwitch
        {
            get
            {
                return myIsSwtich;
            }
            set
            {
                myIsSwtich = value;
            }
        }

        [DefaultValue(null), Description("是否选中")]
        public bool Selected
        {
            get
            {
                return mySelected;
            }
            set
            {
                this.mySelected = value;
                if (mySelected)
                {
                    if (myImageSelected != null)
                    {
                        this.Image = myImageSelected;
                        this.BringToFront();
                        this.Refresh();
                    }

                    if (myIsSwtich && this.Parent != null)
                    {
                        ButtonEx bt;
                        foreach (object obj in this.Parent.Controls)
                        {
                            bt = (ButtonEx)obj;
                            if (bt != this)
                            {
                                bt.Selected = false;
                            }
                        }

                    }
                }
                else
                {
                    this.Image = myImageNormal;
                }
            }
        }

        [DefaultValue(null), Description("默认图片")]
        public Image ImageNormal
        {
            get
            {
                return myImageNormal;
            }
            set
            {
                myImageNormal = value;
                this.Image = myImageNormal;
            }
        }

        [DefaultValue(null), Description("悬停时图片")]
        public Image ImageHover
        {
            get
            {
                return myImageHover;
            }
            set
            {
                myImageHover = value;
            }
        }

        [DefaultValue(null), Description("点击时图片")]
        public Image ImageClick
        {
            get
            {
                return myImageClick;
            }
            set
            {
                myImageClick = value;
            }
        }

        [DefaultValue(null), Description("选择后图片")]
        public Image ImageSelected
        {
            get
            {
                return myImageSelected;
            }
            set
            {
                myImageSelected = value;
            }
        }
    }
}
