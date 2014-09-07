using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 图片浏览器
{
    public partial class FormLarge : Form
    {
        public FormLarge()
        {
            InitializeComponent();
        }

        private void FormLarge_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
