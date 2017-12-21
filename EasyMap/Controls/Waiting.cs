using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap.Properties;

namespace EasyMap.Controls
{
    public partial class Waiting : UserControl
    {
        private bool _AutoProcess = true;
        public int ProcessValue
        {
            get { return progressBar1.Value; }
            set { progressBar1.Value = value; }
        }

        public void SetAutoProcess(bool auto)
        {
            if (auto)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 30;
            }
            timer1.Enabled = auto;
            _AutoProcess = auto;
        }

        public int MaxProcessValue
        {
            get { return progressBar1.Maximum; }
            set { progressBar1.Maximum = value; }
        }

        public int MinProcessValue
        {
            get { return progressBar1.Minimum; }
            set { progressBar1.Minimum = value; }
        }

        public Waiting()
        {
            InitializeComponent();
            pictureBox2.Parent = this;
        }

        public string Tip
        {
            get{return label1.Text;}
            set
            {
                label1.Text = value;
                Image img=Resources.waiting.Clone() as Image;
                Graphics g = Graphics.FromImage(img);
                g.DrawString(value, new Font("", 11), Brushes.White, label1.Left, label1.Top);
                g.Dispose();
                pictureBox2.Image = img;
            }
        }

        private void Waiting_VisibleChanged(object sender, EventArgs e)
        {
            this.Left = (Parent.Width - this.Width) / 2;
            this.Top = (Parent.Height - this.Height) / 2;
            if (_AutoProcess)
            {
                timer1.Enabled = this.Visible;
            }
            progressBar1.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
            }
            else
            {
                progressBar1.Value++;
            }
        }
    }
}
