using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PhotoSettings;
using System.Windows.Forms;
using EasyMap.Properties;

namespace EasyMap.Controls
{
    public partial class MyFlag : PictureBox
    {
        private PhotoData _Photo;

        internal PhotoData Photo
        {
            get { return _Photo; }
            set { _Photo = value; }
        }
        public MyFlag()
        {
            InitializeComponent();
            Image = Resources.DATABASE;
            Width = Image.Width;
            Height = Image.Height;
        }

        public MyFlag(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
